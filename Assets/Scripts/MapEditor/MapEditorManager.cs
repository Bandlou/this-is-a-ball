using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bdl.Utils.Grid;
using Tilemaps;

namespace MapEditor
{
    public class MapEditorManager : MonoBehaviour
    {
        // PUBLIC FIELDS
        public Vector2Int mapSize = new Vector2Int(100, 50);
        public GameObject tilemapMainRenderer;
        public GameObject tilemapBackgroundRenderer;
        public MapEditorCameraController cameraController;

        // PRIVATE FIELDS
        private TilemapMain tilemapMain;
        private Tilemap tilemapBackground;

        // LIFECYCLE

        private void Start()
        {
            // Init camera
            cameraController.MapSize = mapSize;

            // Init background tilemap
            tilemapBackground = new TilemapMain(mapSize.x, mapSize.y);
            tilemapBackground.SetTilemapRenderer(tilemapBackgroundRenderer.GetComponent<TilemapRenderer>());

            // Init main tilemap
            tilemapMain = new TilemapMain(mapSize.x, mapSize.y);
            tilemapMain.AddBedrock();
            tilemapMain.SetTilemapRenderer(tilemapMainRenderer.GetComponent<TilemapRenderer>());
            tilemapMain.SetTilemapCollisionRenderer(tilemapMainRenderer.GetComponent<TilemapCollisionRenderer>());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
                Save();
            if (Input.GetKeyDown(KeyCode.B))
                Load();

            if (Input.GetMouseButtonDown(0))
            {
                var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tilemapBackground.SetTilemapSprite(mouseWorldPosition, TilemapType.GroundA);
            }
            if (Input.GetMouseButtonDown(1))
            {
                var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tilemapBackground.SetTilemapSprite(mouseWorldPosition, TilemapType.GroundB);
            }
        }

        // PRIVATE METHODS

        private void Save()
        {
            tilemapMain.Save();
        }

        private void Load()
        {
            tilemapMain.Load();
        }
    }
}
