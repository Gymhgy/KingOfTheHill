using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingOfTheHill {

    /// <summary>
    /// Represents a submission to a king of the hill game
    /// </summary>
    public struct Submission {

        public static List<Submission> Submissions { get; private set; } = new List<Submission>();

        /// <summary>
        /// Name of the submission
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Path to the submission.
        /// </summary>
        public string FilePath { get; }
        /// <summary>
        /// Command to execute the program.
        /// </summary>
        public string ExecutionPath { get; }

        /// <summary>
        /// Create a submission
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filePath"></param>
        /// <param name="executionPath"></param>
        public Submission(string name, string filePath, string executionPath) {
            Name = name;
            FilePath = filePath;
            ExecutionPath = executionPath;
        }

        /// <summary>
        /// Initialize the list of submissions
        /// </summary>
        /// <param name="submissions"></param>
        public static void InitSubmissions (params Submission[] submissions) {
            if (Submissions.Count > 0)
                throw new InvalidOperationException("Seems like the list of submissions is already initialized. Are you sure you have not called this function twice?");
            Submissions = submissions.ToList();
        }
    }
}
