using HexCode.Common;
using System;

namespace HexCode.Engine.Debug
{
    public class DebugIndicatorLine
    {
        public Location StartLocation { get; set; }
        public Location EndLocation { get; set; }
        public RobotController RobotController { get; set; }
        public DebugColor Color { get; set; }
    }
}
