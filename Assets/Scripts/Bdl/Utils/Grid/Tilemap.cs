using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bdl.Utils.Grid
{
    public class Tilemap
    {
        // SERIALIZABLE CLASS
        [System.Serializable]
        public class SerializedTilemap
        {
            public TilemapObject.SerializedTilemapObject[] tilemapObjects;
        }

        // EVENTS
        public delegate void LoadedAction();
        public event LoadedAction OnLoaded;

        // PRIVATE FIELDS
        protected Grid<TilemapObject> grid;

        // CONSTRUCTOR

        public Tilemap(int width, int height) : this(width, height, 1, Vector3.zero) { }

        public Tilemap(int width, int height, float cellSize, Vector3 origin)
        {
            grid = new Grid<TilemapObject>(width, height, cellSize, origin, (Grid<TilemapObject> g, int x, int y) => new TilemapObject(g, x, y));
        }

        // PUBLIC METHODS

        public virtual void SetTilemapRenderer(TilemapRenderer tilemapRenderer)
        {
            tilemapRenderer.SetGrid(grid, this);
        }

        public virtual void SetTilemapCollisionRenderer(TilemapCollisionRenderer tilemapCollisionRenderer)
        {
            tilemapCollisionRenderer.SetGrid(grid, this);
        }

        public void SetTilemapSprite(Vector3 worldPosition, TilemapType tilemapSprite)
        {
            var tile = grid.GetGridObject(worldPosition);
            if (tile is null)
                return;
            SetTilemapSprite(tile.X, tile.Y, tilemapSprite);
        }

        public virtual void SetTilemapSprite(int x, int y, TilemapType tilemapType)
        {
            var tile = grid.GetGridObject(x, y);
            if (tile is null)
                return; // Out of range
            tile.SetType(tilemapType);
        }

        public virtual void Save(string filename)
        {
            var tilemapObjectList = new List<TilemapObject.SerializedTilemapObject>();
            for (int x = 0; x < grid.Width; ++x)
                for (int y = 0; y < grid.Height; ++y)
                    tilemapObjectList.Add(grid.GetGridObject(x, y).Save());

            var serializedTilemap = new SerializedTilemap { tilemapObjects = tilemapObjectList.ToArray() };

            SaveSystem.SaveObject(filename, serializedTilemap);
        }

        public virtual void Load(string filename)
        {
            var tilemapSave = SaveSystem.LoadObject<SerializedTilemap>(filename);
            foreach (var tileSave in tilemapSave.tilemapObjects)
            {
                var tile = grid.GetGridObject(tileSave.x, tileSave.y);
                tile.Load(tileSave);
            }
            OnLoaded?.Invoke();
        }

        public void Clear()
        {
            for (int x = 0; x < grid.Width; ++x)
                for (int y = 0; y < grid.Height; ++y)
                    grid.GetGridObject(x, y).SetType(TilemapType.None);
        }
    }
}
