using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace MonoGame_UnityFramework
{
    public class Rotator : Component
    {
        private float _rotationSpeed;

        public Rotator(float pRotationSpeed)
        {
            _rotationSpeed = pRotationSpeed;
        }

        public override void Update(GameTime pGameTime)
        {
            transform.Rotate(_rotationSpeed * (float)pGameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
