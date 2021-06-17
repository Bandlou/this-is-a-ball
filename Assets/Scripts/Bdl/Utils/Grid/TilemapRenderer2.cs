using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bdl.Utils.Grid
{
    public class TilemapRenderer2 : MonoBehaviour
    {
        // PUBLIC FIELDS
        public int cellTypeCount = 72;
        public int cellTypeRowCount = 9;
        public int cellTypeColumnCount = 8;
        public float drawingAccuracy = .1f;
        public string sortingLayerName = "Default";
        public int sortingOrder = 0;
        public Material material;

        // PRIVATE FIELDS
        private Grid<TilemapObject> grid;
        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;
        private ComputeBuffer gridValuesBuffer;
        private bool updateGridValues;

        // LIFECYCLE

        private void Awake()
        {
            // Init mesh renderer
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.sortingLayerName = sortingLayerName;
            meshRenderer.sortingOrder = sortingOrder;
            
            meshFilter = GetComponent<MeshFilter>();
        }

        private void Start()
        {
            Debug.Log(material);
            // Set material params
            material.SetInt("_CellTypeCount", cellTypeCount);
            material.SetInt("_CellTypeRowCount", cellTypeRowCount);
            material.SetInt("_CellTypeColumnCount", cellTypeColumnCount);
            material.SetFloat("_DrawingAccuracy", drawingAccuracy);
        }

        private void LateUpdate()
        {
            if (updateGridValues)
            {
                updateGridValues = false;
                UpdateMaterialGridValues();
            }
        }

        private void OnDestroy()
        {
            gridValuesBuffer?.Release();
        }

        // PUBLIC METHODS

        public void SetGrid(Grid<TilemapObject> grid, Tilemap tilemap)
        {
            this.grid = grid;
            material.SetInt("_GridWidth", grid.Width);
            material.SetInt("_GridHeight", grid.Height);
            DrawMesh();

            grid.OnGridValueChanged += (x, y) => updateGridValues = true;
            tilemap.OnLoaded += () => updateGridValues = true;
        }

        // PRIVATE METHODS

        private void DrawMesh()
        {
            meshFilter.mesh = new Mesh
            {
                vertices = new Vector3[4]
                {
                    new Vector3(0, 0),
                    new Vector3(0, grid.Height * grid.CellSize),
                    new Vector3(grid.Width * grid.CellSize, 0),
                    new Vector3(grid.Width * grid.CellSize, grid.Height * grid.CellSize)
                },
                uv = new Vector2[4] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) },
                triangles = new int[6] { 0, 1, 2, 2, 1, 3 }
            };
        }

        private void UpdateMaterialGridValues()
        {
            // Get grid values
            var gridValues = new int[grid.Width * grid.Height];
            for (int i = 0; i < gridValues.Length; i++)
                gridValues[i] = grid.GetGridObject(i % grid.Width, i / grid.Width).GetTileType();

            // Set material buffer from grid values
            int stride = System.Runtime.InteropServices.Marshal.SizeOf(typeof(int));
            gridValuesBuffer?.Release();
            gridValuesBuffer = new ComputeBuffer(grid.Width * grid.Height, stride);
            gridValuesBuffer.SetData(gridValues);
            material.SetBuffer("_GridValuesBuffer", gridValuesBuffer);
            Debug.Log(material);
        }
    }
}
