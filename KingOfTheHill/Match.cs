using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace KingOfTheHill {
    /// <summary>
    /// Represents a match played by a number of participants
    /// </summary>
    public class Match {

        /// <summary>
        /// The list of participants
        /// </summary>
        public ReadOnlyCollection<Submission> Participants { get; }

        /// <summary>
        /// The number of points earned by each submission
        /// </summary>
        public ReadOnlyDictionary<Submission, int> Results { get; }

        /// <summary>
        /// Initializes a Match, with the <paramref>Participants</paramref> list generated automatically from the provided dictionary
        /// </summary>
        /// <param name="results">The results of the match, with each <code>KeyValuePair</code> being a <code>Submission</code> and their points earned.</param>
        public Match(IDictionary<Submission, int> results) {
            Results = new ReadOnlyDictionary<Submission, int>(results);
            Participants = new ReadOnlyCollection<Submission>(results.Keys.ToList());
        }

    }
}
