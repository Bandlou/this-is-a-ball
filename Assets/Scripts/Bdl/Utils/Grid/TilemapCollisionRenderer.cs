using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bdl.Utils.Grid
{
    /// <summary>
    /// Add collisions for each active tiles
    /// </summary>
    public class TilemapCollisionRenderer : MonoBehaviour
    {
        // PUBLIC FIELDS
        public PhysicsMaterial2D physicsMaterial2D;

        // PRIVATE FIELDS
        private Grid<TilemapObject> grid;
        private GameObject colliders;
        private Rigidbody2D rigidBody;
        private CompositeCollider2D compositeCollider;
        private bool updateColliders;

        // LIFECYCLE

        private void Awake()
        {
            colliders = new GameObject("TilemapColliders");
            colliders.transform.parent = gameObject.transform;

            rigidBody = colliders.AddComponent<Rigidbody2D>();
            rigidBody.bodyType = RigidbodyType2D.Static;

            compositeCollider = colliders.AddComponent<CompositeCollider2D>();
            compositeCollider.sharedMaterial = physicsMaterial2D;
            compositeCollider.generationType = CompositeCollider2D.GenerationType.Manual;
            compositeCollider.vertexDistance = .25f;
        }

        private void LateUpdate()
        {
            if (updateColliders)
            {
                updateColliders = false;
                UpdateColliders();
            }
        }

        // PUBLIC METHODS

        public void SetGrid(Grid<TilemapObject> grid, Tilemap tilemap)
        {
            this.grid = grid;
            UpdateColliders();
            GenerateGeometry();

            grid.OnGridValueChanged += (x, y) => updateColliders = true;
            tilemap.OnLoaded += () => updateColliders = true;
        }

        public void GenerateGeometry() => compositeCollider.GenerateGeometry();

        // PRIVATE METHODS

        private void UpdateColliders()
        {
            for (int x = 0; x < grid.Width; ++x)
            {
                for (int y = 0; y < grid.Height; ++y)
                {
                    Vector3 quadSize = new Vector3(1, 1) * grid.CellSize;

                    var tilemapObject = grid.GetGridObject(x, y);
                    var tilemapSprite = tilemapObject.GetTilemapType();

                    if (tilemapSprite != TilemapType.None)
                    {
                        var tileCollider = colliders.AddComponent<BoxCollider2D>();
                        tileCollider.usedByComposite = true;
                        tileCollider.size = quadSize;
                        tileCollider.offset = grid.GetWorldPosition(x, y) + quadSize * .5f;
                    }
                }
            }
        }
    }
}
