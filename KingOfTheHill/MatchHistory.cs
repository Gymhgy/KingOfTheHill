using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace KingOfTheHill {

    /// <summary>
    /// Represents a group of matches, with lookup capabilities
    /// </summary>
    public class MatchHistory {

        /// <summary>
        /// The matches in this <c>MatchHistory</c>
        /// </summary>
        public ReadOnlyCollection<Match> Matches { get; }

        /// <summary>
        /// Initializes a <c>MatchHistory</c> with <paramref name="matches"/>
        /// </summary>
        /// <param name="matches">The list of matches</param>
        public MatchHistory(IList<Match> matches) {
            Matches = new ReadOnlyCollection<Match>(matches);
        }

        /// <summary>
        /// Returns all matches that <paramref name="submission"/> is a participant of.
        /// </summary>
        /// <param name="submission">The submission to check against.</param>
        /// <returns>A <c>MatchHistory</c> objerct with the matches that <paramref name="submission"/> is a part of.</returns>
        public MatchHistory LookupBySubmission(Submission submission) {
            return new MatchHistory(Matches.Where(x => x.Participants.Contains(submission)).ToList());
        }

        /// <summary>
        /// Gets the total points for each participant in this match history, and sorts it
        /// </summary>
        /// <returns>A list containing a tuple of submission + points, sorted based on poitns.</returns>
        public List<(Submission submission, int points)> Rankings() {
            var pointsPerSubmission = new Dictionary<Submission, int>();

            foreach(var match in this.Matches) {
                foreach(var (participant, points) in match.Results) {
                    //We have to check if the participant exists in the dictionary first, else we'll get an error
                    if(pointsPerSubmission.ContainsKey(participant)) {
                        pointsPerSubmission[participant] += points;

                    }
                    //If it doesn't exist, initialize it
                    else {
                        pointsPerSubmission[participant] = points;
                    }
                }
            }

            return pointsPerSubmission.Select(x => (x.Key, x.Value)).OrderByDescending(x => x.Value).ToList();
        }

    }
}
