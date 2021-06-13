using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class IceState : IPlayerState
    {
        public float GetJumpForce() => 150;

        public Sprite GetImage() => Resources.Load<Sprite>("Textures/Player/ice");

        public Color GetColor() => new Color(1, 1, 1);

        public PhysicsMaterial2D GetPhysicsMaterial() => Resources.Load<PhysicsMaterial2D>("PhysicsMaterials/Player/Ice");
    }
}
