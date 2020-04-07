using System.Collections.Generic;

namespace HexCode.Common
{
    public interface IRobotController 
    {
        byte Id { get; }
        int Health { get; }
        Location Location { get; }
        int Round { get; }
        int Energy { get; }
        Team Team { get; }
        IEnumerable<IRadioMessage> ReceiveRadioMessages();
        void SendRadioMessages(byte[] data);
        IRobotType RobotType { get; }
        void Move(Direction direction, int distance);
        bool CanMove(Direction direction, int distance);
        void Attack(Location location);
        bool CanAttack(Location location);
        IEnumerable<RobotInfo> ScanForNearbyRobots();
        bool IsOnMap(Location loc);
        bool IsToxic(Location loc);
        bool IsOccupied(Location loc);
        LocationInfo GetLocationInfo(Location loc);
        void DrawDebugIndicatorLine(Location locStart, Location locEnd, DebugColor color);
        void DrawDebugIndicatorCircle(Location loc, DebugColor color);
        void DrawDebugIndicatorText(Location loc, DebugColor color, string text);
        RcRandom Random { get; }
    }
}