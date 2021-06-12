using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    // PUBLIC FIELDS
    [SerializeField] float parallaxEffectMultiplier = .5f;

    // PRIVATE FIELDS
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private Vector2 textureUnitSize;

    // LIFECYCLE

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;

        var sprite = GetComponent<SpriteRenderer>().sprite;
        var texture = sprite.texture;
        textureUnitSize = new Vector2(texture.width / sprite.pixelsPerUnit, texture.height / sprite.pixelsPerUnit);
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += deltaMovement * parallaxEffectMultiplier;
        lastCameraPosition = cameraTransform.position;

        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSize.x)
        {
            float offset = (cameraTransform.position.x - transform.position.x) % textureUnitSize.x;
            transform.position = new Vector2(cameraTransform.position.x + offset, transform.position.y);
        }
        if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSize.y)
        {
            float offset = (cameraTransform.position.y - transform.position.y) % textureUnitSize.y;
            transform.position = new Vector2(transform.position.x, cameraTransform.position.y);
        }
    }
}
