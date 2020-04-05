using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexCode.Client.Renderer
{
    sealed class RenderBitmaps
    {

        public SKBitmap RobotBlue_N { get; private set; }
        public SKBitmap RobotBlue_NE { get; private set; }
        public SKBitmap RobotBlue_NW { get; private set; }
        public SKBitmap RobotBlue_S { get; private set; }
        public SKBitmap RobotBlue_SE { get; private set; }
        public SKBitmap RobotBlue_SW { get; private set; }
        public SKBitmap RobotRed_N { get; private set; }
        public SKBitmap RobotRed_NE { get; private set; }
        public SKBitmap RobotRed_NW { get; private set; }
        public SKBitmap RobotRed_S { get; private set; }
        public SKBitmap RobotRed_SE { get; private set; }
        public SKBitmap RobotRed_SW { get; private set; }
        public SKBitmap Lightning { get; private set; }
        public SKBitmap Stopwatch { get; private set; }

        public RenderBitmaps()
        {

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.blue_N.png")) {
                RobotBlue_N = SKBitmap.Decode(stream);
            }
            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.blue_NE.png")) {
                RobotBlue_NE = SKBitmap.Decode(stream);
            }
            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.blue_NW.png")) {
                RobotBlue_NW = SKBitmap.Decode(stream);
            }
            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.blue_S.png")) {
                RobotBlue_S = SKBitmap.Decode(stream);
            }
            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.blue_SE.png")) {
                RobotBlue_SE = SKBitmap.Decode(stream);
            }
            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.blue_SW.png")) {
                RobotBlue_SW = SKBitmap.Decode(stream);
            }


            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.red_N.png")) {
                RobotRed_N = SKBitmap.Decode(stream);
            }
            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.red_NE.png")) {
                RobotRed_NE = SKBitmap.Decode(stream);
            }
            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.red_NW.png")) {
                RobotRed_NW = SKBitmap.Decode(stream);
            }
            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.red_S.png")) {
                RobotRed_S = SKBitmap.Decode(stream);
            }
            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.red_SE.png")) {
                RobotRed_SE = SKBitmap.Decode(stream);
            }
            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.red_SW.png")) {
                RobotRed_SW = SKBitmap.Decode(stream);
            }

            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.lightning.png")) {
                Lightning = SKBitmap.Decode(stream);
            }
            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.stopwatch.png")) {
                Stopwatch = SKBitmap.Decode(stream);
            }
        }

    }
}
