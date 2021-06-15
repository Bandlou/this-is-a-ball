using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bdl.Utils.Grid
{
    public abstract class TilemapObject : GridObject
    {
        // SERIALIZABLE CLASS
        [System.Serializable]
        public class SerializedTilemapObject
        {
            public int type;
            public int x;
            public int y;
        }

        // PRIVATE FIELDS
        protected Grid<TilemapObject> grid;
        protected int type;

        // CONSTRUCTOR

        public TilemapObject(Grid<TilemapObject> grid, int x, int y)
            : base(x, y)
        {
            this.grid = grid;
        }

        // PUBLIC METHODS

        public int GetTileType() => type;

        public void SetType(int type)
        {
            this.type = type;
            grid.TriggerGridObjectChanged(x, y);
        }

        public SerializedTilemapObject Save()
        {
            return new SerializedTilemapObject
            {
                type = this.type,
                x = this.x,
                y = this.y
            };
        }

        public void Load(SerializedTilemapObject tilemapObjectSave)
        {
            type = (int)tilemapObjectSave.type;
        }
    }
}
