using HexCode.Common;
using System;

namespace HexCode.Engine.Debug
{
    public class DebugIndicatorCircle
    {
        public Location Location { get; set; }
        public RobotController RobotController { get; set; }
        public DebugColor Color { get; set; }
    }
}
