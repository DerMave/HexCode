
namespace HexCode.Common
{
    public class LocationInfo 
    {
        public LocationInfo(Location location, bool isOnMap, bool isToxic, TileType tileType, RobotInfo robotInfo)
        {
            HasRobot = false;
            Location = location;
            RobotInfo = null;
            IsToxic = isToxic;
            IsOnMap = isOnMap;
            TileType = tileType;
            if (robotInfo != null)
            {
                HasRobot = true;
                RobotInfo = robotInfo;
            }

        }

        public LocationInfo(Location location, bool isOnMap)
        {
            HasRobot = false;
            Location = location;
            IsOnMap = isOnMap;
        }


        public bool HasRobot { get; }
        public Location Location { get; }
        public RobotInfo RobotInfo { get; }
        public bool IsToxic { get; }
        public bool IsOnMap { get; }
        public TileType TileType { get; }
    }
}