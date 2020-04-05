using System;
using System.Linq;
using HexCode.Common;
using HexCode.Engine;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace HexCode.Client
{
    public class LibraryRobotFactory : RobotFactory
    {
        private string _path;

        private Type _type;

        public static IEnumerable<Type> FindTypes(string dllPath)
        {
            dllPath = Path.GetFullPath(dllPath);
            return Assembly.LoadFile(dllPath).GetTypes().Where(x => typeof(BaseRobot).IsAssignableFrom(x)).ToList();
        }


        public LibraryRobotFactory(string path, string typeName)
        {
            _path = path;
            _type = FindTypes(path).Where(x => x.Name == typeName).First();
            TeamName = _type.Name;
        }

        public static bool IsTypeValid(string path, string typeName)
        {
            return File.Exists(path) && FindTypes(path).Where(x => x.Name == typeName).Any();
        }

        public override BaseRobot GetRobot()
        {
            return (BaseRobot)Activator.CreateInstance(_type);
        }

        public override void Dispose()
        {
            
        }
    }
}