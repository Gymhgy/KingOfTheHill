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

        public Match(IDictionary<Submission, int> results) {
            Results = new ReadOnlyDictionary<Submission, int>(results);
            Participants = new ReadOnlyCollection<Submission>(results.Keys.ToList());
        }

    }
}
