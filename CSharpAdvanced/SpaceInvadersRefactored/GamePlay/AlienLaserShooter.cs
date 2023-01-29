using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SpaceInvadersRefactored.Components;


namespace SpaceInvadersRefactored.GamePlay
{
    public class AlienLaserShooter : MonoBehaviour
    {
        //Fields
        private readonly Texture2D _laserTexture;
        private readonly float _minLaserCooldown;
        private readonly float _maxLaserCooldown;
        private readonly float _randomRange;

        private float _lastLaserShotFired;
        private readonly Random _random;

        //Constructor
        public AlienLaserShooter(Texture2D pLaserTexture, float pMinLaserCooldown, float pMaxLaserCooldown)
        {
            _laserTexture = pLaserTexture;
            _minLaserCooldown = pMinLaserCooldown;
            _maxLaserCooldown = pMaxLaserCooldown;
            _randomRange = pMaxLaserCooldown - pMinLaserCooldown;

            _random = new Random();

            _lastLaserShotFired = (float)_random.NextDouble() * (pMinLaserCooldown - 1) + 1;
        }

        //Methods
        public override void Update(GameTime pGameTime)
        {
            if (pGameTime.TotalGameTime.TotalSeconds > _lastLaserShotFired)
            {
                UpdateLastLaserShotFired();

                GameObject alienLaser = CreateAlienLaser();
                game.AddGameObject(alienLaser);
            }
        }

        private void UpdateLastLaserShotFired()
        {
            _lastLaserShotFired += (float)_random.NextDouble() * _randomRange + _minLaserCooldown;
        }

        private GameObject CreateAlienLaser()
        {
            AlienLaser alienLaser = new AlienLaser();
            SpriteRenderer laserRenderer = new SpriteRenderer(_laserTexture);
            Collider laserCollider = new Collider(laserRenderer);
            LaserMovement laserMovement = new LaserMovement(new Vector2(0, 1), 250);
            DestroyAfter laserDestroyer = new DestroyAfter(2);
            GameObject newPlayerLaser = new GameObject(game, "new AlienLaser", transform.Position, new Vector2(0.5f, 0.0f), 0, Vector2.One, 
                alienLaser, laserRenderer, laserCollider, laserMovement, laserDestroyer);
            return newPlayerLaser;
        }
    }
}
