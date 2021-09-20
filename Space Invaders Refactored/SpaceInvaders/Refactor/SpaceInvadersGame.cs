using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Refactor;
using SpaceInvaders.Refactor.Core;
using SpaceInvaders.Refactor.Core.Components;
using SpaceInvaders.Refactor.GamePlay;

namespace SpaceInvaders.Refactor
{
}

public class SpaceInvadersGame : RefactoredGameBase
{
    //Fields
    private GameObject _background;
    private GameObject _player;
    private GameObject _alienWave1;

    //Constructor
    protected override void LoadContent()
    {
        base.LoadContent();

        //Basic graphical initialization
        Viewport viewport = GraphicsDevice.Viewport;

        //Background setup
        SpriteRenderer backgroundRenderer = new SpriteRenderer("background", Content, Color.White, 1.0f);
        _background = new GameObject(this, "background", Vector2.Zero, Vector2.Zero, backgroundRenderer);
        AddGameObject(_background);


        //Player setup
        Player playerBehaviour = new Player();
        SpriteRenderer playerRenderer = new SpriteRenderer("player", Content, Color.White, 0.5f);
        Collider playerCollider = new Collider(playerRenderer);
        PlayerMovement playerMovement = new PlayerMovement(5, viewport, playerRenderer.Width);

        Texture2D playerLaserTexture = Content.Load<Texture2D>("laser2");
        PlayerLaserShooter playerLaserShooter = new PlayerLaserShooter(playerLaserTexture, 0.5f);
        Vector2 playerSpawnPosition = new Vector2(viewport.Width / 2, viewport.Height - playerRenderer.Height);
        _player = new GameObject(this, "player", playerSpawnPosition, new Vector2(0.5f, 0.5f), 0, Vector2.One, playerBehaviour, playerCollider, playerRenderer, playerMovement, playerLaserShooter);
        AddGameObject(_player);


        //AlienWave1 setup
        Texture2D alienTexture = Content.Load<Texture2D>("alien1");
        Texture2D alienLaserTexture = Content.Load<Texture2D>("laser1");
        AlienWaveBehaviour alienWaveBehaviour = new AlienWaveBehaviour(this, alienTexture, alienLaserTexture, viewport, 3, 10, 1.5f);
        _alienWave1 = new GameObject(this, "Alien Wave 1", alienWaveBehaviour);
        AddGameObject(_alienWave1);
    }
}


