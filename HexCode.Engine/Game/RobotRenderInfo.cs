using HexCode.Common;
using System;
using System.Text.Json.Serialization;


namespace HexCode.Engine.Game
{
    public class RobotRenderInfo
    {
        private RobotRenderInfo() // for serialization
        {

        }
        public RobotRenderInfo(RobotController rc)
        {
            Location = rc.Location;
            Team = rc.Team;
            Direction = rc.Direction;
            HasOutage = rc.HasOutage;
            Timeouts = rc.Timeouts;
            AttackLocation = rc.AttackLocation;
            Health = rc.Health;
            RobotType = (RobotType)rc.RobotType;
            RobotId = rc.Id;
        }
        public Location Location { get; set; }
        public Team Team { get; set; }
        public Direction Direction { get; set; }
        public bool HasOutage { get; set; }
        public int Timeouts { get; set; }
        public Location AttackLocation { get; set; }
        public int Health { get; set; }
        public byte RobotId { get; set; }


        [JsonIgnore]
        public RobotType RobotType { get; set; } = RobotType.Default;
    }
}
