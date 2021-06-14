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
        public MapEditorUIController mapEditorUIController;
        public MapEditorCameraController cameraController;

        // PRIVATE FIELDS
        private TilemapMain tilemapMain;
        private TilemapBackground tilemapBackground;

        // LIFECYCLE

        private void Start()
        {
            // Init UI controller
            mapEditorUIController.Locked = true;
            mapEditorUIController.OnNewMapRequest += NewMap;
            mapEditorUIController.OnSaveMapRequest += SaveMap;
            mapEditorUIController.OnLoadMapRequest += LoadMap;
            mapEditorUIController.OnBackToMainMenuRequest += () => Debug.Log("back main menu");
            mapEditorUIController.OnBackToDesktopRequest += () => Debug.Log("desktop");
            mapEditorUIController.OnHideMainLayerRequest += value => tilemapMainRenderer.SetActive(value);
            mapEditorUIController.OnHideBackgroundLayerRequest += value => tilemapBackgroundRenderer.SetActive(value);

            // Init camera
            cameraController.MapSize = mapSize;

            // Init background tilemap
            tilemapBackground = new TilemapBackground(mapSize.x, mapSize.y);
            tilemapBackground.SetTilemapRenderer(tilemapBackgroundRenderer.GetComponent<TilemapRenderer>());

            // Init main tilemap
            tilemapMain = new TilemapMain(mapSize.x, mapSize.y);
            tilemapMain.SetTilemapRenderer(tilemapMainRenderer.GetComponent<TilemapRenderer>());
            tilemapMain.SetTilemapCollisionRenderer(tilemapMainRenderer.GetComponent<TilemapCollisionRenderer>());
        }

        private void Update()
        {
            // UI control
            if (Input.GetKeyDown(KeyCode.Escape))
                mapEditorUIController.ToogleMainMenuVisibility();

            // Map edition
            if (!mapEditorUIController.IsMainMenuActive)
            {
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
        }

        // PRIVATE METHODS

        private void NewMap()
        {
            // Update UI
            mapEditorUIController.Locked = false;
            mapEditorUIController.SetMainMenuVisibility(false);

            // Init background tilemap
            tilemapBackground.Clear();
            tilemapBackground.AddDefault();

            // Init main tilemap
            tilemapMain.Clear();
            tilemapMain.AddBedrock();
        }

        private void SaveMap()
        {
            // Save background tilemap
            tilemapBackground.Save("salut");
            // Save main tilemap
            tilemapMain.Save("salut");
        }

        private void LoadMap()
        {
            // Update UI
            mapEditorUIController.Locked = false;
            mapEditorUIController.SetMainMenuVisibility(false);

            // Load background tilemap
            tilemapBackground.Load("salut");

            // Load main tilemap
            tilemapMain.Load("salut");
        }
    }
}
