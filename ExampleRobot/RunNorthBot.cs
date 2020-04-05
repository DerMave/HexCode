using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HexCode.Common;

namespace ExampleRobot
{
    class RunNorthBot : BaseRobot
    {
        public override void RunRound()
        {
            var rc = this.RobotController;
            if (rc.CanMove(Direction.North, 1))
            {
                rc.Move(Direction.North, 1);
            }

        }
    }
}
