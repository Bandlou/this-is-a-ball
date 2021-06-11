using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public interface IPlayerState
    {
        public Color GetColor();

        public PhysicsMaterial2D GetPhysicsMaterial();
    }
}
