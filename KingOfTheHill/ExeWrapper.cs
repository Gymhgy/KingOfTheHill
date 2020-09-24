using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class ExeWrapper {
    public static void Main(string[] args) {
        if (args.Length < 1)
            throw new Exception("No exe to wrap");
        if(File.Exists(args[0])) {
            var startInfo = new ProcessStartInfo(args[0], string.Join(" ", args.Skip(1).Select(EscapeCommandLineArg)))
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process p = Process.Start(startInfo)) {
                Console.WriteLine(p.StandardOutput.ReadToEnd());
            }
        }
        else {
            throw new Exception(string.Format("{0} is not an exe file", args[0]));
        }
    }
    private static string EscapeCommandLineArg(string arg) {
        //Wrap the arg in double quotes
        //Then double the number of backslashes at the end
        //If there is at least 2 backslashes at the end
        return "\"" + Regex.Replace(arg, @"(\\+)$", @"$1$1") + "\"";
    }
}