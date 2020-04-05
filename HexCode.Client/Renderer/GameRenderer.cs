using System;
using System.Collections.Generic;
using System.Linq;
using HexCode.Common;
using HexCode.Engine;
using HexCode.Engine.Debug;
using HexCode.Engine.Game;
using SkiaSharp;

namespace HexCode.Client.Renderer
{
    public sealed class GameRenderer
    {
        private SKCanvas _canvas;
        private int _hexWidth;
        private float _hexHeight;
        private SKSize _screenSize;

        private RenderBitmaps Bitmaps = new RenderBitmaps();
        public RenderPaints Paints = new RenderPaints();

        public bool ShowRobotId { get; set; }
        public bool ShowDebugIndicators { get; set; }
        public bool ShowAllDebugIndicators { get; set; }
        public byte ShowDebugIndicatorRobotId { get; set; }

        public bool ShowScoreboard { get; set; } = false;
        public int Speed { get; set; }

        public GameRenderer(SKCanvas canvas, float scale, int hexWidth, float hexHeight, SKSize size)
        {

            _screenSize = new SKSize(size.Width * (1 / scale), size.Height * (1 / scale));
            _canvas = canvas;
            // handle the device screen density
            canvas.Scale(scale);

            // make sure the canvas Is blank
            canvas.Clear(SKColors.Black);
            _hexWidth = hexWidth;
            _hexHeight = hexHeight;



        }

        public void DrawMap(Map map, List<Location> ToxicLocations)
        {

            for (int x = 0; x < map.Width; x++) {
                for (int y = 0; y < map.Height; y++) {
                    if ((x + y) % 2 == 0) {
                        Location loc = new Location(x, y);
                        DrawHex(x, y, Paints.TerrainStroke);
                        if (map.GetTileType(loc) == TileType.Terrain) {
                            if (ToxicLocations.Any(x => x.Equals(loc))) {
                                DrawHex(x, y, Paints.TerrainToxic);
                            } else {
                                DrawHex(x, y, Paints.TerrainFill);
                            }
                        } else if (map.GetTileType(loc) == TileType.Water) {
                            DrawHex(x, y, Paints.Water);
                        } else if (map.GetTileType(loc) == TileType.Mountain) {
                            DrawHex(x, y, Paints.Mountain);
                        }
                    }
                }
            }
        }

        public void DrawGame(IGameController _gameController)
        {
            if (ShowScoreboard) {
                DrawScore(_gameController);
            }


            DrawMap(_gameController.Map, _gameController.ToxicLocations);

            if (_gameController != null && _gameController.Map != null) {


                foreach (RobotRenderInfo rri in _gameController.DeadRobotRenderInfos) {
                    DrawRobot(rri);
                    //Healthbar folgt der "Bewegung" der Robots(enthält keine eigene Bewegungslogik)
                }
                foreach (RobotRenderInfo rri in _gameController.RobotRenderInfos) {
                    DrawRobot(rri);
                }
                foreach (var rm in _gameController.RadioMessages) {
                    DrawRadio(rm);
                }
                if (ShowDebugIndicators) {

                    List<DebugIndicatorLine> debugIndicatorLines = _gameController.DebugIndicatorLines;
                    List<DebugIndicatorCircle> debugIndicatorCircles = _gameController.DebugIndicatorCircles;
                    List<DebugIndicatorText> debugIndicatorTexts = _gameController.DebugIndicatorTexts;

                    if (!ShowAllDebugIndicators) {
                        debugIndicatorLines = debugIndicatorLines.Where(x => x.RobotController.Id == ShowDebugIndicatorRobotId).ToList();
                        debugIndicatorCircles = debugIndicatorCircles.Where(x => x.RobotController.Id == ShowDebugIndicatorRobotId).ToList();
                        debugIndicatorTexts = debugIndicatorTexts.Where(x => x.RobotController.Id == ShowDebugIndicatorRobotId).ToList();
                    }

                    foreach (var dil in debugIndicatorLines) {
                        DrawDebugIndicatorLine(dil);
                    }
                    foreach (var dic in debugIndicatorCircles) {
                        DrawDebugIndicatorCircle(dic);
                    }
                    foreach (var dit in debugIndicatorTexts) {
                        DrawDebugIndicatorText(dit);
                    }
                }
            }


        }


        public void DrawScore(IGameController _gameController)
        {
            SKRect scoreRect = new SKRect(0, 0, 580, _screenSize.Height);
            _canvas.DrawRect(scoreRect, Paints.Score);

            SKRect logoR = section(scoreRect, 10, 10, scoreRect.Width - 20, 100);
            _canvas.DrawRect(logoR, Paints.LogoBg);
            DrawCenterText(@"# HexCode #", logoR, 68, SKColors.Red, true);



            //DrawCenterText(@"# HexCode #", section(scoreRect, 10, 110, scoreRect.Width - 10, 100), 56, SKColors.Red, true);

            SKRect statsRect = section(scoreRect, 0, 130, scoreRect.Width - 10, 800);
            SKRect leftStatsCol = margin(partition(statsRect, 2, 0, 1, 0), 20);
            SKRect rightStatsCol = margin(partition(statsRect, 2, 1, 1, 0), 20);

            float top = 0;
            DrawText("Round", leftStatsCol, 0, top, 48, SKColors.White);
            DrawText(_gameController.Round.ToString(), rightStatsCol, 0, top, 48, SKColors.White);

            top += 75;

            string speedText = (Speed == 0) ? "paused" : Speed.ToString() + "x";
            DrawText("Speed", leftStatsCol, 0, top, 48, SKColors.White);
            DrawText(speedText, rightStatsCol, 0, top, 48, SKColors.White);


            top += 120;
            DrawText(_gameController.BlueTeamName, leftStatsCol, 0, top, 48, SKColors.Blue);
            DrawText(_gameController.RedTeamName, rightStatsCol, 0, top, 48, SKColors.Red);

            top += 75;
            DrawText(_gameController.RobotRenderInfos.Where(x => x.Team == Team.Blue).Sum(x => x.Health).ToString() + "HP", leftStatsCol, 0, top, 48, SKColors.White);
            DrawText(_gameController.RobotRenderInfos.Where(x => x.Team == Team.Red).Sum(x => x.Health).ToString() + "HP", rightStatsCol, 0, top, 48, SKColors.White);



        }

        private SKRect Offset(SKRect rect, int leftOffset, int topOffset, int rightOffset, int bottomOffset)
        {
            return new SKRect(rect.Left + leftOffset, rect.Top + topOffset, rect.Right + leftOffset + rightOffset, rect.Bottom + topOffset + bottomOffset);
        }


        public void DrawRobot(HexCode.Engine.Game.RobotRenderInfo rri)
        {
            if ((rri.Location.XPos + rri.Location.YPos) % 2 == 0) {
                SKRect robotRect = GetSKRect(rri.Location.XPos, rri.Location.YPos);
                _canvas.DrawBitmap(getRobotBitmap(rri.Team, rri.Direction), robotRect);

                if (rri.HasOutage) {
                    _canvas.DrawBitmap(Bitmaps.Lightning, robotRect);
                }

                if (rri.Timeouts > 0) {
                    _canvas.DrawBitmap(Bitmaps.Stopwatch, robotRect);
                }
            }

            if (rri.AttackLocation != null) {
                var p1 = getSKPoint(rri.Location, true);
                p1 = weaponOffset(p1, rri.Direction);

                var p2 = getSKPoint(rri.AttackLocation, true);
                p2 = weaponOffset(p2, rri.Direction);
                if (rri.Team == Team.Blue) {
                    _canvas.DrawLine(p1, p2, Paints.LaserBlue);
                } else if (rri.Team == Team.Red) {
                    _canvas.DrawLine(p1, p2, Paints.LaserRed);
                } else {
                    throw new NotImplementedException();
                }

            }

            drawHealthBar(rri.Location, rri.Health, rri.RobotType.MaxHealth);

            if (ShowRobotId) {
                SKPoint start = GetSKPoint(rri.Location.XPos, rri.Location.YPos, true);
                start.Offset(0f, 18f);
                _canvas.DrawText("#" + rri.RobotId.ToString(), start, Paints.InnerRobotId);
                _canvas.DrawText("#" + rri.RobotId.ToString(), start, Paints.OuterRobotId);
            }
        }

        private SKPoint getSKPoint(HexCode.Common.Location loc, bool midPoint = false)
        {
            return GetSKPoint(loc.XPos, loc.YPos, midPoint);
        }

        public void DrawDebugIndicatorLine(DebugIndicatorLine dil)
        {
            SKPaint pLine = new SKPaint() { StrokeWidth = 6, Color = Paints.GetColor(dil.Color), IsAntialias = true, Style = SKPaintStyle.Stroke };
            var p1 = getSKPoint(dil.StartLocation, true);
            var p2 = getSKPoint(dil.EndLocation, true);
            _canvas.DrawLine(p1, p2, pLine);
        }

        public void DrawDebugIndicatorCircle(DebugIndicatorCircle dic)
        {
            SKPaint pLine = new SKPaint() { StrokeWidth = 6, Color = Paints.GetColor(dic.Color), IsAntialias = true, Style = SKPaintStyle.Stroke };
            var p1 = getSKPoint(dic.Location, true);

            _canvas.DrawCircle(p1, this._hexWidth / 2, pLine);
        }
        public void DrawDebugIndicatorText(DebugIndicatorText dit)
        {
            SKPaint pLine = new SKPaint() { TextAlign = SKTextAlign.Center, TextSize = 18, Color = Paints.GetColor(dit.Color), IsAntialias = true, Style = SKPaintStyle.Fill };
            var p1 = getSKPoint(dit.Location, true);
            if (dit.Text.Length <= 2) {
                pLine.TextSize = 32;
            }

            _canvas.DrawText(dit.Text, p1, pLine);
        }

        public SKPoint GetSKPoint(int x, int y, bool midPoint = false)
        {
            if ((x + y) % 2 == 0) {
                float xPos = _hexWidth * 0.75F * x;
                float yPos = _hexHeight * 0.5F * y;  // "Halbe Zeilen"
                if (midPoint) {
                    xPos += _hexWidth * 0.5F;
                    yPos += _hexHeight * 0.5F;
                }

                if (ShowScoreboard) {
                    return new SKPoint(600 + xPos, yPos);
                } else {
                    return new SKPoint(xPos, yPos);
                }

            } else {
                throw new ArgumentException();
            }
        }

        public SKRect GetSKRect(int x, int y)
        {
            if ((x + y) % 2 == 0) {
                float xPos = _hexWidth * 0.75F * x;
                float yPos = _hexHeight * 0.5F * y;  // "Halbe Zeilen"

                if (ShowScoreboard) {
                    return new SKRect(600 + xPos, yPos, 600 + xPos + _hexWidth, yPos + _hexHeight);
                } else {
                    return new SKRect(xPos, yPos, xPos + _hexWidth, yPos + _hexHeight);
                }

            } else {
                throw new ArgumentException();
            }
        }


        public void DrawWinnerScreen(HexCode.Common.Team winnerTeam, string teamName)
        {
            var screenRect = new SKRect(0, 0, _screenSize.Width, _screenSize.Height);

            SKColor teamColor = SKColors.Black;
            if (winnerTeam == HexCode.Common.Team.Blue) {
                teamColor = SKColors.Blue;
            } else if (winnerTeam == HexCode.Common.Team.Red) {
                teamColor = SKColors.Red;
            }

            DrawCenterText(teamName, partition(screenRect, 1, 0, 4, 1), 256f, teamColor, true);
            DrawCenterText("wins!", partition(screenRect, 1, 0, 4, 2), 256f, SKColors.White);
        }

        private SKRect margin(SKRect rect, float value)
        {
            return new SKRect(rect.Left + value, rect.Top + value, rect.Right - value, rect.Bottom - value);

        }
        private SKRect partition(SKRect rect, int columnCount, int columnIndex, int rowCount, int rowIndex)
        {
            float colWidth = rect.Width / columnCount;
            float rowHeight = rect.Height / rowCount;

            float left = rect.Left + (columnIndex * colWidth);
            float top = rect.Top + (rowIndex * rowHeight);
            return new SKRect(left, top, left + colWidth, top + rowHeight);

        }

        private SKRect section(SKRect rect, float left, float top, float width, float height)
        {

            float newLeft = rect.Left + left;
            float newTop = rect.Top + top;
            return new SKRect(newLeft, newTop, newLeft + width, newTop + height);

        }

        private void DrawCenterText(string text, SKRect rect, float textSize, SKColor color, bool whiteBorder = false)
        {
            //_canvas.DrawRect(rect, Paints.TerrainStroke);
            using (var paint = new SKPaint()) {
                paint.TextSize = textSize;
                paint.IsAntialias = true;
                //paint.FakeBoldText = true;
                paint.Color = color;
                paint.Style = SKPaintStyle.Fill;
                paint.TextAlign = SKTextAlign.Center;

                _canvas.DrawText(text, rect.Left + (rect.Width / 2), rect.Top + (rect.Height / 2) + (textSize * 1 / 3), paint);

                if (whiteBorder) {
                    paint.Style = SKPaintStyle.Stroke;
                    paint.StrokeWidth = textSize / 100;
                    paint.Color = SKColors.White;
                    _canvas.DrawText(text, rect.Left + (rect.Width / 2), rect.Top + (rect.Height / 2) + (textSize * 1 / 3), paint);
                }
            }

        }


        private void DrawText(string text, SKRect rect, float left, float top, float textSize, SKColor color)
        {

            //_canvas.DrawRect(rect, Paints.TerrainStroke);
            using (var paint = new SKPaint()) {
                paint.TextSize = textSize;
                paint.IsAntialias = true;
                //paint.FakeBoldText = true;
                paint.Color = color;
                paint.Style = SKPaintStyle.Fill;
                paint.TextAlign = SKTextAlign.Left;

                _canvas.DrawText(text, rect.Left + left, rect.Top + top + textSize, paint);
            }

        }

        public void DrawInfo()
        {

            var screenRect = new SKRect(0, 0, _screenSize.Width, _screenSize.Height);
            DrawCenterText("HexCode", partition(screenRect, 1, 0, 3, 0), 256f, SKColors.Red, true);


            using (var paint = new SKPaint()) {
                paint.TextSize = 50;
                paint.IsAntialias = true;
                paint.Color = SKColors.White;
                paint.Style = SKPaintStyle.Fill;
                paint.TextAlign = SKTextAlign.Center;
                _canvas.DrawText("enter = start new game", _screenSize.Width / 2, (_screenSize.Height / 2) - 150, paint);
                _canvas.DrawText("r = start replay", _screenSize.Width / 2, (_screenSize.Height / 2) - 100, paint);
                _canvas.DrawText("space = pause / resume", _screenSize.Width / 2, (_screenSize.Height / 2) - 50, paint);
                _canvas.DrawText("+ = speed up", _screenSize.Width / 2, (_screenSize.Height / 2) + 0, paint);
                _canvas.DrawText("- = speed down", _screenSize.Width / 2, (_screenSize.Height / 2) + 50, paint);
                _canvas.DrawText("o = options", _screenSize.Width / 2, (_screenSize.Height / 2) + 100, paint);
                _canvas.DrawText("p = show/hide panel", _screenSize.Width / 2, (_screenSize.Height / 2) + 150, paint);
                _canvas.DrawText("m = map editor", _screenSize.Width / 2, (_screenSize.Height / 2) + 200, paint);
            }

        }


        public void DrawStartScreen(string blueTeamName, string redTeamName)
        {

            var screenRect = new SKRect(0, 0, _screenSize.Width, _screenSize.Height);
            DrawCenterText(redTeamName, partition(screenRect, 5, 1, 3, 0), 256f, SKColors.Red, true);
            DrawCenterText("vs", partition(screenRect, 1, 0, 1, 0), 256f, SKColors.White);
            DrawCenterText(blueTeamName, partition(screenRect, 5, 3, 3, 2), 256f, SKColors.Blue, true);
        }

        private SKBitmap getRobotBitmap(Team team, Direction direction)
        {
            if (team == Team.Blue) {
                if (direction == Direction.North) return Bitmaps.RobotBlue_N;
                else if (direction == Direction.NorthEast) return Bitmaps.RobotBlue_NE;
                else if (direction == Direction.NorthWest) return Bitmaps.RobotBlue_NW;
                else if (direction == Direction.South) return Bitmaps.RobotBlue_S;
                else if (direction == Direction.SouthEast) return Bitmaps.RobotBlue_SE;
                else if (direction == Direction.SouthWest) return Bitmaps.RobotBlue_SW;
                else throw new NotImplementedException();
            } else if (team == Team.Red) {
                if (direction == Direction.North) return Bitmaps.RobotRed_N;
                else if (direction == Direction.NorthEast) return Bitmaps.RobotRed_NE;
                else if (direction == Direction.NorthWest) return Bitmaps.RobotRed_NW;
                else if (direction == Direction.South) return Bitmaps.RobotRed_S;
                else if (direction == Direction.SouthEast) return Bitmaps.RobotRed_SE;
                else if (direction == Direction.SouthWest) return Bitmaps.RobotRed_SW;
                else throw new NotImplementedException();
            } else throw new NotImplementedException();

        }


        public void DrawHex(int x, int y, SKPaint p)
        {
            if ((x + y) % 2 == 0) {
                var path = getHexagonPath(_hexWidth, GetSKPoint(x, y));
                _canvas.DrawPath(path, p);
            }
        }
        private void drawHealthBar(Location loc, int health, int maxHealth)
        {
            float healthPortion = (float)health / (float)maxHealth;
            SKPoint start = GetSKPoint(loc.XPos, loc.YPos, true);
            if (healthPortion <= 0) {
                _canvas.DrawText("DEAD", start, Paints.HealthBarFillWhite);
            } else {
                _canvas.DrawRect(start.X - 22, start.Y - 30, 44F, 14, Paints.HealthBarFillWhite);
                _canvas.DrawRect(start.X - 22, start.Y - 30, 44F * healthPortion, 14, Paints.HealthBarFillGreene);
                _canvas.DrawRect(start.X - 22, start.Y - 30, 44F, 14, Paints.HealthBarStroke);
            }

        }

        public void DrawRadio(RadioMessage rm)
        {

            SKPoint pos = GetSKPoint(rm.Location.XPos, rm.Location.YPos, true);
            pos.Offset(0, 30);
            _canvas.DrawText(rm.Text, pos, Paints.HealthBarFillWhite);
        }

        private SKPoint pointMove(SKPoint p, int deg, float distance)
        {
            float radians = Convert.ToSingle(Math.PI * deg / 180);
            float x = p.X + Convert.ToSingle(distance * Math.Cos(radians));
            float y = p.Y + Convert.ToSingle(distance * Math.Sin(radians));
            return new SKPoint(x, y);
        }

        private SKPoint weaponOffset(SKPoint center, Direction direction)
        {
            int deg = ((int)direction * 60) + 270;
            deg = deg % 360;
            return pointMove(center, deg, 5);
        }

        private SKPath getHexagonPath(int hexWidth, SKPoint midPoint)
        {
            var ret = new SKPath();
            var p0 = pointMove(midPoint, 0, hexWidth / 4F);
            ret.MoveTo(p0);
            for (int i = 0; i <= 5; i++) {
                int deg = i * 60;
                p0 = pointMove(p0, deg, hexWidth / 2F);
                ret.LineTo(p0);
            }

            return ret;
        }

    }
}