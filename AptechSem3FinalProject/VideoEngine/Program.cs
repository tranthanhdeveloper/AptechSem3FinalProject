using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace VideoEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseTemPath = @"E:\Sem3FinalProject\videosTest\";
            string fileName = "BestPractices.mp4";
            string destinationPath = @"E:\Sem3FinalProject\videosTest\destination2\";
            Directory.CreateDirectory(destinationPath);
            string ffmpegArg = string.Format("-i \"{0}\" -vf scale=1280:720 \"{1}\"", baseTemPath + fileName, destinationPath + fileName);
            string ffmpegPath = @"F:\AptechSem3FinalProject\AptechSem3FinalProject\VideoEngine\ffmpeg.exe";
            FFmpegProcess ffmpegTask = new FFmpegProcess(ffmpegPath, ffmpegArg);
            ffmpegTask.Start();
        }
    }
}
