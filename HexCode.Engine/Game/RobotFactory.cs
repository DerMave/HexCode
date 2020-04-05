using HexCode.Common;
using System;

namespace HexCode.Engine
{
    public abstract class RobotFactory : IDisposable
    {
        public string TeamName { get; protected set;  }

        public abstract void Dispose();

        public abstract BaseRobot GetRobot();
    }
}