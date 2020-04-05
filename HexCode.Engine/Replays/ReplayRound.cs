using HexCode.Common;
using HexCode.Engine;
using HexCode.Engine.Game;
using System;
using System.Collections.Generic;

namespace HexCode.Engine.Replays
{
    public class ReplayRound
    {
        public int Round { get; set; }
        public List<RobotRenderInfo> DeadRobotRenderInfos { get; set; } = new List<RobotRenderInfo>();
        public List<RobotRenderInfo> RobotRenderInfos { get; set; } = new List<RobotRenderInfo>();
        public List<RadioMessage> RadioMessages { get; set; } = new List<RadioMessage>();
        public List<Location> ToxicLocations { get; set; } = new List<Location>();

    }
}
