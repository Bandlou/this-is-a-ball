using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bdl.Utils.Grid;
using System;

namespace Tilemaps
{
    public class TilemapBackground : Tilemap
    {
        // CONSTRUCTOR

        public TilemapBackground(int width, int height)
            : base(width, height, (g, x, y) => new TilemapBackgroundObject(g, x, y)) { }

        public TilemapBackground(int width, int height, float cellSize, Vector3 origin)
            : base(width, height, cellSize, origin, (g, x, y) => new TilemapBackgroundObject(g, x, y)) { }

        // PUBLIC METHODS

        public void AddDefault()
        {
            // Fill every tile with base type
            for (int x = 0; x < grid.Width; ++x)
                for (int y = 0; y < grid.Height; ++y)
                    SetTilemapSprite(x, y, y % 2 == 0 ? (int)BackgroundTileType.BaseGrayA : (int)BackgroundTileType.BaseGrayB);
        }

        public override void SetTilemapSprite(int x, int y, int tileType)
        {
            var tile = grid.GetGridObject(x, y);
            if (tile is null)
                return; // Out of range

            tile.SetType((int)tileType);
        }

        public override void SetTilemapRenderer(TilemapRenderer tilemapRenderer)
        {
            base.SetTilemapRenderer(tilemapRenderer);
        }

        public override void Save(string filename)
        {
            base.Save("tilemap_background_" + filename);
        }

        public override void Load(string filename)
        {
            base.Load("tilemap_background_" + filename);
        }
    }
}
