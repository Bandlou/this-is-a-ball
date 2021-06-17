using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bdl.Utils.Grid;
using Tilemaps;

public class GameManager : MonoBehaviour
{
    // PUBLIC FIELDS
    public float mapCellSize = 1;
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
        tilemapBackground = new TilemapBackground(mapSize.x, mapSize.y, mapCellSize, Vector3.zero);
        tilemapBackground.SetTilemapRenderer(tilemapBackgroundRenderer.GetComponent<TilemapRenderer2>());

        // Init main tilemap
        tilemapMain = new TilemapMain(mapSize.x, mapSize.y, mapCellSize, Vector3.zero);
        tilemapMain.SetTilemapRenderer(tilemapMainRenderer.GetComponent<TilemapRenderer2>());
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
    }
}
