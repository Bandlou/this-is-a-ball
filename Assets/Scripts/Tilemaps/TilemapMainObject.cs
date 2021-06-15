using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bdl.Utils.Grid;

namespace Tilemaps
{
    public enum MainTileType
    {
        None, MainTileA, MainTileB, MainTileC
    }

    public class TilemapMainObject : TilemapObject
    {
        // CONSTRUCTOR

        public TilemapMainObject(Grid<TilemapObject> grid, int x, int y)
            : base(grid, x, y) { }
    }
}
