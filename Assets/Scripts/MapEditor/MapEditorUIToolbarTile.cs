using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MapEditor
{
    public class MapEditorUIToolbarTile : MonoBehaviour, IPointerClickHandler
    {
        // EVENTS
        public delegate void ClickAction(Tilemaps.TilemapLayer layer, int type);
        public event ClickAction OnClicked;

        // PUBLIC FIELDS
        public Image image;

        // PRIVATE FIELDS
        private int type;
        private Tilemaps.TilemapLayer layer;

        // PUBLIC METHODS

        public void SetData(Tilemaps.TilemapLayer layer, int type, Sprite sprite)
        {
            this.layer = layer;
            this.type = type;
            image.sprite = sprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(layer, type);
        }
    }
}
