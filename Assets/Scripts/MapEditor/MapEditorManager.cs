using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Bdl.Utils.Grid;
using Tilemaps;

namespace MapEditor
{
    public enum MapEditorBrush
    {
        Paint
    }

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
        private TilemapLayer activeLayer = TilemapLayer.Main;
        private MapEditorBrush activeBrush = MapEditorBrush.Paint;
        private int activeBrushTileType = 1;

        // LIFECYCLE

        private void Start()
        {
            // Init UI controller
            mapEditorUIController.SetLayer(activeLayer);
            mapEditorUIController.Locked = true;
            mapEditorUIController.OnNewMapRequest += NewMap;
            mapEditorUIController.OnSaveMapRequest += SaveMap; // TODO: save name in param
            mapEditorUIController.OnLoadMapRequest += LoadMap; // TODO: save name in param
            mapEditorUIController.OnBackToMainMenuRequest += () => Debug.Log("back main menu");
            mapEditorUIController.OnBackToDesktopRequest += () => Application.Quit(); // TODO: check for save
            mapEditorUIController.OnHideMainLayerRequest += value => tilemapMainRenderer.SetActive(value);
            mapEditorUIController.OnHideBackgroundLayerRequest += value => tilemapBackgroundRenderer.SetActive(value);
            mapEditorUIController.OnTileTypeSelected += (layer, type) => { activeLayer = layer; activeBrushTileType = type; };

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
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        GetActiveTilemap().SetTilemapSprite(mouseWorldPosition, activeBrushTileType);
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        GetActiveTilemap().SetTilemapSprite(mouseWorldPosition, 0);
                    }
                }
            }
        }

        // PRIVATE METHODS

        private Tilemap GetActiveTilemap()
        {
            return activeLayer switch
            {
                TilemapLayer.Main => tilemapMain,
                TilemapLayer.Background => tilemapBackground,
                _ => null,
            };
        }

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
