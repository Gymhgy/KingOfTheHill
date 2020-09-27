using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingOfTheHill {

    /// <summary>
    /// Represents a submission to a king of the hill game
    /// </summary>
    public struct Submission : IEqualityComparer<Submission> {

        private static List<Submission> Submissions = new List<Submission>();

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
        /// Create a submission.
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
        /// Get a submission by name from the list of all submissions.
        /// </summary>
        /// <param name="name">The name to be searched for.</param>
        /// <returns></returns>
        public static Submission Get(string name) {
            try {
                return Submissions.Single(submission => submission.Name == name);
            }
            catch(InvalidOperationException e){
                throw e;
            }
        }

        

        /// <summary>
        /// Exposes the list of submissions
        /// </summary>
        /// <returns>A copy of the submissions list</returns>
        public static IList<Submission> GetAll() {
            return Submissions.ToList();
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

        /// <summary>
        /// Checks whether two submissions are equal based on their <c>Name</c> property.
        /// </summary>
        /// <param name="x">The first submission to be compared.</param>
        /// <param name="y">The second submission to be compared</param>
        /// <returns></returns>
        public bool Equals(Submission x, Submission y) {
            return x.Equals(y);
        }

        /// <summary>
        /// Hash code for a Submission. Uses the <c>HashCode</c> of the name of the submission.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(Submission obj) {
            return obj.GetHashCode();
        }

        /// <summary>
        /// Check if there are any duplicate submissions
        /// </summary>
        /// <param name="submissions"></param>
        /// <returns></returns>
        internal bool Validate(IEnumerable<Submission> submissions) {
            return submissions.Distinct().Count() == submissions.Count();
        }

        /// <summary>
        /// Checks whether two submissions are equal based on their <c>Name</c> property.
        /// </summary>
        /// <param name="obj">The submission to be compared against.</param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            return obj is Submission && this.Name == ((Submission)obj).Name;
        }

        /// <summary>
        /// Hash code for this Submission. Uses the <c>HashCode</c> of the name of the submission.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return this.Name.GetHashCode();
        }

        /// <summary>
        /// Checks whether two submissions are equal based on their <c>Name</c> property.
        /// </summary>
        /// <param name="left">The first submission to be compared.</param>
        /// <param name="right">The second submission to be compared</param>
        /// <returns></returns>
        public static bool operator ==(Submission left, Submission right) {
            return left.Equals(right);
        }

        /// <summary>
        /// Checks whether two submissions are not equal based on their <c>Name</c> property.
        /// </summary>
        /// <param name="left">The first submission to be compared.</param>
        /// <param name="right">The second submission to be compared</param>
        public static bool operator !=(Submission left, Submission right) {
            return !(left == right);
        }
    }
}
