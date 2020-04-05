using HexCode.Client.Renderer;
using HexCode.Common;
using HexCode.Engine;
using HexCode.Engine.Game;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace HexCode.Client
{
    public partial class MapEditor : Form
    {
        public MapEditor()
        {
            InitializeComponent();
        }

        enum dragOption
        {
            none,
            dragAdd,
            dragRemove
        }


        private Map Map;
        private float _scale = 0.5F;
        private GameRenderer _gameRenderer;
        private dragOption _dragging = dragOption.none;

        private float getDistance(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        private List<Location> _currentLocations = new List<Location>();

        private Location GetLocation(System.Drawing.Point mouseLocation)
        {
            Location ret = null;
            if (!mouseLocation.IsEmpty) {
                var hexHeight = Form1.GetHexagonHeight(80);

                for (int x = 0; x < Map.Width; x++) {
                    for (int y = 0; y < Map.Height; y++) {
                        if ((x + y) % 2 == 0) {
                            var r = _gameRenderer.GetSKPoint(x, y, true);
                            var mp = new SKPoint(mouseLocation.X / _scale, mouseLocation.Y / _scale);

                            var dist = getDistance(r.X, r.Y, mp.X, mp.Y);

                            if (dist <= hexHeight / 2) {
                                ret = new Location(x, y);
                                break;
                            }

                        }
                    }
                    if (ret != null) {
                        break;
                    }
                }
            }
            return ret;
        }
        
        private void cmdNewMap_Click(object sender, EventArgs e)
        {
            Map = new Map(Int32.Parse(txtWidth.Text), Int32.Parse(txtHeight.Text));
            skControl1.Invalidate();
        } 
        private void cmdRandom_Click(object sender, EventArgs e)
        {
            
            for (int x = 0; x < Map.Width; x++) {
                for (int y = 0; y < Map.Height; y++) {
                    Map.SetTileType(x, y, TileType.Terrain);
                }
            }

            for (int x = 0; x < 25; x++) {
                Location loc = GetRandomLocation();
                TileType tt = TileType.Mountain;
                if (_rnd.Next(0, 2) == 1) {
                    tt = TileType.Water;
                }

                Map.SetTileType(loc, tt);
                Direction dir = getRandomDirection();
                for (int y = 0; y < _rnd.Next(4, 8); y++) {
                    loc = loc.DirectTo(directionAdd(dir, _rnd.Next(-1, 2)), 1);
                    if (Map.IsOnMap(loc)) {
                        Map.SetTileType(loc, tt);
                    }
                }
            }
            skControl1.Invalidate();
        } 
        private void cmdSaveMap_Click(object sender, EventArgs e)
        {
            if (txtMapName.Text.Length == 0) {
                MessageBox.Show("Missing mapname");
            } else {
                Map.RobotsPerTeam = Int32.Parse(txtRobotsPerTeam.Text);
                MapLoader.SaveMap(Map, txtMapName.Text);
            }

        }
        private void cmdLoadMap_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = ".hcmap|*.hcmap";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            //if (tb.Text.Length > 0) {

            //} else {
            //    System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
            //    openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(ass.Location);
            //}
            openFileDialog.InitialDirectory = System.IO.Path.GetFullPath(System.IO.Path.GetDirectoryName(@".\Maps\"));
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                var fn = System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                Map = MapLoader.LoadMap(fn);
                txtMapName.Text = fn;
                txtRobotsPerTeam.Text = Map.RobotsPerTeam.ToString();
                skControl1.Invalidate();
            }

        }

        private void skControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            if (Map != null) {


                var canvas = e.Surface.Canvas;
                _gameRenderer = new GameRenderer(canvas, _scale, 80, Form1.GetHexagonHeight(80), new SKSize(skControl1.Width, skControl1.Height));

                _gameRenderer.DrawMap(Map, new List<Common.Location>());


                foreach (Location loc in _currentLocations) {
                    _gameRenderer.DrawDebugIndicatorCircle(new Engine.Debug.DebugIndicatorCircle() { Color = DebugColor.Red, Location = loc, RobotController = null });
                }



                //canvas.DrawCircle(, 10f, _gameRenderer.Paints.TerrainStroke);
            }


        }

        private void MapEditor_Load(object sender, EventArgs e)
        {

            skControl1.Invalidate();

            this.cmdTerrain.Click += (s, e) => transformLocation(TileType.Terrain);
            this.cmdMountain.Click += (s, e) => transformLocation(TileType.Mountain);
            this.cmdWater.Click += (s, e) => transformLocation(TileType.Water);
            //this.radioButton1.Click += (s, e) => MapInit();
            this.cmdLoadMap.Click += new System.EventHandler(this.cmdLoadMap_Click);
            this.cmdSaveMap.Click += new System.EventHandler(this.cmdSaveMap_Click);
            this.cmdNewMap.Click += new System.EventHandler(this.cmdNewMap_Click);
            this.cmdRandom.Click += new System.EventHandler(this.cmdRandom_Click);
        }

        private void transformLocation(Common.TileType tileType)
        {
            foreach (Location loc in _currentLocations) {
                Map.SetTileType(loc, tileType);
            }

            skControl1.Invalidate();

        }


        private void MapInit()
        {

            for (int x = 0; x < Map.Width; x++) {
                for (int y = 0; y < Map.Height; y++) {
                    if ((x + y) % 2 == 0) {
                        var r = _gameRenderer.GetSKRect(x, y);
                        //Location loc = new Location(x, y);
                        //DrawHex(x, y, Paints.TerrainStroke);

                    }
                }
            }
        }

        private void skControl1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = selectLocation(e.Location);
            skControl1.Invalidate();
        }

        private void skControl1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = dragOption.none;
        }

        private void skControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging != dragOption.none) {
                selectLocation(e.Location);
                skControl1.Invalidate();
            }
        }


        private dragOption selectLocation(System.Drawing.Point mouseLocation)
        {
            dragOption ret = dragOption.none;
            Location loc = GetLocation(mouseLocation);
            if (loc != null) {
                if (Control.ModifierKeys != Keys.Shift && _dragging == dragOption.none) {
                    _currentLocations.Clear();
                }

                if (_currentLocations.Contains(loc)) {
                    if (_dragging != dragOption.dragAdd) {
                        _currentLocations.Remove(loc);
                        ret = dragOption.dragRemove;
                    }
                } else {
                    if (_dragging != dragOption.dragRemove) {
                        _currentLocations.Add(loc);
                        ret = dragOption.dragAdd;
                    }
                }
            }
            return ret;
        }




        protected Random _rnd = new Random();
        public Location GetRandomLocation()
        {
            int x = 0;
            int y = 0;
            do {
                x = _rnd.Next(0, Map.Width);
                y = _rnd.Next(0, Map.Height);
            }
            while (!Common.Location.IsXYValid(x, y) || Map.GetTileType(x, y) != TileType.Terrain);
            return new Location(x, y);
        }


        private Direction directionAdd(Direction direction, int amount)
        {
            if (amount < 0) {
                amount = amount % 6 + 6;
            }

            int ret = ((int)direction - 1 + amount) % 6 + 1;
            return (Direction)ret;
        }

        private Direction getRandomDirection()
        {
            return (Direction)_rnd.Next(1, 7);
        }

    }
}
