using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bdl.Utils.Grid;

namespace Tilemaps
{
    public class TilemapMain : Tilemap
    {
        // CONSTRUCTOR

        public TilemapMain(int width, int height)
            : base(width, height, (g, x, y) => new TilemapMainObject(g, x, y)) { }

        public TilemapMain(int width, int height, float cellSize, Vector3 origin)
            : base(width, height, cellSize, origin, (g, x, y) => new TilemapMainObject(g, x, y)) { }

        // PUBLIC METHODS

        public void AddBedrock()
        {
            if (grid.Width < 2 || grid.Height < 2)
                return;

            // First and last columns
            for (int y = 0; y < grid.Height; ++y)
            {
                grid.GetGridObject(0, y).SetType((int)MainTileType.MainTileB);
                grid.GetGridObject(grid.Width - 1, y).SetType((int)MainTileType.MainTileB);
            }
            // First and last lines (except first/last column)
            for (int x = 1; x < grid.Width - 1; ++x)
            {
                grid.GetGridObject(x, 0).SetType((int)MainTileType.MainTileB);
                grid.GetGridObject(x, grid.Height - 1).SetType((int)MainTileType.MainTileB);
            }
        }

        public override void SetTilemapSprite(int x, int y, int tileType)
        {
            var tile = grid.GetGridObject(x, y);
            if (tile is null)
                return; // Out of range
            if (tile.X <= 0 || tile.X >= grid.Width - 1 || tile.Y <= 0 || tile.Y >= grid.Height - 1)
                return; // Prevent from altering bedrock
            tile.SetType(tileType);
        }

        public override void Save(string filename)
        {
            base.Save("tilemap_main_" + filename);
        }

        public override void Load(string filename)
        {
            base.Load("tilemap_main_" + filename);
        }
    }
}
