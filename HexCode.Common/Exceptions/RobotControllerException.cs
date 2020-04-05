using System;

namespace HexCode.Common.Exceptions
{
    public class RobotControllerException : Exception
    {
        public string MethodName { get; private set; }

        public RobotControllerException(string mn, string msg) : base(msg)
        {
            MethodName = mn;
        }

        public RobotControllerException(string mn, string msg, Exception innerException) : base(msg, innerException)
        {
            MethodName = mn;
        }
    }
}