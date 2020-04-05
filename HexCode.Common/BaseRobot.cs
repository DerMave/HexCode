
namespace HexCode.Common
{
    public abstract class BaseRobot 
    {
        protected IRobotController RobotController { get; private set; }

        public void SetRobotController(IRobotController robotController)
        {
            RobotController = robotController;
        }

        public abstract void RunRound();
    }
}