using System.Collections.Generic;
using System.Diagnostics;

namespace UnityPatcher
{
    internal class CmdUtility
    {
        public static void DoCmd(string arguments)
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = arguments
                }
            };
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }

        public static string DoCmdResult(string arguments, bool logErrors = false)
        {
            var outputLines = new List<string>();
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = arguments
                }
            };
            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null) outputLines.Add(e.Data);
            };
            if (logErrors)
            {
                process.ErrorDataReceived += (sender, e) =>
                {
                    if (e.Data != null) outputLines.Add("[ERR] " + e.Data);
                };
            }
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            return string.Join(System.Environment.NewLine, outputLines);
        }
    }
}
