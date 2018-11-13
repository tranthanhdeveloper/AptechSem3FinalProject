using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Web.Configuration;
using System.Web.Http;
using Service.Service;

namespace Web.Controllers
{    
    public class MediaController : ApiController
    {
        public const int ReadStreamBufferSize = 1024 * 1024;
        public static readonly IReadOnlyDictionary<string, string> MimeNames;
        public static readonly IReadOnlyCollection<char> InvalidFileNameChars;
        public static readonly string InitialDirectory;

        static MediaController()
        {
            var mimeNames = new Dictionary<string, string>
            {
                {".mp3", "audio/mpeg"},
                {".mp4", "video/mp4"},
                {".ogg", "application/ogg"},
                {".ogv", "video/ogg"},
                {".oga", "audio/ogg"},
                {".wav", "audio/x-wav"},
                {".webm", "video/webm"}
            };

            MimeNames = new ReadOnlyDictionary<string, string>(mimeNames);
            InvalidFileNameChars = Array.AsReadOnly(Path.GetInvalidFileNameChars());
            InitialDirectory = WebConfigurationManager.AppSettings["DefaultVideoDirectory"];
        }

        #region Actions method
        [Helper.Sercurity.Authorize]
        public HttpResponseMessage Get(string f)
        {
            var response = new HttpResponseMessage();
            if (string.IsNullOrWhiteSpace(f) || AnyInvalidFileNameChars(f))
                throw new HttpResponseException(HttpStatusCode.NotFound);

            FileInfo fileInfo = new FileInfo(Path.Combine(InitialDirectory, f));

            if (!fileInfo.Exists)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var totalLength = fileInfo.Length;
            var rangeHeader = Request.Headers.Range;
            response.Headers.AcceptRanges.Add("bytes");

            if (rangeHeader == null || !rangeHeader.Ranges.Any())
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new PushStreamContent((outputStream, httpContent, transContext)
                    =>
                {
                    using (outputStream)
                    using (Stream inputStream = fileInfo.OpenRead())
                    {
                        try
                        {
                            inputStream.CopyTo(outputStream, ReadStreamBufferSize);
                        }
                        catch (Exception error)
                        {
                            Debug.WriteLine(error);
                        }
                    }
                }, GetMimeNameFromExt(fileInfo.Extension));

                response.Content.Headers.ContentLength = totalLength;
                return response;
            }

            long start = 0, end = 0;

            if (rangeHeader.Unit != "bytes" || rangeHeader.Ranges.Count > 1 ||
                !TryReadRangeItem(rangeHeader.Ranges.First(), totalLength, out start, out end))
            {
                response.StatusCode = HttpStatusCode.RequestedRangeNotSatisfiable;
                response.Content = new StreamContent(Stream.Null);
                response.Content.Headers.ContentRange = new ContentRangeHeaderValue(totalLength);
                response.Content.Headers.ContentType = GetMimeNameFromExt(fileInfo.Extension);

                return response;
            }

            var contentRange = new ContentRangeHeaderValue(start, end, totalLength);
            response.StatusCode = HttpStatusCode.PartialContent;
            response.Content = new PushStreamContent((outputStream, httpContent, transpContext)
                =>
            {
                using (outputStream)
                using (Stream inputStream = fileInfo.OpenRead())
                {
                    CreatePartialContent(inputStream, outputStream, start, end);
                }
            }, GetMimeNameFromExt(fileInfo.Extension));

            response.Content.Headers.ContentLength = end - start + 1;
            response.Content.Headers.ContentRange = contentRange;

            return response;
        }

        #endregion


        #region NonAction methods

        private static bool AnyInvalidFileNameChars(string fileName)
        {
            return InvalidFileNameChars.Intersect(fileName).Any();
        }

        private static MediaTypeHeaderValue GetMimeNameFromExt(string ext)
        {
            string value;

            if (MimeNames.TryGetValue(ext.ToLowerInvariant(), out value))
                return new MediaTypeHeaderValue(value);
            return new MediaTypeHeaderValue(MediaTypeNames.Application.Octet);
        }

        private static bool TryReadRangeItem(RangeItemHeaderValue range, long contentLength,
            out long start, out long end)
        {
            if (range.From != null)
            {
                start = range.From.Value;
                if (range.To != null)
                    end = range.To.Value;
                else
                    end = contentLength - 1;
            }
            else
            {
                end = contentLength - 1;
                if (range.To != null)
                    start = contentLength - range.To.Value;
                else
                    start = 0;
            }

            return start < contentLength && end < contentLength;
        }

        private static void CreatePartialContent(Stream inputStream, Stream outputStream,
            long start, long end)
        {
            var count = 0;
            var remainingBytes = end - start + 1;
            var position = start;
            var buffer = new byte[ReadStreamBufferSize];

            inputStream.Position = start;
            do
            {
                try
                {
                    if (remainingBytes > ReadStreamBufferSize)
                        count = inputStream.Read(buffer, 0, ReadStreamBufferSize);
                    else
                        count = inputStream.Read(buffer, 0, (int) remainingBytes);
                    outputStream.Write(buffer, 0, count);
                }
                catch (Exception error)
                {
                    Debug.WriteLine(error);
                    break;
                }

                position = inputStream.Position;
                remainingBytes = end - position + 1;
            } while (position <= end);
        }

        #endregion
    }
}