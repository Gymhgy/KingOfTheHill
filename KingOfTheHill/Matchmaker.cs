using System;
using System.Collections.Generic;
using System.Text;

namespace KingOfTheHill {
    public class Matchmaker {

        /// <summary>
        /// Runs a round robin tournament with <code>rounds</code> rounds. Note: Only works in 1v1 games
        /// </summary>
        /// <param name="pool">Contestant Pool</param>
        /// <param name="rounds">Number of rounds to play</param>
        /// <param name="matchRunner">Logic behind the match</param>
        /// <returns>A <code>MatchHistory</code> object, which represents the matches that were played</returns>
        public static MatchHistory RoundRobin(IList<Submission> pool, int rounds, Func<Submission, Submission, (int, int)> matchRunner) {
            List<Match> matches = new List<Match>();
            for(; rounds > 0; rounds--) {
                for(int i = 0; i < pool.Count; i++) {
                    for (int j = i + 1; j < pool.Count; j++) {

                        //Takes two submissions from the pool aand run them through the user supplied lambda
                        var (aPoints, bPoints) = matchRunner(pool[i], pool[j]);
                        matches.Add(new Match(new Dictionary<Submission, int> {
                            { pool[i], aPoints },
                            { pool[j], bPoints }
                        }));;
                    }
                }
            }
            return new MatchHistory(matches);
        }
        
    }
}
