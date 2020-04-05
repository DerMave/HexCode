using HexCode.Common;
using HexCode.Engine.Debug;
using System;
using System.Collections.Generic;

namespace HexCode.Engine.Game
{
    public interface IGameController
    {
        public int Round { get; }
        public Map Map { get; }
        public List<Location> ToxicLocations { get; }
        public List<RobotRenderInfo> DeadRobotRenderInfos { get; }
        public List<RobotRenderInfo> RobotRenderInfos { get; }
        public List<RadioMessage> RadioMessages { get; }
        public List<DebugIndicatorCircle> DebugIndicatorCircles { get; }
        public List<DebugIndicatorText> DebugIndicatorTexts { get; }
        public List<DebugIndicatorLine> DebugIndicatorLines { get; }
        public Team WinnerTeam { get; }
        public bool IsFinished { get; }
        public string BlueTeamName { get; }
        public string RedTeamName { get; }
        public void NextRound();
        public void StartGame();
    }
}
