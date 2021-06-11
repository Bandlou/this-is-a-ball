using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ElectronState : IPlayerState
    {
        public Color GetColor()
        {
            return new Color(0.2f, 0.282353f, 0.6235294f);
        }

        public PhysicsMaterial2D GetPhysicsMaterial()
        {
            return Resources.Load<PhysicsMaterial2D>("Materials/Player/Electron");
        }
    }
}
