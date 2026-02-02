using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimpleAnimationNamespace;

namespace Assignment_01;


public class Game1 : Game
{
    Texture2D texture;
    Texture2D texture2;
    Texture2D backgroundTexture;
    Texture2D ballTexture;
    Texture2D staticImage; 
    SpriteFont testfont;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    int counter = 0;
    int frameWidth = 205;
    int frameWidth2 = 186;
    int frameHeight = 208;
    int frameHeight2 = 125;
    int activeFrame = 0;

    Vector2 ballPosition = new Vector2(200, 200);
    Vector2 staticPosition = new Vector2(100, 50);
    float ballVelocity = 305f;
        public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        texture = Content.Load<Texture2D>("walking");
        texture2 = Content.Load<Texture2D>("running");
        backgroundTexture = Content.Load<Texture2D>("pixelforest");
        ballTexture = Content.Load<Texture2D>("ball");
        staticImage = Content.Load<Texture2D>("nonowa");

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        KeyboardState kState = Keyboard.GetState();
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Vector2 moveDir = Vector2.Zero;

        if (kState.IsKeyDown(Keys.W) || kState.IsKeyDown(Keys.Up)) moveDir.Y -= 1;
        if (kState.IsKeyDown(Keys.A) || kState.IsKeyDown(Keys.Left)) moveDir.X -= 1;
        if (kState.IsKeyDown(Keys.S) || kState.IsKeyDown(Keys.Down)) moveDir.Y += 1;
        if (kState.IsKeyDown(Keys.D) || kState.IsKeyDown(Keys.Right)) moveDir.X += 1;

        if (moveDir != Vector2.Zero)
        {
            moveDir.Normalize();
            ballPosition += moveDir * ballVelocity * dt;
        }

        
       counter++;

       if (counter > 30)
        {
           activeFrame++;
           counter = 0; 
            if (activeFrame  >=  6)
        {
            activeFrame = 0;
        }
        base.Update(gameTime);
        }       
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        int x = activeFrame  * frameWidth;

        Rectangle sourceRect = new Rectangle(x, 0, frameWidth, frameHeight);
        Rectangle sourceRect2 = new Rectangle(x, 10, frameWidth2, frameHeight2);
        _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.Wheat);
        _spriteBatch.Draw(staticImage, staticPosition, Color.Bisque);
        _spriteBatch.Draw(texture, new  Vector2(200,300), sourceRect, Color.Bisque);
        _spriteBatch.Draw(texture2, new  Vector2(400,300), sourceRect2, Color.Bisque);
        _spriteBatch.Draw(ballTexture,ballPosition, Color.Bisque);
        _spriteBatch.End();

        base.Draw(gameTime);

    }
}
