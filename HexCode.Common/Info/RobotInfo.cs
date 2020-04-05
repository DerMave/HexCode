
namespace HexCode.Common
{
    public class RobotInfo 
    {

        public RobotInfo(IRobotController robotController, Location attackLocation, bool turnCompleted, bool hasOutage)
        {
            Team = robotController.Team;
            RobotType = robotController.RobotType;
            AttackLocation = attackLocation;
            Location = robotController.Location;
            Health = robotController.Health;
            Energy = robotController.Energy;
            TurnCompleted = turnCompleted;
            HasOutage = hasOutage;
            Parts = robotController.Parts;
            Id = robotController.Id;
        }

        public byte Id { get; }
        public Team Team { get;  }
        public IRobotType RobotType {  get; }
        public Location AttackLocation {  get; }
        public Location Location {  get; }
        public int Health {  get; }
        public int Energy {  get; }
        public bool TurnCompleted {  get; }
        public bool HasOutage {  get; }
        public int Parts { get; }
    }
}