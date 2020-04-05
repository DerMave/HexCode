using System;
using HexCode.Common;

namespace HexCode.Engine.Debug
{
    public class DebugIndicatorText
    {
        public Location Location { get; set; }
        public RobotController RobotController { get; set; }
        public DebugColor Color { get; set; }
        public string Text { get; set; }

    }
}
