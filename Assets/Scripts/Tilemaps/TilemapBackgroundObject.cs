using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bdl.Utils.Grid;

namespace Tilemaps
{
    public enum BackgroundTileType
    {
        None, BGTile1, BGTile2, BGTile3, BGTile4, BGTile5, BGTile6, BGTile7, BGTile8, BGTile9, BGTile10,
        BGTile11, BGTile12, BGTile13, BGTile14, BGTile15, BGTile16, BGTile17, BGTile18, BGTile19, BGTile20,
        BGTile21, BGTile22, BGTile23, BGTile24, BGTile25, BGTile26, BGTile27, BGTile28, BGTile29, BGTile30,
        BGTile31, BGTile32, BGTile33, BGTile34, BGTile35, BGTile36, BGTile37, BGTile38, BGTile39, BGTile40,
        BGTile41, BGTile42, BGTile43, BGTile44, BaseGrayA, BGTile46, BGTile47, BGTile48, BGTile49, BGTile50,
        BGTile51, BGTile52, BGTile53, BaseGrayB, BGTile55, BGTile56, BGTile57, BGTile58, BGTile59, BGTile60,
        BGTile61, BGTile62, BGTile63, BGTile64, BGTile65, BGTile66, BGTile67, BGTile68, BGTile69, BGTile70,
        BGTile71, BGTile72
    }

    public class TilemapBackgroundObject : TilemapObject
    {
        // CONSTRUCTOR

        public TilemapBackgroundObject(Grid<TilemapObject> grid, int x, int y)
            : base(grid, x, y) { }
    }
}
