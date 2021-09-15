using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Refactor.Core;
using SpaceInvaders.Refactor.Core.Components;

namespace SpaceInvaders.Refactor.GamePlay
{
    public class AlienWaveBehaviour : MonoBehaviour
    {
        //Fields - Constructor/Configuration
        private readonly RefactoredGame _game;
        private readonly Texture2D _alienTexture;
        private readonly Texture2D _alienLaserTexture;
        private readonly int _numberOfRows;
        private readonly int _numberOfColumns;
        private readonly float _stepDuration;

        private const int _moveSteps = 4;

        //Tracking
        private readonly List<GameObject> _aliens = new List<GameObject>();
        private float _previousStepTime;
        private int _alienStepCount;

        //Calculated
        private readonly int _horizontalStepSize;
        private readonly int _verticalStepSize;
        private readonly int _spacing;

        public AlienWaveBehaviour(RefactoredGame pGame, Texture2D pAlienTexture, Texture2D pAlienLaserTexture, Viewport pViewport, int pNumberOfRows, int pNumberOfColumns, float pStepDuration)
        {
            //Store all configuration values
            _game = pGame;
            _alienTexture = pAlienTexture;
            _alienLaserTexture = pAlienLaserTexture;

            _numberOfRows = pNumberOfRows;
            _numberOfColumns = pNumberOfColumns;
            _stepDuration = pStepDuration;

            //Calculate remaining values
            int totalHorizontalMoveDistance = (pViewport.Width / _moveSteps);
            _horizontalStepSize = totalHorizontalMoveDistance / _moveSteps;
            _verticalStepSize = (pViewport.Height / 20);

            int combinedSpacingWidth = pViewport.Width - (pNumberOfColumns * pAlienTexture.Width) - totalHorizontalMoveDistance;
            _spacing = combinedSpacingWidth / (pNumberOfColumns + 1);//Possible loss of precision due to rounding to an int (Floor)

            //Create aliens
            _alienStepCount = _moveSteps / 2;
            CreateAliens(pNumberOfRows * pNumberOfColumns);
            _previousStepTime = _stepDuration;
        }

        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds > _previousStepTime)
            {
                _previousStepTime = (float)gameTime.TotalGameTime.TotalSeconds + _stepDuration;
                PositionAliens();
                _alienStepCount++;
            }
        }

        private void PositionAliens()
        {
            int alienWidth = _alienTexture.Width;
            int alienHeight = _alienTexture.Height;

            int horizontalMoveStep = _alienStepCount % (_moveSteps + 1);
            int verticalMoveStep = _alienStepCount / (_moveSteps + 1);

            if(horizontalMoveStep == 0 || horizontalMoveStep == _moveSteps) 
                _alienStepCount++;

            if (verticalMoveStep % 2 != 0) 
                horizontalMoveStep = _moveSteps - horizontalMoveStep;

            int horizontalOffset = horizontalMoveStep * _horizontalStepSize;
            int verticalOffset = verticalMoveStep * _verticalStepSize;

            for (int i = 0; i < _aliens.Count; i++)
            {
                Vector2 newPosition = new Vector2(
                    _spacing + horizontalOffset + (i % _numberOfColumns) * (_spacing + alienWidth) + alienWidth / 2,
                    _spacing + verticalOffset + (i / _numberOfColumns) * (_spacing + alienHeight) + alienHeight / 2
                    );

                _aliens[i].transform.SetPosition(newPosition);
            }
        }

        private void CreateAliens(int pNumberOfAliens)
        {
            for (int i = 0; i < pNumberOfAliens; i++)
            {
                GameObject newAlien = CreateAlien();
                _aliens.Add(newAlien);
                _game.AddGameObject(newAlien);
            }

            PositionAliens();
        }

        private GameObject CreateAlien()
        {
            Alien alien = new Alien();
            //OneHitDeath oneHitDeath = new OneHitDeath();
            SpriteRenderer alienRenderer = new SpriteRenderer(_alienTexture, Color.White, 0.4f);
            AlienLaserShooter alienLaserShooter = new AlienLaserShooter(_alienLaserTexture, 6, 10);
            Collider alienCollider = new Collider(alienRenderer);

            return new GameObject(_game, "Alien", alien, alienRenderer, alienLaserShooter, alienCollider);
        }
    }
}

