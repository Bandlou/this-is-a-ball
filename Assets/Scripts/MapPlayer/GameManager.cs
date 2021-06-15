using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bdl.Utils.Grid;
using Tilemaps;

public class GameManager : MonoBehaviour
{
    // PUBLIC FIELDS
    public Vector2Int mapSize = new Vector2Int(100, 50);
    public GameObject tilemapMainRenderer;
    public GameObject tilemapBackgroundRenderer;

    // PRIVATE FIELDS
    private TilemapMain tilemapMain;
    private TilemapBackground tilemapBackground;

    // LIFECYCLE

    private void Start()
    {
        // Init background tilemap
        tilemapBackground = new TilemapBackground(mapSize.x, mapSize.y);
        tilemapBackground.SetTilemapRenderer(tilemapBackgroundRenderer.GetComponent<TilemapRenderer>());

        // Init main tilemap
        tilemapMain = new TilemapMain(mapSize.x, mapSize.y);
        tilemapMain.SetTilemapRenderer(tilemapMainRenderer.GetComponent<TilemapRenderer>());
        tilemapMain.SetTilemapCollisionRenderer(tilemapMainRenderer.GetComponent<TilemapCollisionRenderer>());

        // Load map
        LoadMap();
    }

    // PRIVATE METHODS

    private void LoadMap()
    {
        // Load background tilemap
        tilemapBackground.Load("salut");

        // Load main tilemap
        tilemapMain.Load("salut");
        tilemapMainRenderer.GetComponent<TilemapCollisionRenderer>().GenerateGeometry();
    }
}
