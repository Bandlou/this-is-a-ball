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
        private const float JumpForce = 200;

        // PROPERTIES
        public IPlayerState CurrentState { get => currentState; set => SetCurrentState(value); }

        // PRIVATE FIELDS
        private Rigidbody2D rb;
        private CircleCollider2D collider;
        private SpriteRenderer sr;
        private IPlayerState currentState;

        // LIFE CYCLE

        void Start()
        {
            // Get components
            rb = GetComponent<Rigidbody2D>();
            collider = GetComponent<CircleCollider2D>();
            sr = GetComponent<SpriteRenderer>();

            // Initialize state
            CurrentState = new DefaultState();
        }

        void FixedUpdate()
        {
            // State player inputs
            if (Input.GetKey(KeyCode.E))
                CurrentState = new DefaultState();
            else if (Input.GetKey(KeyCode.Q))
                CurrentState = new StickyState();
            else if (Input.GetKey(KeyCode.F))
                CurrentState = new ElectronState();

            // Handle player inputs
            float horizontalLinearForce = Input.GetAxis("Horizontal") * MaxHorizontalLinearForce;
            float verticalLinearForce = Mathf.Clamp(Input.GetAxis("Vertical"), -1, 0) * MaxVerticalLinearForce;
            rb.AddForce(new Vector2(horizontalLinearForce, verticalLinearForce));
        }

        // PUBLIC METHODS

        public void OnCollisionEnter2D(Collision2D collision) => CheckJump(collision);

        public void OnCollisionStay2D(Collision2D collision) => CheckJump(collision);

        // PRIVATE METHODS

        private void CheckJump(Collision2D collision)
        {
            if (Input.GetKey(KeyCode.Space) && collision.contactCount > 0)
                rb.AddForce(collision.contacts[0].normal * JumpForce);
        }

        private void SetCurrentState(IPlayerState state)
        {
            currentState = state;
            collider.sharedMaterial = state.GetPhysicsMaterial();
            sr.color = state.GetColor();
        }
    }
}
