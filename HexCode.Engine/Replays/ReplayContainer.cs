using HexCode.Common;
using System.Collections.Generic;

namespace HexCode.Engine.Replays
{
    public class ReplayContainer
    {
        public Team WinnerTeam { get; set; }
        public Map Map { get; set; }
        public string BlueTeamName { get; set; }
        public string RedTeamName { get; set; }
        public int Rounds { get; set; }
        public List<ReplayRound> ReplayRounds { get; set; } = new List<ReplayRound>();
    }
}
