using HexCode.Common;
using HexCode.Engine.Debug;
using HexCode.Engine.Replays;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace HexCode.Engine.Game
{
    public class ReplayController : IGameController
    {

        public ReplayController(ReplayContainer replayContainer)
        {
            _ReplayContainer = replayContainer;
        }


        private ReplayRound _CurrentReplayRound;
        private ReplayContainer _ReplayContainer;

        public int Round { get; private set; }
        public Map Map { get { return _ReplayContainer.Map; } }
        public Team WinnerTeam { get { return _ReplayContainer.WinnerTeam; } }
        public bool IsFinished { get { return Round >= _ReplayContainer.Rounds; } }
        public string BlueTeamName { get { return _ReplayContainer.BlueTeamName; } }
        public string RedTeamName { get { return _ReplayContainer.RedTeamName; } }
        public List<RadioMessage> RadioMessages { get { return _CurrentReplayRound.RadioMessages; } }
        public List<DebugIndicatorCircle> DebugIndicatorCircles { get; } = new List<DebugIndicatorCircle>(); // not part of the replay
        public List<DebugIndicatorText> DebugIndicatorTexts { get; } = new List<DebugIndicatorText>(); // not part of the replay
        public List<DebugIndicatorLine> DebugIndicatorLines { get; } = new List<DebugIndicatorLine>(); // not part of the replay
        public List<RobotRenderInfo> DeadRobotRenderInfos { get { return _CurrentReplayRound.DeadRobotRenderInfos; } }
        public List<RobotRenderInfo> RobotRenderInfos { get { return _CurrentReplayRound.RobotRenderInfos; } }

        public List<Location> ToxicLocations { get { return _CurrentReplayRound.ToxicLocations; } }

        public void NextRound()
        {
            if (_ReplayContainer.Rounds > Round) {
                Round++;
                _CurrentReplayRound = _ReplayContainer.ReplayRounds[Round - 1];
            }
        }

        public static byte[] SaveReplay(ReplayContainer replayContainer)
        {
            JsonSerializer serializer = new JsonSerializer();

            serializer.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

            //if (!System.IO.Directory.Exists(MapFolder)) {
            //    System.IO.Directory.CreateDirectory(MapFolder);
            //}

            //string fileName = replayContainer.RedTeamName + "_VS_" + replayContainer.BlueTeamName + "_" + DateTime.Now.ToString("yyyyMMddTHHmmss");

            byte[] data = null;
            //using (FileStream fileStream = File.Create(MapFolder + fileName + ".hcrep")) {
            using (MemoryStream ms = new MemoryStream()) {
                using (var zipStream = new GZipStream(ms, CompressionMode.Compress, true)) {
                    using (var streamWriter = new StreamWriter(zipStream)) {
                        using (var jsonWriter = new JsonTextWriter(streamWriter)) {
                            serializer.Serialize(jsonWriter, replayContainer);
                        }
                    }
                }
                data = ms.ToArray();
            }


            return data;
        }

        //public static String MapFolder { get; set; }
        public static ReplayController LoadReplay(string path)
        {
            JsonSerializer serializer = new JsonSerializer();
            ReplayContainer replayContainer;
            serializer.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
            using (FileStream fileStream = File.OpenRead(path)) { // @".\Replays\" + fileName + ".hcrep"
                using (var zipStream = new GZipStream(fileStream, CompressionMode.Decompress, true)) {
                    using (var streamRead = new StreamReader(zipStream)) {
                            //var txt = streamRead.ReadToEnd();
                        using (var jsonReader = new JsonTextReader(streamRead)) {
                            replayContainer = (ReplayContainer)serializer.Deserialize(jsonReader, typeof(ReplayContainer));
                        }
                    }
                }
            }

            return new ReplayController(replayContainer);
        }

        public static ReplayController GetReplayStream(string path)
        {
            JsonSerializer serializer = new JsonSerializer();
            ReplayContainer replayContainer;
            serializer.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
            using (FileStream fileStream = File.OpenRead(path)) { // @".\Replays\" + fileName + ".hcrep"
                using (var zipStream = new GZipStream(fileStream, CompressionMode.Decompress, true)) {
                    using (var streamRead = new StreamReader(zipStream)) {
                        var txt = streamRead.ReadToEnd();
                        using (var jsonReader = new JsonTextReader(streamRead)) {
                            replayContainer = (ReplayContainer)serializer.Deserialize(jsonReader, typeof(ReplayContainer));
                        }
                    }
                }
            }

            return new ReplayController(replayContainer);
        }

        public static ReplayController LoadReplayFromStream(Stream stream)
        {
            JsonSerializer serializer = new JsonSerializer();
            ReplayContainer replayContainer;
            serializer.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
            using (var zipStream = new GZipStream(stream, CompressionMode.Decompress, true)) {
                using (var streamRead = new StreamReader(zipStream)) {
                    using (var jsonReader = new JsonTextReader(streamRead)) {
                        replayContainer = (ReplayContainer)serializer.Deserialize(jsonReader, typeof(ReplayContainer));
                    }
                }
            }
            return new ReplayController(replayContainer);

        }

        public void StartGame()
        {
            NextRound();
        }

    }
}
