using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        // CONST
        private const float MaxHorizontalLinearForce = 10;
        private const float MaxVerticalLinearForce = 10;

        // PROPERTIES
        public IPlayerState CurrentState { get => currentState; set => SetCurrentState(value); }
        public float Q { get => currentState is MetalState ? q : 0; }

        // PRIVATE FIELDS
        private Rigidbody2D rigidBody;
        private new CircleCollider2D collider;
        private SpriteRenderer spriteRenderer;
        private IPlayerState currentState;
        private float q = 0;

        // LIFE CYCLE

        void Start()
        {
            // Get components
            rigidBody = GetComponent<Rigidbody2D>();
            collider = GetComponent<CircleCollider2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            // Initialize state
            CurrentState = new DefaultState();
            q = -1;
        }

        void FixedUpdate()
        {
            // State player inputs
            if (Input.GetKey(KeyCode.J))
                CurrentState = new DefaultState();
            else if (Input.GetKey(KeyCode.L))
                CurrentState = new StickyState();
            else if (Input.GetKey(KeyCode.M))
                CurrentState = new MetalState();
            else if (Input.GetKey(KeyCode.I))
                CurrentState = new IceState();

            // Movement player inputs
            float horizontalLinearForce = Input.GetAxis("Horizontal") * MaxHorizontalLinearForce;
            float verticalLinearForce = Mathf.Clamp(Input.GetAxis("Vertical"), -1, 0) * MaxVerticalLinearForce;
            rigidBody.AddForce(new Vector2(horizontalLinearForce, verticalLinearForce));
        }

        // PUBLIC METHODS

        public void AddForce(Vector2 force)
        {
            rigidBody.AddForce(force);
        }

        public void OnCollisionEnter2D(Collision2D collision) => CheckJump(collision);

        public void OnCollisionStay2D(Collision2D collision) => CheckJump(collision);

        // PRIVATE METHODS

        private void CheckJump(Collision2D collision)
        {
            if (Input.GetKey(KeyCode.Space) && collision.contactCount > 0)
                rigidBody.AddForce(collision.contacts[0].normal * currentState.GetJumpForce());
        }

        private void SetCurrentState(IPlayerState state)
        {
            currentState = state;
            collider.sharedMaterial = state.GetPhysicsMaterial();
            spriteRenderer.sprite = state.GetImage();
            spriteRenderer.color = state.GetColor();
        }
    }
}
