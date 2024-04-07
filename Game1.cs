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

	private int xVelocity;
	private int xSpeed;
	
	// Aggregates of all acceleration forces
	
	private int yVelocity;
	private int yAccelerations; 
	private int jumpForce;
	private int gravity;
	
	private KeyboardState keyboardState;
	private bool isGrounded;
	private int deltaTime;

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
		playerTexture = new Texture2D(GraphicsDevice, 1,1);
		playerTexture.SetData<Color>(new Color[] { Color.White });
		xSpeed = 10;
		xVelocity = 0;
		yVelocity = 0;
		jumpForce = 40;
		gravity = 2;
		yAccelerations = 0;
		isGrounded = false;

		base.Initialize();
	}

	protected override void LoadContent()
	{
		_spriteBatch = new SpriteBatch(GraphicsDevice);

		// TODO: use this.Content to load your game content here
	}

	protected override void Update(GameTime gameTime)
	{
		
		if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
			|| Keyboard.GetState().IsKeyDown(Keys.Escape))
			Exit();
		
		deltaTime = (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
		
		// Velocity for the next frame calculation is
		// dependent on player input and constant forces
		// (Y axis gravity for now, X axis drag planned)
		keyboardState = Keyboard.GetState();
		
		xVelocity = 0;
		if (keyboardState.IsKeyDown(Keys.A)) 
		{
			xVelocity -= xSpeed;
		}
		
		if (keyboardState.IsKeyDown(Keys.D))
		{
			xVelocity += xSpeed;
		}
		
		if (keyboardState.IsKeyDown(Keys.Space) && isGrounded)
		{
			yAccelerations -= jumpForce;
			isGrounded = false;
		}
		
		yAccelerations += gravity;
		yVelocity = yAccelerations * deltaTime;
		
		// Position for next frame is given by 
		// current position + velocity * delta time
		player.X += xVelocity * deltaTime;
		player.Y += yVelocity * deltaTime;
		
		// Screen bounds check
		if (player.Right > _graphics.PreferredBackBufferWidth)
		{
			player.X = _graphics.PreferredBackBufferWidth - player.Width;
		}
		if (player.Left < 0)
		{
			player.X = 0;
		}
		
		if (player.Bottom > _graphics.PreferredBackBufferHeight)
		{
			player.Y = _graphics.PreferredBackBufferHeight - player.Height;
			isGrounded = true;
			yVelocity = 0;
			yAccelerations = 0;
		}

		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		GraphicsDevice.Clear(Color.CornflowerBlue);

		// TODO: Add your drawing code here
		_spriteBatch.Begin();
		_spriteBatch.Draw(playerTexture,player,Color.White);
		_spriteBatch.End();
		base.Draw(gameTime);
	}
}
