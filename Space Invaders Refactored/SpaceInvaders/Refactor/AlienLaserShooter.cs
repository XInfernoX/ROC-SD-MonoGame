using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Refactor
{
    public class AlienLaserShooter : MonoBehaviour
    {
        //Fields
        private Texture2D _laserTexture;
        private float _minLaserCooldown;
        private float _maxLaserCooldown;
        private float _randomRange;

        private float _lastLaserShotFired;
        private Random _random;

        //Constructor
        public AlienLaserShooter(Texture2D pLaserTexture, float pMinLaserCooldown, float pMaxLaserCooldown)
        {
            _laserTexture = pLaserTexture;
            _minLaserCooldown = pMinLaserCooldown;
            _maxLaserCooldown = pMaxLaserCooldown;
            _randomRange = pMaxLaserCooldown - pMinLaserCooldown;

            _random = new Random();

            _lastLaserShotFired = (float)_random.NextDouble() * (pMinLaserCooldown - 1) + 1;
            //UpdateLastLaserShotFired();
        }

        //Methods
        public override void Update(GameTime pGameTime)
        {
            if (pGameTime.TotalGameTime.TotalSeconds > _lastLaserShotFired)
            {
                UpdateLastLaserShotFired();

                GameObject alienlaser = CreateAlienLaser();
                game.AddGameObject(alienlaser);
            }
        }

        private void UpdateLastLaserShotFired()
        {
            _lastLaserShotFired += (float)_random.NextDouble() * _randomRange + _minLaserCooldown;

        }

        private GameObject CreateAlienLaser()
        {
            SpriteRenderer laserRenderer = new SpriteRenderer(_laserTexture);
            Collider laserCollider = new Collider(laserRenderer);
            LaserMovement laserMovement = new LaserMovement(new Vector2(0, 1), 300);
            DestroyAfter laserDestroyer = new DestroyAfter(2);
            GameObject newPlayerLaser = new GameObject(game, "new playerLaser", transform.Position, new Vector2(0.5f, 0.0f), 0, Vector2.One, 
                laserRenderer, laserCollider, laserMovement, laserDestroyer);
            return newPlayerLaser;
        }
    }
}
