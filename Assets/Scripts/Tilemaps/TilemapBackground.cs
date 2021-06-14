using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bdl.Utils.Grid;

namespace Tilemaps
{
    public class TilemapBackground : Tilemap
    {
        // CONSTRUCTOR

        public TilemapBackground(int width, int height)
            : base(width, height) { }

        public TilemapBackground(int width, int height, float cellSize, Vector3 origin)
            : base(width, height, cellSize, origin) { }

        // PUBLIC METHODS

        public void AddDefault()
        {
            // Fill every tile with base type

            for (int x = 0; x < grid.Width; ++x)
                for (int y = 0; y < grid.Height; ++y)
                    SetTilemapSprite(x, y, TilemapType.GroundA);
        }

        public override void SetTilemapSprite(int x, int y, TilemapType tilemapType)
        {
            var tile = grid.GetGridObject(x, y);
            if (tile is null)
                return; // Out of range

            if (tilemapType is TilemapType.GroundA || tilemapType is TilemapType.GroundB)
                tile.SetType(tile.Y % 2 == 0 ? TilemapType.GroundA : TilemapType.GroundB);
            else
                tile.SetType(tilemapType);
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
