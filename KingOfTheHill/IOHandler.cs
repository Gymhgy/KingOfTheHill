using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KingOfTheHill {
    public static class IOHandler {

        /// <summary>
        /// Run a submission with specified command line arguments
        /// </summary>
        /// <param name="submission">The submission to be run</param>
        /// <param name="arguments">The arguments to be passed into the submission</param>
        /// <param name="timeout">(Optional)The timeout in milliseconds. Default is 50ms</param>
        /// <returns></returns>
        public static string RunSubmission(Submission submission, string[] arguments, int timeout = 50) {
            var startInfo = new ProcessStartInfo(submission.ExecutionPath, string.Join(" ", arguments.Select(EscapeCommandLineArg)))
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process p = Process.Start(startInfo)) {
                if(!p.WaitForExit(timeout)) {
                    try {
                        p.Kill();
                        return "";
                    }
                    catch (InvalidOperationException) {
                        p.WaitForExit();
                        return p.StandardOutput.ReadToEnd();
                    }
                }
                return p.StandardOutput.ReadToEnd();
            }
        }

        /// <summary>
        /// Runs a group of submissions asynchrounously using a given lambda
        /// </summary>
        /// <param name="submission">The submission to be run</param>
        /// <param name="arguments">The arguments to be passed into the submission</param>
        /// <returns></returns>
        public async static Task<Dictionary<string, string>> RunSubmissionGroup(IEnumerable<Submission> submissions, Func<Submission, string[]> argumentsSelector) {
            Dictionary<string, string> results = new Dictionary<string, string>();
            foreach (var submission in submissions) {
                results[submission.Name] = await Task.Run(() => RunSubmission(submission, argumentsSelector(submission)));
            }
            return results;
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
