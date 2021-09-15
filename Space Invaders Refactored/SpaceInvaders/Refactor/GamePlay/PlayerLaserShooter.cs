using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Refactor.Core;
using SpaceInvaders.Refactor.Core.Components;

namespace SpaceInvaders.Refactor.GamePlay
{
    public class PlayerLaserShooter : MonoBehaviour
    {
        //Fields
        private readonly Texture2D _laserTexture;
        private readonly float _laserCooldown;

        private float _lastLaserShotFired;

        //Constructor
        public PlayerLaserShooter(Texture2D pLaserTexture, float pLaserCooldown)
        {
            _laserTexture = pLaserTexture;
            _laserCooldown = pLaserCooldown;
        }

        //Copy Constructor-ish
        //public override Component Copy()
        //{
        //    return new PlayerLaserShooter(_game, _laserTexture);
        //}

        public override void Update(GameTime pGameTime)
        {
            // player movement
            KeyboardState state = Keyboard.GetState();

            //Laser shot
            if (state.IsKeyDown(Keys.Space) && pGameTime.TotalGameTime.TotalSeconds > _lastLaserShotFired)
            {
                _lastLaserShotFired = (float)pGameTime.TotalGameTime.TotalSeconds + _laserCooldown;
                GameObject newPlayerLaser = CreatePlayerLaser();
                game.AddGameObject(newPlayerLaser);
            }
        }

        private GameObject CreatePlayerLaser()
        {
            PlayerLaser playerLaser = new PlayerLaser();
            SpriteRenderer laserRenderer = new SpriteRenderer(_laserTexture);
            LaserMovement laserMovement = new LaserMovement(new Vector2(0, -1), 500);
            Collider laserCollider = new Collider(laserRenderer);
            DestroyAfter laserDestroyer = new DestroyAfter(2);

            Vector2 spawnPosition = transform.Position;
            spawnPosition.Y -= laserRenderer.Height;

            GameObject newPlayerLaser = new GameObject(game, "new playerLaser", spawnPosition, new Vector2(0.5f, 1.0f), 0, Vector2.One,
                playerLaser, laserMovement, laserRenderer, laserCollider, laserDestroyer);
            return newPlayerLaser;
        }
    }
}
