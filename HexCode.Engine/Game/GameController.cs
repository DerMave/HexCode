using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HexCode.Common;
using HexCode.Engine.Debug;
using HexCode.Engine.Game;

namespace HexCode.Engine
{
    public class GameController : IDisposable, IGameController
    {


        public GameController(RobotFactory redRobotFactory, RobotFactory blueRobotFactory, int rndSeed)
        {
            _redRobotFactory = redRobotFactory;
            _blueRobotFactory = blueRobotFactory;
            RedTeamName = redRobotFactory.TeamName;
            BlueTeamName = blueRobotFactory.TeamName;

            Rnd = new Random(rndSeed);
            TimeoutsEnabled = true;
        }

        public Boolean TimeoutsEnabled { get; set; }
        public HexCode.Engine.Replays.ReplayContainer ReplayContainer { get; private set; }

        //public int RobotsPerTeam { get; set; } = 1;
        public int ToxRate { get; set; } = 6;

        public List<RobotController> RobotControllers { get; private set; }
        public int Round { get; private set; }
        public Map Map { get; set; }

        private RobotFactory _redRobotFactory;
        private RobotFactory _blueRobotFactory;


        private List<Location> _NonToxicLocations = null;
        public List<Location> ToxicLocations { get; private set; } = null;

        public List<RadioMessage> RadioMessages { get; private set; } = new List<RadioMessage>();

        public List<DebugIndicatorCircle> DebugIndicatorCircles { get; private set; } = new List<DebugIndicatorCircle>();
        public List<DebugIndicatorText> DebugIndicatorTexts { get; private set; } = new List<DebugIndicatorText>();
        public List<DebugIndicatorLine> DebugIndicatorLines { get; private set; } = new List<DebugIndicatorLine>();


        public Random Rnd { get; private set; }



        public List<RobotController> DeadRobots { get; private set; } = new List<RobotController>();

        public bool IsOnMap(Location loc)
        {
            return Map.IsOnMap(loc);
        }

        public bool IsToxic(Location loc)
        {
            return ToxicLocations.Any(x => x.Equals(loc));
        }

        public IEnumerable<RobotInfo> ScanForNearbyRobots(RobotController rc)
        {
            return RobotControllers.Where(x => x != rc && x.Location.Distance(rc.Location) <= rc.RobotType.ScannerRange).Select(x => x.GetRobotInfo()).ToList();
        }

        public IEnumerable<RadioMessage> ReceiveRadioMessages(RobotController rc)
        {
            return RadioMessages.Where(x => x.RobotId != rc.Id).OrderBy(x => Rnd.Next()).ToList();
        }

        public Location GetRandomLocation()
        {
            int x;
            int y;
            do {
                x = Rnd.Next(0, Map.Width);
                y = Rnd.Next(0, Map.Height);
            }
            while (!Location.IsXYValid(x, y) || Map.GetTileType(x, y) != TileType.Terrain);
            return new Location(x, y);
        }

        public void StartGame()
        {
            RobotControllers = new List<RobotController>();
            ReplayContainer = new HexCode.Engine.Replays.ReplayContainer() { BlueTeamName = BlueTeamName, RedTeamName = RedTeamName, Map = Map };

            for (int i = 1; i <= Map.RobotsPerTeam; i++) {
                foreach (Team team in new[] { Team.Red, Team.Blue }) {
                    Location rndLocation = null;
                    do {
                        rndLocation = GetRandomLocation();
                    }
                    while (GetRobotController(rndLocation) != null);


                    RobotControllers.Add(new RobotController(this, getNewRobot(team), team, rndLocation, new RobotType(), new RcRandom(Rnd.Next()), getNewId()));
                }
            }

            _NonToxicLocations = new List<Location>();
            ToxicLocations = new List<Location>();

            for (int x = 0; x < Map.Width; x++) {
                for (int y = 0; y < Map.Height; y++) {
                    if ((x + y) % 2 == 0) {
                        Location loc = new Location(x, y);
                        if (Map.GetTileType(loc) == TileType.Terrain) {
                            _NonToxicLocations.Add(loc);
                        }
                    }
                }
            }

            Location midMap = getMidPoint(Map);
            _NonToxicLocations = _NonToxicLocations.OrderByDescending(x => x.Distance(midMap) + Rnd.Next(0, 2)).ThenBy(x => Rnd.Next()).ToList();
            RobotRenderInfos = RobotControllers.Select(x => new RobotRenderInfo(x)).ToList();

        }

        private byte getNewId()
        {
            List<byte> list = new List<byte>();
            for (int i = 1; i <= 255; i++) {
                if (!RobotControllers.Where(x => x.Id == (byte)i).Any()) {
                    list.Add((byte)i);
                }
            }
            return list.OrderBy(x => Rnd.Next()).First();
        }

        public Team WinnerTeam { get; private set; }

        public bool IsFinished { get; private set; }
        public string BlueTeamName { get; private set; }
        public string RedTeamName { get; private set; }
        public List<RobotRenderInfo> DeadRobotRenderInfos { get; private set; }
        public List<RobotRenderInfo> RobotRenderInfos { get; private set; }

        private Location getMidPoint(Map map)
        {
            int x = (map.Width / 2);
            int y = (map.Height / 2);
            if ((x + y) % 2 == 1) {
                y--;
            }

            return new Location(x, y);
        }

        private BaseRobot getNewRobot(Team team)
        {
            if (team == Team.Red) { return _redRobotFactory.GetRobot(); } else { return _blueRobotFactory.GetRobot(); }
        }


  
        public void NextRound()
        {
            Round += 1;

            DebugIndicatorCircles.Clear();
            DebugIndicatorLines.Clear();
            DebugIndicatorTexts.Clear();


            for (int i = 1; i <= ToxRate; i++) {
                if (_NonToxicLocations.Any()) {
                    Location newToxicLoc = _NonToxicLocations.First();
                    _NonToxicLocations.Remove(newToxicLoc);
                    ToxicLocations.Add(newToxicLoc);
                }
            }

            RobotControllers.ForEach(x => x.TurnCompleted = false);

            // Process Round  
            foreach (RobotController rc in RobotControllers.OrderBy(x => Rnd.Next())) {
                // Alte nachrichten des Robots löschen
                foreach (RadioMessage rm in RadioMessages.Where(x => x.RobotId == rc.Id).ToList()) {
                    RadioMessages.Remove(rm);
                }
                rc.AttackLocation = null;


                rc.HasOutage = false;
                if (rc.Energy < 50) {
                    rc.HasOutage = (Rnd.Next(0, 51) > rc.Energy);
                }

                rc.Energy += rc.RobotType.EnergyRegeneration;
                if (rc.Energy > rc.RobotType.MaxEnergy) {
                    rc.Energy = rc.RobotType.MaxEnergy;
                }

                if (rc.Timeouts > 0) {
                    rc.Timeouts--;
                } else if (!rc.HasOutage) {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    try {
                        rc.Robot.RunRound();
                    }
                    catch (Exception ex) {
                        rc.AttackLocation = null;
                        rc.NewMoveLocation = null;
                        rc.HasOutage = true;
                    }

                    stopwatch.Stop();
                    if (stopwatch.ElapsedMilliseconds > 100 && TimeoutsEnabled) {
                        rc.Timeouts += (int)(stopwatch.ElapsedMilliseconds / 100);
                    }
                }

                //radiomessages get processed immediately
                if (rc.RadioMessageData != null) {
                    RadioMessages.Add(new RadioMessage() { Data = rc.RadioMessageData, Location = rc.Location, RobotId = rc.Id });
                    rc.RadioMessageData = null;
                }

                if (rc.NewMoveLocation != null) {
                    if (!IsOnMap(rc.NewMoveLocation)) { throw new ApplicationException(); }

                    rc.Direction = rc.Location.RouteTo(rc.NewMoveLocation).Direction1;
                    rc.Location = rc.NewMoveLocation;
                    rc.Energy -= rc.RobotType.MoveEnergyCost;
                    rc.NewMoveLocation = null;
                }

                rc.TurnCompleted = true;
            }

            //correctForLocationOverPopulation();



            // Attack & Degen 
            foreach (RobotController rc in RobotControllers) {
                if (rc.AttackLocation != null) {

                    var opponent = this.GetRobotController(rc.AttackLocation);
                    if (opponent != null) {
                        opponent.Health -= rc.RobotType.Damage;
                    }

                    rc.Direction = rc.Location.RouteTo(rc.AttackLocation).Direction1;
                    rc.Energy -= rc.RobotType.AttackEnergyCost;
                    //rc.AttackLocation = null;
                }

                if (this.IsToxic(rc.Location)) {
                    rc.Health -= GameConstants.TOXIC_DAMAGE;
                }



            }

            // Death
            foreach (RobotController rc in RobotControllers.ToList()) {
                if (rc.Health <= 0) {
                    rc.Health = 0;
                    rc.AttackLocation = null;
                    RobotControllers.Remove(rc);
                    foreach (RadioMessage rm in RadioMessages.Where(x => x.RobotId == rc.Id).ToList()) {
                        RadioMessages.Remove(rm);
                    }
                    DeadRobots.Add(rc);
                }
            }

            foreach (RobotController rc in RobotControllers) {
                //Regen
                if (!this.IsToxic(rc.Location)) {
                    rc.Health += rc.RobotType.HealthRegeneration;
                    if (rc.Health > rc.RobotType.MaxHealth) {
                        rc.Health = rc.RobotType.MaxHealth;
                    }

                }
            }

            DeadRobotRenderInfos = DeadRobots.Select(x => new RobotRenderInfo(x)).ToList();
            RobotRenderInfos = RobotControllers.Select(x => new RobotRenderInfo(x)).ToList();

            ReplayContainer.ReplayRounds.Add(new HexCode.Engine.Replays.ReplayRound()
            {
                Round = Round,
                RobotRenderInfos = RobotRenderInfos,
                DeadRobotRenderInfos = DeadRobotRenderInfos,
                RadioMessages = RadioMessages.ToList(),
                ToxicLocations = ToxicLocations.ToList(),
            });

            if (!RobotControllers.Any(x => x.Team == Team.Blue) && !IsFinished) {
                WinnerTeam = Team.Red;
                IsFinished = true;
            } else if (!RobotControllers.Any(x => x.Team == Team.Red) && !IsFinished) {
                WinnerTeam = Team.Blue;
                IsFinished = true;
            }

            if (IsFinished) {
                ReplayContainer.WinnerTeam = WinnerTeam;
                ReplayContainer.Rounds = Round;

                ReplayController.SaveReplay(ReplayContainer);

            }

        }





        public void DrawDebugIndicatorLine(RobotController robotController, Location locStart, Location locEnd, DebugColor color)
        {
            DebugIndicatorLines.Add(new Debug.DebugIndicatorLine() { RobotController = robotController, StartLocation = locStart, EndLocation = locEnd, Color = color });
        }

        public void DrawDebugIndicatorCircle(RobotController robotController, Location loc, DebugColor color)
        {
            DebugIndicatorCircles.Add(new Debug.DebugIndicatorCircle() { RobotController = robotController, Location = loc, Color = color });
        }
        public void DrawDebugIndicatorText(RobotController robotController, Location loc, DebugColor color, string text)
        {
            DebugIndicatorTexts.Add(new Debug.DebugIndicatorText() { RobotController = robotController, Location = loc, Color = color, Text = text });
        }

        /// <summary>
        /// Verhindert, dass mehrere Robots auf das selbe Feld laufen
        /// </summary>
        private void correctForLocationOverPopulation()
        {
            List<Location> setLocations = RobotControllers.Where(x => x.NewMoveLocation == null).Select(x => x.Location).ToList();

            foreach (RobotController rc in RobotControllers.Where(x => x.NewMoveLocation != null).ToList()) {
                if (setLocations.Any(x => x.Equals(rc.NewMoveLocation))) {
                    rc.NewMoveLocation = null;
                } else {
                    setLocations.Add(rc.NewMoveLocation);
                }
            }
        }

        public bool IsOccupied(Location loc)
        {
            return RobotControllers.Any(x => x.Location.Equals(loc));
        }

        public LocationInfo GetLocationInfo(Location loc)
        {
            LocationInfo ret = null;
            var rc = GetRobotController(loc);
            RobotInfo robotInfo = null;
            if (rc != null) {
                robotInfo = rc.GetRobotInfo();
            }

            if (IsOnMap(loc)) {
                ret = new LocationInfo(loc, true, IsToxic(loc), Map.GetTileType(loc), robotInfo);
            } else {
                ret = new LocationInfo(loc, false);
            }

            return ret;
        }

        public RobotController GetRobotController(Location loc)
        {
            return RobotControllers.Where(x => x.Location.Equals(loc)).FirstOrDefault();
        }

        public void Dispose()
        {
            _blueRobotFactory.Dispose();
            _blueRobotFactory = null;
            _redRobotFactory.Dispose();
            _redRobotFactory = null;
            _NonToxicLocations = null;
            RadioMessages = null;
            ToxicLocations = null;
        }

    }
}