using HexCode.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexCode.Common
{
    public static class EnumExtensions
    {
        public static Direction Rotate(this Direction direction, Rotation rotation = Rotation.clockwise, int value = 1)
        {
            if (rotation == Rotation.counterclockwise) { value = value * -1; }
            return directionAdd(direction, value);
        }

        /// <summary>
        /// rotates clockwise
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="value">1-5</param>
        /// <returns></returns>
        public static Direction Rotate(this Direction direction, int value )
        {
            return directionAdd(direction, value);
        }

        /// <summary>
        /// Add oder Subtract to Direction, with Limit correction
        /// North + 1 ==> NorthEast
        /// North -1 ==> NorthWest
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private static Direction directionAdd(Direction direction, int amount)
        {
            if (amount < 0) {
                amount = amount % 6 + 6;
            }

            int ret = ((int)(direction) - 1 + amount) % 6 + 1;
            return (Direction)ret;
        }
    }
}
