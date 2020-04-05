

namespace HexCode.Common
{
    public class Route
    {

        public Route(Direction direction1, int distance1, Direction? direction2, int? distance2)
        {
            Direction1 = direction1;
            Distance1 = distance1;
            Direction2 = direction2;
            Distance2 = distance2;
            TotalDistance = distance1 + (distance2 ?? 0);
        }

        public Direction Direction1 { get; }
        public int Distance1 { get; }
        public Direction? Direction2 { get; }
        public int? Distance2 { get; }
        public int TotalDistance { get; }
    }
}
