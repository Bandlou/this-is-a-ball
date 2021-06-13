using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{
    public class MapEditorCameraController : MonoBehaviour
    {
        // PUBLIC FIELDS
        public float maxOrthographicSize = 20;
        public float minOrthographicSize = 5;
        public float movespeed = .2f;

        // PROPERTIES
        public Vector2 MapSize { get => mapSize; set => mapSize = value; }

        // PRIVATE FIELDS
        private new Camera camera;
        private Vector2 mapSize = new Vector2(40, 20);

        // LIFECYCLE

        private void Start()
        {
            camera = GetComponent<Camera>();
        }

        private void FixedUpdate()
        {
            // Mouse scroll => zoom
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            float clampedSize = Mathf.Clamp(camera.orthographicSize - scroll, minOrthographicSize, maxOrthographicSize);
            camera.orthographicSize = Mathf.Round(clampedSize * 10) * .1f;

            // Arrow keys => movement
            var moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            var nextPosition = camera.transform.position + moveInput * movespeed;
            nextPosition.x = Mathf.Clamp(nextPosition.x, 0, mapSize.x);
            nextPosition.y = Mathf.Clamp(nextPosition.y, 0, mapSize.y);
            camera.transform.position = nextPosition;
        }
    }
}
