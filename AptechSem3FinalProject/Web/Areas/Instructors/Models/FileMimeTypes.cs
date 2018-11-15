using System.Collections.Generic;

namespace Web.Areas.Instructors.Models
{
    public class FileMimeTypes
    {
        public readonly Dictionary<string, string> AcceptedFileType = new Dictionary<string, string>
        {
            {".mp3", "audio/mpeg"},
            {".mp4", "video/mp4"},
            {".ogg", "application/ogg"},
            {".ogv", "video/ogg"},
            {".oga", "audio/ogg"},
            {".wav", "audio/x-wav"},
            {".webm", "video/webm"}
        };
    }
}