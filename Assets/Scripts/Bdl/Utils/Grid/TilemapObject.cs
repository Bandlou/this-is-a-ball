using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bdl.Utils.Grid
{
    public enum TilemapType
    {
        None, GroundA, GroundB
    }

    public class TilemapObject : GridObject
    {
        // SERIALIZABLE CLASS
        [System.Serializable]
        public class SerializedTilemapObject
        {
            public TilemapType tilemapSprite;
            public int x;
            public int y;
        }

        // PRIVATE FIELDS
        protected Grid<TilemapObject> grid;
        private TilemapType type;

        // CONSTRUCTOR

        public TilemapObject(Grid<TilemapObject> grid, int x, int y)
            : base(x, y)
        {
            this.grid = grid;
        }

        // PUBLIC METHODS

        public TilemapType GetTilemapType() => type;

        public void SetType(TilemapType type)
        {
            this.type = type;
            grid.TriggerGridObjectChanged(x, y);
        }

        public SerializedTilemapObject Save()
        {
            return new SerializedTilemapObject
            {
                tilemapSprite = this.type,
                x = this.x,
                y = this.y
            };
        }

        public void Load(SerializedTilemapObject tilemapObjectSave)
        {
            type = tilemapObjectSave.tilemapSprite;
        }
    }
}
