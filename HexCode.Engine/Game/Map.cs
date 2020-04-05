using System;
using HexCode.Common;

namespace HexCode.Engine
{
    public class Map 
    {

        public Map() // serialization
        {

        }
        public Map(int width, int height)
        {
            Width = width;
            Height = height;
   
            Tiles = new TileType[width, height];
       }
        public void SetTileType(int x, int y, TileType tt)
        {
            Tiles[x, y] = tt;
        }
        public void SetTileType(Location loc, TileType tt)
        {
            SetTileType(loc.XPos, loc.YPos, tt);
        }

        public TileType GetTileType(int x, int y)
        {
            return Tiles[x, y];
        }

        public TileType GetTileType(Location loc)
        {
            return GetTileType(loc.XPos, loc.YPos);
        }

        public TileType[,] Tiles { get; set; }


        public int Width { get; set; }
        public int Height { get; set; }
        public int RobotsPerTeam { get; set; }


        public bool IsOnMap(Location loc)
        {
            if (loc.XPos >= 0 & loc.XPos < this.Width & loc.YPos >= 0 & loc.YPos < this.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}