using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bdl.Utils.Grid
{
    public class TilemapRenderer : MonoBehaviour
    {
        // STRUCT
        private struct UVCoords
        {
            public Vector2 uv00;
            public Vector2 uv11;
        }

        // PUBLIC FIELDS
        public int cellSize = 32;
        public int tileTypeCount = 1;
        public string sortingLayerName = "Default";
        public int sortingOrder = 0;

        // PRIVATE FIELDS
        private Grid<TilemapObject> grid;
        private Mesh mesh;
        private MeshRenderer meshRenderer;
        private bool updateMesh;
        private Dictionary<int, UVCoords> uvCoodsDictionary;

        // LIFECYCLE

        private void Awake()
        {
            // Init mesh filter
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

            // Init mesh renderer
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.sortingLayerName = sortingLayerName;
            meshRenderer.sortingOrder = sortingOrder;

            // Build UV coords from mesh renderer's texture
            var texture = (Texture2D)meshRenderer.material.mainTexture;
            uvCoodsDictionary = new Dictionary<int, UVCoords>();

            int y = 0, tilemapTypeIndex = 1; // start at index = 1 to skip => TilemapType.None = 0
            while (y < texture.height / cellSize && tilemapTypeIndex < tileTypeCount)
            {
                int x = 0;
                while (x < texture.width / cellSize && tilemapTypeIndex < tileTypeCount)
                {
                    var uvCoords = new UVCoords()
                    {
                        uv00 = new Vector2(
                                x * cellSize / (float)texture.width,
                                (texture.height - (y + 1) * cellSize) / (float)texture.height),
                        uv11 = new Vector2(
                                (x + 1) * cellSize / (float)texture.width,
                                (texture.height - y * cellSize) / (float)texture.height)
                    };
                    uvCoodsDictionary.Add(tilemapTypeIndex++, uvCoords);

                    ++x;
                }
                ++y;
            }
        }

        private void LateUpdate()
        {
            if (updateMesh)
            {
                updateMesh = false;
                UpdateVisual();
            }
        }

        // PUBLIC METHODS

        public void SetGrid(Grid<TilemapObject> grid, Tilemap tilemap)
        {
            this.grid = grid;
            UpdateVisual();

            grid.OnGridValueChanged += (x, y) => updateMesh = true;
            tilemap.OnLoaded += () => updateMesh = true;
        }

        // PRIVATE METHODS

        private void UpdateVisual()
        {
            MeshUtils.CreateEmptyMeshArrays(grid.Width * grid.Height,
                                            out Vector3[] verticles,
                                            out Vector2[] uv,
                                            out int[] trianges);

            for (int x = 0; x < grid.Width; ++x)
            {
                for (int y = 0; y < grid.Height; ++y)
                {
                    int index = x * grid.Height + y;
                    Vector3 quadSize = new Vector3(1, 1) * grid.CellSize;

                    var tilemapObject = grid.GetGridObject(x, y);
                    var tilemapType = tilemapObject.GetTileType();

                    Vector2 gridValueUV00, gridValueUV11;
                    if (tilemapType <= 0)
                    {
                        gridValueUV00 = Vector2.zero;
                        gridValueUV11 = Vector2.zero;
                        quadSize = Vector3.zero;
                    }
                    else
                    {
                        var uvCoords = uvCoodsDictionary[tilemapType];
                        gridValueUV00 = uvCoords.uv00;
                        gridValueUV11 = uvCoords.uv11;
                    }

                    MeshUtils.AddToMeshArrays(verticles,
                                              uv,
                                              trianges,
                                              index,
                                              grid.GetWorldPosition(x, y) + quadSize * .5f,
                                              0,
                                              quadSize,
                                              gridValueUV00,
                                              gridValueUV11);
                }
            }

            mesh.vertices = verticles;
            mesh.uv = uv;
            mesh.triangles = trianges;
        }
    }
}
