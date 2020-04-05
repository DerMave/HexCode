using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace HexCode.Engine.Game
{
    public class MapLoader
    {

        public static string MapFolder { get; set; }
        public static void SaveMap(Map map, string mapName)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

            if (!System.IO.Directory.Exists(MapFolder)) {
                System.IO.Directory.CreateDirectory(MapFolder);
            }

            using (StreamWriter sw = File.CreateText(MapFolder + mapName + ".hcmap")) {
                serializer.Serialize(sw, map);
            }
        }

        public static Map LoadMap(string mapName)
        {
            JsonSerializer serializer = new JsonSerializer();
            Map map;
            serializer.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
            using (StreamReader sr = File.OpenText(MapFolder + mapName + ".hcmap")) {
                map = (Map)serializer.Deserialize(sr, typeof(Map));
            }

            return map;
        }


        public static List<string> GetMapNames()
        {
            List<string> ret = new List<string>();

            foreach (string s in Directory.GetFiles(MapFolder, "*.hcmap")) {
                string mapName = System.IO.Path.GetFileNameWithoutExtension(s);
                ret.Add(mapName);
            }

            return ret;
        }

        public static bool IsMapValid(string mapName)
        {
            return File.Exists(MapFolder + mapName + ".hcmap");
        }
    }
}
