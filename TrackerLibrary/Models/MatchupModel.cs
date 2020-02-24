using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class MatchupModel
    {
        /// <summary>
        /// Represents the entries in a matchup
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();

        /// <summary>
        /// Represents the winner of the matchup
        /// </summary>
        public TeamModel Winner { get; set; }

        /// <summary>
        /// Represents the round of the matchup
        /// </summary>
        public int MatchupRound { get; set; }

        /// <summary>
        /// Represents the tournament of the matchup
        /// </summary>
        public TournamentModel Tournament { get; set; }
    }
}
