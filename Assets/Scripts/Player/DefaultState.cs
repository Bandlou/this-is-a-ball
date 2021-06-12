using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class DefaultState : IPlayerState
    {
        public float GetJumpForce() => 200;

        public Sprite GetImage() => Resources.Load<Sprite>("Textures/Player/default");

        public Color GetColor() => new Color(0.8207547f, 0.7998322f, 0.367791f);

        public PhysicsMaterial2D GetPhysicsMaterial() => Resources.Load<PhysicsMaterial2D>("Materials/Player/Physic/Default");
    }
}
