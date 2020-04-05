using HexCode.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexCode.Client
{
    public class Settings : AppSettings<Settings>
    {
        public Settings() { }
        public string PlayerBlueDll { get; set; }

        public string PlayerBlueTypeName { get; set; }

        public string PlayerRedDll { get; set; }

        public string PlayerRedTypeName { get; set; }
        public string MapName { get; set; }
    }
}
