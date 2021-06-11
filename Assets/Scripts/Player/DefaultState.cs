using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class DefaultState : IPlayerState
    {
        public Color GetColor()
        {
            return new Color(0.6078432f, 0.6235294f, 0.2f);
        }

        public PhysicsMaterial2D GetPhysicsMaterial()
        {
            return Resources.Load<PhysicsMaterial2D>("Materials/Player/Default");
        }
    }
}
