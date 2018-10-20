using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEngine
{
    class FFmpegProcess
    {
        public Process process;

        public FFmpegProcess(string ffmpegPath, string arguments)
        {
            process = new Process();
            process.StartInfo.FileName = ffmpegPath;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.EnableRaisingEvents = true;

        }

        public void Start()
        {
            process.Start();
        }

        public void KillProcess()
        {
            process.Close();
        }
    }
}
