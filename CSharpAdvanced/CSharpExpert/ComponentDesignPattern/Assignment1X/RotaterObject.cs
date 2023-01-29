﻿using Microsoft.Xna.Framework;

namespace CSharpAdvanced.CSharpExpert.ComponentDesignPattern.Assignment1X
{
    public class RotaterObject : GameObject
    {
        //Fields - configurable
        private readonly float _rotationSpeed;

        //Constructor
        public RotaterObject(string pName, Transform pTransform, SpriteRenderer pRenderer, float pRotationSpeed) : base(pName, pTransform, pRenderer)
        {
            _rotationSpeed = pRotationSpeed;
        }

        //Methods
        public override void UpdateGameObject(GameTime pGameTime)
        {
            Transform.Rotation += _rotationSpeed;
        }
    }
}