using System;
using System.Linq;
using HexCode.Common;

namespace HexCode.Engine
{
    public class RadioMessage : IRadioMessage
    {
        public Location Location { get; set; }
        public byte[] Data { get; set; }
        public byte RobotId { get; set; }

        public string Text
        {
            get { return Data.Aggregate<byte, string>("", (x, y) => (string)x + y.ToString("X2")); }
        }

    }
}
