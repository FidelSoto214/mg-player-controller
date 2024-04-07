using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Movement;

public class Game1 : Game
{
	private GraphicsDeviceManager _graphics;
	private SpriteBatch _spriteBatch;
	private Rectangle player;
	private Texture2D playerTexture;

	private int xVelocity, yVelocity;
	private int xSpeed, ySpeed;
	private int xAcceleration;
	private int yAcceleration;
	private int jumpForce;
	private KeyboardState keyboardState;
	private int gravity;
	private bool grounded;

	public Game1()
	{
		_graphics = new GraphicsDeviceManager(this);
		Content.RootDirectory = "Content";
		IsMouseVisible = true;

	}

	protected override void Initialize()
	{
		// TODO: Add your initialization logic here
		player = new Rectangle(_graphics.PreferredBackBufferWidth / 2,
								_graphics.PreferredBackBufferHeight / 2,
								40,
								40);
		playerTexture = new Texture2D(GraphicsDevice, 1, 1);
		playerTexture.SetData<Color>(new Color[] { Color.White });
		xSpeed = 10;
		ySpeed = 10;
		xVelocity = 0;
		yVelocity = 0;
		jumpForce = 30;
		gravity = 1;
		xAcceleration = 3;
		yAcceleration = 3;
		grounded = false;

		base.Initialize();
	}

	protected override void LoadContent()
	{
		_spriteBatch = new SpriteBatch(GraphicsDevice);

		// TODO: use this.Content to load your game content here
	}

	protected override void Update(GameTime gameTime)
	{
		if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			Exit();

		// TODO: Add your update logic here
		
		
		// P_[t+1] = P_[t] + V[t] * t
		player.X += xVelocity * (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
		player.Y += yVelocity * (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
		
		// V_[t+1] = V_[t] + A[t] * t
		// TODO: not fully there yet, but we'll get there. Still need to implement this
		// part correctly. 
		keyboardState = Keyboard.GetState();
		if (keyboardState.IsKeyDown(Keys.A)) { xVelocity = keyboardState.IsKeyDown(Keys.D) ? 0 : -xSpeed; }
		else if (keyboardState.IsKeyDown(Keys.D)) { xVelocity = keyboardState.IsKeyDown(Keys.A) ? 0: xSpeed; }
		else { xVelocity = 0; }
		if (player.Right > _graphics.PreferredBackBufferWidth)
		{
			player.X = _graphics.PreferredBackBufferWidth - player.Width;
		}
		if (player.Left < 0)
		{
			player.X = 0;
		}
		
		if (keyboardState.IsKeyDown(Keys.Space) && grounded)  
		{
			// player.Y -= jumpForce * (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
			player.Y -= jumpForce;
			yVelocity = -10;
			grounded = false;
		}
		if (!grounded)
		{
			// player.Y += gravity * (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
			yVelocity += gravity;
		}
			

		
		if (player.Bottom > _graphics.PreferredBackBufferHeight)
		{
			player.Y = _graphics.PreferredBackBufferHeight - player.Height;
			grounded = true;
			yVelocity = 0;
		}

		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		GraphicsDevice.Clear(Color.CornflowerBlue);

		// TODO: Add your drawing code here
		_spriteBatch.Begin();
		_spriteBatch.Draw(playerTexture, player, Color.White);
		_spriteBatch.End();
		base.Draw(gameTime);
	}
}
