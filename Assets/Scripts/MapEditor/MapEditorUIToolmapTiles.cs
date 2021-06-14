using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bdl.Utils.Grid;

namespace MapEditor
{
    public class MapEditorUIToolmapTiles : MonoBehaviour
    {
        // EVENTS
        public delegate void TileClickAction(string layer, TilemapType type);
        public event TileClickAction OnTileClicked;

        // PUBLIC FIELDS
        public GameObject tilePrefab;

        // PUBLIC METHODS

        public void SetLayer(string layer)
        {
            UpdateGrid(layer);
        }

        // PRIVATE METHODS

        private void UpdateGrid(string layer)
        {
            // Clear
            foreach (Transform child in transform)
                GameObject.Destroy(child.gameObject);

            // Populate
            var sprites = Resources.LoadAll<Sprite>("Textures/Tiles/TilesetBase/" + layer);
            foreach (var sprite in sprites)
            {
                if (!sprite.name.Contains("_n_"))
                {
                    var tile = Instantiate(tilePrefab, transform);
                    var tileScript = tile.GetComponent<MapEditorUIToolbarTile>();
                    tileScript.SetData(layer, Bdl.Utils.Grid.TilemapType.GroundA, sprite);
                    tileScript.OnClicked += (layer, type) => OnTileClicked?.Invoke(layer, type);
                }
            }
        }
    }
}
