using System;
using System.Collections.Generic;
using System.Linq;
using HexCode.Common;
using HexCode.Common.Exceptions;

namespace HexCode.Engine
{
    public class RobotController : IRobotController
    {
        private GameController _gameController;

        public RobotController(GameController gameController, BaseRobot robot, Team team, Location location, RobotType robotType, RcRandom random, byte id)
        {
            _gameController = gameController;
            Robot = robot;
            Team = team;
            Location = location;
            RobotType = robotType;
            Random = random;
            Health = robotType.MaxHealth;
            Energy = robotType.MaxEnergy;
            Id = id;
            robot.SetRobotController(this);
            Parts = 100;
        }
        public bool TurnCompleted { get; set; }

        public BaseRobot Robot { get; set; }
        public Location Location { get; set; }
        public Location NewMoveLocation { get; set; }
        public Direction Direction { get; set; } = Direction.North;
        public Location AttackLocation { get; set; }
        public Team Team { get; set; }
        public IRobotType RobotType { get; set; }
        public RcRandom Random { get; }
        public int Timeouts { get; set; }

        public int Round
        {
            get
            {
                return _gameController.Round;
            }
        }

        public int Health { get; set; }
        public byte[] RadioMessageData { get; set; }

        public int Energy { get; set; }
        public bool HasOutage { get; set; }

        public int Parts { get; set; }

        public byte Id { get; set; }

        public void Move(Direction direction, int distance)
        {
            if (AttackLocation != null)
            {
                throw new RobotControllerException("Move", "Move() darf nicht auf Attack() folgen (Robot muss sich zuerst bewegen und dann attacken)");
            }
            if (distance <= 0 | distance > RobotType.MoveRange)
            {
                throw new RobotControllerException("Move", "Fehler bei Parameter 'distance'");
            }

            Location newLoc = Location.DirectTo(direction, distance);
            if (!_gameController.IsOnMap(newLoc) || _gameController.Map.GetTileType(newLoc) != TileType.Terrain || _gameController.IsOccupied(newLoc))
            {
                throw new RobotControllerException("Move", "Ungültiger Move Befehl (siehe CanMove())");
            }


            NewMoveLocation = newLoc;
        }

        public void Attack(Location location)
        {
            if (CanAttack(location))
            {
                AttackLocation = location;
            }  
        }

        public bool CanMove(Direction direction, int distance)
        {
            if (distance <= 0 | distance > RobotType.MoveRange)
            {
                return false;
            }
            else
            {
                var newLoc = Location.DirectTo(direction, distance);
                if (_gameController.IsOnMap(newLoc) && _gameController.Map.GetTileType(newLoc) == TileType.Terrain && !_gameController.IsOccupied(newLoc))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool isTerrain(Location location)
        {
            return _gameController.Map.GetTileType(location) == TileType.Terrain;
        }

        private bool isTerrainOrWater(Location location)
        {
            return (_gameController.Map.GetTileType(location) == TileType.Terrain || _gameController.Map.GetTileType(location) == TileType.Water);
        }

        public bool CanAttack(Location location)
        {

            Location startLoc = NewMoveLocation ?? this.Location;
            int dist = startLoc.Distance(location);
            if (dist > 3 || !IsOnMap(location) || !isTerrain(location))
            {
                return false;
            }
            else if (dist == 1) { return true; }
            else if (dist == 2)
            {
                Route route = startLoc.RouteTo(location);
                if (!route.Direction2.HasValue)
                {
                    return isTerrainOrWater(startLoc.DirectTo(route.Direction1, 1));
                } 
                else
                {
                    return isTerrainOrWater(startLoc.DirectTo(route.Direction1, 1)) || isTerrainOrWater(startLoc.DirectTo(route.Direction2.Value, 1));
                }
            }
            else if (dist == 3)
            {
                Route route = startLoc.RouteTo(location);
                if (!route.Direction2.HasValue)
                {
                    return isTerrainOrWater(startLoc.DirectTo(route.Direction1, 1)) && isTerrainOrWater(startLoc.DirectTo(route.Direction1, 2));
                }
                else
                {
                    Location stop1 = startLoc.DirectTo(route.Direction1, 1);
                    Location stop2 = stop1.DirectTo(route.Direction2.Value, 1);
                    return isTerrainOrWater(stop1) && isTerrainOrWater(stop2);
                }
            } 
            else
            {
                return false;
            }
        }

        public IEnumerable<RobotInfo> ScanForNearbyRobots()
        {
            return _gameController.ScanForNearbyRobots(this);
        }

        public bool IsOnMap(Location loc)
        {
            return _gameController.IsOnMap(loc);
        }

        public bool IsToxic(Location loc)
        {
            if (Location.Distance(loc) > RobotType.ScannerRange)
            {
                throw new LocationOutOfSightException();
            }
            return _gameController.IsToxic(loc);
        }

        public RobotInfo GetRobotInfo()
        {
            return new RobotInfo(this, AttackLocation, TurnCompleted, HasOutage);
        }

        /// <summary>
        /// 
        /// throws LocationOutOfSightException
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public LocationInfo GetLocationInfo(Location loc)
        {
            if (Location.Distance(loc) > RobotType.ScannerRange)
            {
                throw new LocationOutOfSightException();
            }
            return _gameController.GetLocationInfo(loc);
        }

        public bool IsOccupied(Location loc)
        {
            if (Location.Distance(loc) > RobotType.ScannerRange)
            {
                throw new LocationOutOfSightException();
            }
            return _gameController.IsOccupied(loc);
        }

        public IEnumerable<HexCode.Common.IRadioMessage> ReceiveRadioMessages()
        {
            return _gameController.ReceiveRadioMessages(this).OfType<IRadioMessage>().ToList();
        }

        public void SendRadioMessages(byte[] data)
        {
            if (data.Length > 4)
            {
                throw new ArgumentOutOfRangeException(nameof(data), "RadioMessage data cannot be more than 4 bytes.");
            }
            RadioMessageData = data;
        }

        public void DrawDebugIndicatorLine(Location locStart, Location locEnd, DebugColor color)
        {
            _gameController.DrawDebugIndicatorLine(this, locStart, locEnd, color);
        }
        public void DrawDebugIndicatorCircle(Location loc, DebugColor color)
        {
            _gameController.DrawDebugIndicatorCircle(this, loc, color);
        }
        public void DrawDebugIndicatorText(Location loc, DebugColor color, string text)
        {
            _gameController.DrawDebugIndicatorText(this, loc, color, text);
        }

      
        LocationInfo IRobotController.GetLocationInfo(Location loc)
        {
            if (Location.Distance(loc) > RobotType.ScannerRange) {
                throw new LocationOutOfSightException();
            }
            return _gameController.GetLocationInfo(loc);
        }
    }
}