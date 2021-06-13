using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class StickyState : IPlayerState
    {
        public float GetJumpForce() => 100;

        public Sprite GetImage() => Resources.Load<Sprite>("Textures/Player/sticky");

        public Color GetColor() => new Color(0.7021412f, 0.8490566f, 0.2763439f);

        public PhysicsMaterial2D GetPhysicsMaterial() => Resources.Load<PhysicsMaterial2D>("PhysicsMaterials/Player/Sticky");
    }
}
