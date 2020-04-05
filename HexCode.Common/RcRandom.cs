using System;

namespace HexCode.Common
{
    public class RcRandom 
    {

        private Random _rnd;
        public RcRandom(int seed)
        {
            _rnd = new Random(seed);
        }

        public DebugColor GetRandomColor()
        {
            return (DebugColor)_rnd.Next(0, 140); //does not return transparent
        }

        public Direction GetRandomDirection()
        {
            return (Direction)_rnd.Next(1, 7);
        }

        public Rotation GetRandomRotation()
        {
            return (Rotation)_rnd.Next(1, 3);
        }

        //
        // Summary:
        //     Returns a random integer that is within a specified range.
        //
        // Parameters:
        //   minValue:
        //     The inclusive lower bound of the random number returned.
        //
        //   maxValue:
        //     The exclusive upper bound of the random number returned. maxValue must be greater
        //     than or equal to minValue.
        //
        // Returns:
        //     A 32-bit signed integer greater than or equal to minValue and less than maxValue;
        //     that is, the range of return values includes minValue but not maxValue. If minValue
        //     equals maxValue, minValue is returned.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     minValue is greater than maxValue.
        public int Next(int minValue, int maxValue)
        {
            return _rnd.Next(minValue, maxValue);
        }

        public int Next()
        {
            return _rnd.Next();
        }
    }
}
