using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace KingOfTheHill {
    public static class IOHandler {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string RunSubmission(Submission submission, string[] arguments) {
            var startInfo = new ProcessStartInfo(submission.ExecutionPath, string.Join(" ", arguments.Select(EscapeCommandLineArg)))
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process p = Process.Start(startInfo)) {
                return p.StandardOutput.ReadToEnd();
            }
        }

        /// <summary>
        /// Escapes a command line argument so that it doesn't break anything when passed in
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private static string EscapeCommandLineArg(string arg) {
            //Wrap the arg in double quotes
            //Then double the number of backslashes at the end
            //If there is at least 2 backslashes at the end
            return "\"" + Regex.Replace(arg, @"(\\+)$", @"$1$1") + "\"";
        }

    }
}
