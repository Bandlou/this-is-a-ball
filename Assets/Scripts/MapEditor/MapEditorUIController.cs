using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
    public class MapEditorUIController : MonoBehaviour
    {
        // EVENTS
        public delegate void RequestAction();
        public event RequestAction OnNewMapRequest;
        public event RequestAction OnSaveMapRequest;
        public event RequestAction OnLoadMapRequest;
        public event RequestAction OnBackToMainMenuRequest;
        public event RequestAction OnBackToDesktopRequest;
        public delegate void ToogleRequestAction(bool value);
        public event ToogleRequestAction OnHideMainLayerRequest;
        public event ToogleRequestAction OnHideBackgroundLayerRequest;
        public delegate void TileTypeSelectedAction(Tilemaps.TilemapLayer layer, int tileType);
        public event TileTypeSelectedAction OnTileTypeSelected;

        // PUBLIC FIELDS
        public GameObject mainMenu;
        public GameObject toolbar;
        public Button resumeButton;
        public TMPro.TMP_Dropdown layerDropdown;
        public MapEditorUIToolmapTiles toolbarTiles;

        // PROPERTIES
        public bool Locked { get => locked; set => SetLocked(value); }
        public bool IsMainMenuActive { get => mainMenu.activeSelf; }

        // PRIVATE FIELDS
        private bool locked;

        // LIFECYLCE

        private void Awake()
        {
            Locked = false;
            SetMainMenuVisibility(true);

            SetLayer(Tilemaps.TilemapLayer.Main);

            toolbarTiles.OnTileClicked += (layer, type) => OnTileTypeSelected?.Invoke(layer, type);
        }

        // PUBLIC METHODS

        public void ToogleMainMenuVisibility() => SetMainMenuVisibility(!mainMenu.activeSelf);

        public void SetMainMenuVisibility(bool visibility)
        {
            if (!Locked)
            {
                mainMenu.SetActive(visibility);
                toolbar.SetActive(!visibility);
            }
        }

        public void SetLayer(Tilemaps.TilemapLayer layer)
        {
            layerDropdown.value = (int)layer;
            toolbarTiles.SetLayer(layer);
            OnTileTypeSelected?.Invoke(layer, 1);
        }

        // PUBLIC METHODS (EVENTS)

        public void OnResumePressed() => SetMainMenuVisibility(false);

        public void OnNewMapPressed() => OnNewMapRequest?.Invoke();

        public void OnSaveMapPressed() => OnSaveMapRequest?.Invoke();

        public void OnLoadMapPressed() => OnLoadMapRequest?.Invoke();

        public void OnBackToMainMenuPressed() => OnBackToMainMenuRequest?.Invoke();

        public void OnBackToDesktopPressed() => OnBackToDesktopRequest?.Invoke();

        public void OnHideMainLayerToogled(bool value) => OnHideMainLayerRequest?.Invoke(value);

        public void OnHideBackgroundLayerToogled(bool value) => OnHideBackgroundLayerRequest?.Invoke(value);

        public void OnLayerDropdownSelected(int value)
        {
            var layer = (Tilemaps.TilemapLayer)value;
            toolbarTiles.SetLayer(layer);
            OnTileTypeSelected?.Invoke(layer, 1);
        }

        // PRIVATE METHODS

        private void SetLocked(bool locked)
        {
            this.locked = locked;
            resumeButton.interactable = !locked;
        }
    }
}
