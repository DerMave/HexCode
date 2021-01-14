using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HexCode.Engine;
using HexCode.Common;
using System.Timers;
using SkiaSharp;
using HexCode.Engine.Game;
using HexCode.Client.Renderer;

namespace HexCode.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            var rm = new System.Resources.ResourceManager(this.GetType());
            
            using (System.IO.Stream stream = assembly.GetManifestResourceStream("HexCode.Client.Resources.HexCode.ico")) {
                this.Icon = new System.Drawing.Icon(stream);
            }

            //this.Icon = ((System.Drawing.Icon)(rm.GetObject("$this.Icon")));
            _timer.Elapsed += OnTimedEvent;
        }


        private System.Timers.Timer _timer = new System.Timers.Timer(1000);
        private int timerSpeed = 4;
        private float _scale = 0.5F;
        private IGameController _gameController;

        private bool _timeouts = true;


        private void skControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {

            var canvas = e.Surface.Canvas;
            GameRenderer gameRenderer = new GameRenderer(canvas, _scale, 80, GetHexagonHeight(80), new SKSize(skControl1.Width, skControl1.Height));
            gameRenderer.ShowScoreboard = true;
            gameRenderer.ShowRobotId = chkShowRobotId.Checked;

            if (_timer.Enabled) {
                gameRenderer.Speed = timerSpeed;
            }else {
                gameRenderer.Speed = 0;
            }


            if (cboIndicator.SelectedItem == null || ((RobotIdSelection)cboIndicator.SelectedItem).IsNone) {
                gameRenderer.ShowDebugIndicators = false;
            } else if (((RobotIdSelection)cboIndicator.SelectedItem).IsAll) {
                gameRenderer.ShowDebugIndicators = true;
                gameRenderer.ShowAllDebugIndicators = true;
            } else {
                gameRenderer.ShowDebugIndicators = true;
                gameRenderer.ShowAllDebugIndicators = false;
                gameRenderer.ShowDebugIndicatorRobotId = ((RobotIdSelection)cboIndicator.SelectedItem).Id;

            }








            if (_gameController != null) {
                if (_gameController.Round == 0) {
                    gameRenderer.DrawStartScreen(_gameController.BlueTeamName, _gameController.RedTeamName);
                } else if (_gameController.IsFinished) {

                    gameRenderer.DrawWinnerScreen(_gameController.WinnerTeam, _gameController.WinnerTeam == Team.Blue ? _gameController.BlueTeamName : _gameController.RedTeamName);
                } else {
                    lock (_syncLockObj) {
                        gameRenderer.DrawGame(_gameController);
                    }
                }
            } else {
                gameRenderer.DrawInfo();

            }
        }



        public static float GetHexagonHeight(int sideLength)
        {
            double radians = Math.PI * 30.0 / 180.0;
            return Convert.ToSingle(sideLength * Math.Cos(radians));
        }

        private void showOptions()
        {
            frmOptions f = new frmOptions();
            f.ShowDialog();
        }

        private void startGame()
        {
            var settings = Settings.Load();
            if (!LibraryRobotFactory.IsTypeValid(settings.PlayerBlueDll, settings.PlayerBlueTypeName) ||
                !LibraryRobotFactory.IsTypeValid(settings.PlayerRedDll, settings.PlayerRedTypeName) ||
                !MapLoader.IsMapValid(settings.MapName)) {
                showOptions();
                settings = Settings.Load();
            }


            lock (_syncLockObj) {
                if (_gameController != null && _gameController is GameController) {
                    ((GameController)_gameController).Dispose();
                    _gameController = null;
                }


                //_gameController = new GameController(new RoslynRobotFactory(@"C:\HexCodeCore\AlexRobot\MaveBot.cs"),
                //                                 new RoslynRobotFactory(@"C:\HexCodeCore\AlexRobot\MaveBot.cs"));

                int rndSeed = chkRandomSeed.Checked ? new Random().Next() : int.Parse(txtSeed.Text);

                if (chkRandomSeed.Checked) {
                    txtSeed.Text = rndSeed.ToString();
                }

                _gameController = new GameController(new LibraryRobotFactory(settings.PlayerRedDll, settings.PlayerRedTypeName),
                                                 new LibraryRobotFactory(settings.PlayerBlueDll, settings.PlayerBlueTypeName), rndSeed);


                ((GameController)_gameController).TimeoutsEnabled = _timeouts;
                //Map bigMap2 = MapLoader.LoadMap("BigMap2");
                ((GameController)_gameController).Map = MapLoader.LoadMap(settings.MapName);

                //_gameController = ReplayController.LoadReplay(@"C:\HexCodeCore\HexCode.Client\bin\Debug\netcoreapp3.1\Replays\MaveBot_VS_MaveBot_20200326T123957.hcrep");

                _gameController.StartGame();
                fillComboBox(_gameController);
            }
            skControl1.Invalidate();
            changeTimerSpeed();

        }


        public static string OpenReplayDialog()
        {
            string ret = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = ".hcrep|*.hcrep";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            openFileDialog.InitialDirectory = System.IO.Path.GetFullPath(System.IO.Path.GetDirectoryName(@".\Replays\"));
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                ret = openFileDialog.FileName;
            }

            return ret;
        }

        private void startReplay(string replayFilePath = null)
        {
            lock (_syncLockObj) {
                if (_gameController != null && _gameController is GameController) {
                    ((GameController)_gameController).Dispose();
                    _gameController = null;
                }
                if (replayFilePath == null) {
                    replayFilePath = OpenReplayDialog();
                }

                _gameController = ReplayController.LoadReplay(replayFilePath);
                _gameController.StartGame();
                fillComboBox(_gameController);
            }
            skControl1.Invalidate();
            changeTimerSpeed();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private Object _syncLockObj = new Object();

        private void SetTimer()
        {
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
        private void OnTimedEvent(Object Source, ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            if (_gameController.IsFinished) {
                return;
            }
            lock (_syncLockObj) {
                _gameController.NextRound();
            }
            skControl1.Invalidate();
            _timer.Enabled = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {


        }

        //MouseClick
        private void skControl1_MouseClick(object sender, MouseEventArgs e)
        {
            skControl1.Focus();
        }

        private void skControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) {
                _timer.Enabled = !_timer.Enabled;
                skControl1.Invalidate();
            } else if (e.KeyCode == Keys.Add) {
                timerSpeed++;
                changeTimerSpeed();
            } else if (e.KeyCode == Keys.Subtract) {
                timerSpeed--;
                changeTimerSpeed();
            } else if (e.KeyCode == Keys.Enter) {
                startGame();
            } else if (e.KeyCode == Keys.O) {
                showOptions();
                startGame();
            } else if (e.KeyCode == Keys.M) {
                var f = new MapEditor();
                f.Show();
            } else if (e.KeyCode == Keys.R) {
                startReplay();
            } else if (e.KeyCode == Keys.P) {
                splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
            }


        }

        void changeTimerSpeed()
        {
            if (timerSpeed > 16) {
                timerSpeed = 16;
            } else if (timerSpeed < 1) {
                timerSpeed = 1;
            }
            _timer.Interval = 1000 / timerSpeed;
            OnTimedEvent(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var CommandLineArgs = Environment.GetCommandLineArgs().Skip(1); ;
            
            if (CommandLineArgs.Any() && System.IO.File.Exists(CommandLineArgs.First()) && CommandLineArgs.First().EndsWith(".hcrep")) {
                startReplay(CommandLineArgs.First());
            } else if (CommandLineArgs.Any() && CommandLineArgs.First().ToUpper() == "-NOTIMEOUT") {
                _timeouts = false;
            }

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string homeDir = System.IO.Path.GetDirectoryName(assembly.Location);
            MapLoader.MapFolder = homeDir + @"\Maps\";
            //ReplayController.MapFolder = homeDir + @"\Replays\";

        }

        private void chkShowRobotId_CheckedChanged(object sender, EventArgs e)
        {
            skControl1.Invalidate();
        }

        private void cboIndicator_SelectedIndexChanged(object sender, EventArgs e)
        {
            skControl1.Invalidate();
        }


        private void chkRandomSeed_CheckedChanged(object sender, EventArgs e)
        {
            txtSeed.Enabled = !chkRandomSeed.Checked;
        }

        private void fillComboBox(IGameController gc = null)
        {

            string lastSelectedValue = (string)cboIndicator.SelectedValue;
            BindingSource bs = new BindingSource();
            List<RobotIdSelection> list = new List<RobotIdSelection>();
            list.Add(new RobotIdSelection() { IsAll = true });
            list.Add(new RobotIdSelection() { IsNone = true });

            if (gc != null && gc.RobotRenderInfos != null) {
                foreach (byte robotId in gc.RobotRenderInfos.Select(x => x.RobotId)) {
                    list.Add(new RobotIdSelection() { Id = robotId });
                }
            }
            bs.DataSource = list;

            cboIndicator.DisplayMember = "Text";
            cboIndicator.ValueMember = "Text";
            cboIndicator.DataSource = bs;

            if (cboIndicator.SelectedIndex == -1 || cboIndicator.SelectedIndex == 0) {
                if (lastSelectedValue != null) {
                    cboIndicator.SelectedValue = lastSelectedValue;
                }
                if (cboIndicator.SelectedIndex == -1) {
                    cboIndicator.SelectedIndex = 0;
                }

            }

        }

        public sealed class RobotIdSelection
        {

            public bool IsAll { get; set; } = false;
            public bool IsNone { get; set; } = false;
            public byte Id { get; set; } = 0;

            public string Text
            {
                get
                {
                    if (IsAll) {
                        return "All";
                    } else if (IsNone) {
                        return "None";
                    } else {
                        return @"Robot #" + Id.ToString();
                    }
                }
            }
        }
    }
}


