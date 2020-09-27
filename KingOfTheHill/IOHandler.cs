using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KingOfTheHill {
    /// <summary>
    /// A static class to provide I/O with regards to <see cref="Submission"/>
    /// </summary>
    public static class IOHandler {

        /// <summary>
        /// Run a submission with specified command line arguments
        /// </summary>
        /// <param name="submission">The submission to be run</param>
        /// <param name="arguments">The arguments to be passed into the submission</param>
        /// <param name="timeout">(Optional)The timeout in milliseconds. Default is 500ms</param>
        /// <returns></returns>
        public static string RunSubmission(Submission submission, string[] arguments, int timeout = 500) {
            var formattedArgs = string.Join(" ", arguments.Prepend(submission.FilePath).Select(EscapeCommandLineArg));
            var startInfo = new ProcessStartInfo(submission.ExecutionPath, formattedArgs)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            string output = "";
            using (Process process = Process.Start(startInfo)) {
                process.OutputDataReceived += (sender, e) => {
                    if (!string.IsNullOrEmpty(e.Data))
                        output += e.Data;
                };

                process.BeginOutputReadLine();
                process.WaitForExit(timeout);
                return output;
            }
        }

        /// <summary>
        /// Runs a group of submissions asynchrounously using a given argument selector function
        /// </summary>
        /// <param name="submissions">The submission to be run</param>
        /// <param name="argumentsSelector">A function that outputs a <c>string[]</c> when run with a <c>Submission</c></param>
        /// <returns></returns>
        public async static Task<Dictionary<Submission, string>> RunSubmissionGroup(IEnumerable<Submission> submissions, Func<Submission, string[]> argumentsSelector) {
            Dictionary<Submission, string> results = new Dictionary<Submission, string>();
            foreach (var submission in submissions) {
                results[submission] = await Task.Run(() => RunSubmission(submission, argumentsSelector(submission)));
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
