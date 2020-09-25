using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace KingOfTheHill {
    public class MatchHistory {
        public ReadOnlyCollection<Match> Matches { get; }

        public MatchHistory(IList<Match> matches) {
            Matches = new ReadOnlyCollection<Match>(matches);
        }

        public MatchHistory LookupBySubmission(Submission submission) {
            return new MatchHistory(Matches.Where(x => x.Participants.Contains(submission)).ToList());
        }

    }
}
