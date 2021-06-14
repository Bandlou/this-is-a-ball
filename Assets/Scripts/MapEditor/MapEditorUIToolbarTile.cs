using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Bdl.Utils.Grid;

namespace MapEditor
{
    public class MapEditorUIToolbarTile : MonoBehaviour, IPointerClickHandler
    {
        // EVENTS
        public delegate void ClickAction(string layer, TilemapType type);
        public event ClickAction OnClicked;

        // PUBLIC FIELDS
        public Image image;

        // PRIVATE FIELDS
        private TilemapType type;
        private string layer;

        // PUBLIC METHODS

        public void SetData(string layer, TilemapType type, Sprite sprite)
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
