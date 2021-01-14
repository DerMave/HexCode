using System;
using System.Linq;
using HexCode.Common;

namespace SamplePlayer
{
    public class SampleBot : BaseRobot
    {


        private Location _lastLocation;
        private Direction _roamDirection;

        private void init()
        {
            _roamDirection = RobotController.Random.GetRandomDirection();
        }


        public override void RunRound()
        {
            //variable for convenience
            var rc = this.RobotController;
            if (rc.Round == 1) {
                init();
            }


            if (rc.IsToxic(rc.Location)) {
                //Standing on Toxic Ground --> Go to safety
                Direction? newDir = null;
                //check every direction (but in random order)
                foreach (Direction dir in Enum.GetValues<Direction>().OrderBy(x => rc.Random.Next())) {
                    //this would be our new Location
                    Location newLocation = rc.Location.DirectTo(dir, 1);
                    if (rc.CanMove(dir, 1) && !rc.IsToxic(newLocation)) {
                        //safe to go!
                        newDir = dir;
                        break;
                    }
                }

                //is there a safe Direction?
                if (newDir.HasValue) {
                    rc.Move(newDir.Value, 1);
                }

            } else {

                var enemies = rc.ScanForNearbyRobots().Where(x => x.Team != rc.Team && rc.Location.Distance(x.Location) <= rc.RobotType.AttackRange).ToList();
                if (!enemies.Any()) {
                    //roam ( follow our roam Direction (roughly) until we reach toxic ground)
                    Location newLocation = rc.Location.DirectTo(_roamDirection, 1);
                    if (rc.IsToxic(newLocation)) {
                        //change roam Direction to something opposite by rotating 2 to 4 times:
                        _roamDirection = _roamDirection.Rotate(rc.Random.Next(2, 4));
                        newLocation = rc.Location.DirectTo(_roamDirection, 1);
                    }

                    if (rc.CanMove(_roamDirection, 1)) {
                        rc.Move(_roamDirection, 1);
                    } else {
                        //find a way around
                        Direction? aroundDirection = null;
                        foreach (int i in new[] { 1, -1, 2, -2 }) {
                            if (rc.CanMove(_roamDirection.Rotate(i), 1)) {
                                aroundDirection = _roamDirection.Rotate(i);
                                break;
                            }
                        }
                        if (aroundDirection.HasValue) {
                            rc.Move(aroundDirection.Value, 1);
                        } else {
                            //that sucks... maybe we need a new direction:
                            _roamDirection = rc.Random.GetRandomDirection();
                            if (rc.CanMove(_roamDirection, 1)) {
                                rc.Move(_roamDirection, 1);
                            }
                        }

                    }


                } else if (enemies.Count == 1 && enemies.First().Health <= rc.Health) {
                    //1 enemy with same or lower health --> fight
                    var target = enemies.First();
                    //saving the target for later, maybe move first

                    if (rc.CanAttack(target.Location)) {
                        if (target.Location.Distance(rc.Location) > 1 && rc.Energy > 75) {
                            //chase the enemy (if energy is high)
                            var routeToTarget = rc.Location.RouteTo(target.Location);
                            if (rc.CanMove(routeToTarget.Direction1, 1)) {
                                //go to the main direction if possible
                                rc.Move(routeToTarget.Direction1, 1);
                            } else if (routeToTarget.Direction2.HasValue && rc.CanMove(routeToTarget.Direction2.Value, 1)) {
                                //go to secondary direction if it exists and it is possible
                                rc.Move(routeToTarget.Direction2.Value, 1);
                            }
                        }
                        //now (after move) we can attack
                        if (rc.CanAttack(target.Location)) {
                            rc.Attack(target.Location);
                        }
                    } else {
                        //something is blocking the path, try moving
                        Direction dir = rc.Random.GetRandomDirection();
                        for (int i = 0; i < 6; i++) { //try all directions
                            if (rc.CanMove(dir, 1)) {
                                rc.Move(dir, 1);
                                break;
                            } 
                            dir = dir.Rotate(); //rotate to next direction
                        }
                    }

                } else {
                    //many enemies or one with more health --> run
                    Direction? runDirection = null;
                    int distanceSum = enemies.Sum(x => rc.Location.Distance(x.Location));
                    //check all Directions for the highest Distance to the enemies
                    foreach (Direction dir in Enum.GetValues<Direction>().OrderBy(x => rc.Random.Next())) {
                        //Get the new possible Location
                        Location newLocation = rc.Location.DirectTo(dir, 1);
                        //check for distance
                        int newDistanceSum = enemies.Sum(x => newLocation.Distance(x.Location));
                        //is the new location better or equal than the last?
                        if (newDistanceSum >= distanceSum && rc.CanMove(dir, 1) && !rc.IsToxic(newLocation)) {
                            distanceSum = newDistanceSum;
                            runDirection = dir;
                        }
                    }

                    if (runDirection.HasValue) {
                        rc.Move(runDirection.Value, 1);
                    }

                }



            }




            _lastLocation = rc.Location;

        }
    }
}
