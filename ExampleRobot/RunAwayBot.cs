using HexCode.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleRobot
{
    public class RunAwayBot : BaseRobot
    {

        private Direction _LastDirection = Direction.North;
        
        public override void RunRound()
        {
            var rc = this.RobotController;
        
            if (rc.CanMove(_LastDirection, 1)) {
                rc.Move(_LastDirection, 1);
            } else {
                _LastDirection = rc.Random.GetRandomDirection();
                if (rc.CanMove(_LastDirection, 1)) {
                    rc.Move(_LastDirection, 1);
                }
            }

        }
    }
}
