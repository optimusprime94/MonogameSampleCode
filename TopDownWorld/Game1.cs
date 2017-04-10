using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownWorld
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D mapTexture;
        private Texture2D personTexture;

        // The color data for the textures
        // (used for per-pixel collision detection)
        private Color[] personTextureData;
        private Color[] mapTextureData;

        // Person 
        private Vector2 personPosition;
        private Vector2 mapPosition;
        private Rectangle personBoundingRectangle;
        private Rectangle mapBoundingRectangle;
        private const int personMoveSpeed = 5;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load textures
            mapTexture = Content.Load<Texture2D>("map1");
            personTexture = Content.Load<Texture2D>("person");

            // Extract collision data (for per-pixel collision detection)
            mapTextureData = new Color[mapTexture.Width * mapTexture.Height];
            mapTexture.GetData(mapTextureData);
            personTextureData = new Color[personTexture.Width * personTexture.Height];
            personTexture.GetData(personTextureData);

            // Start the player in the center along the bottom of the screen
            personPosition.X = (GraphicsDevice.Viewport.Width - personTexture.Width) / 2;
            personPosition.Y = GraphicsDevice.Viewport.Height - personTexture.Height;

            mapPosition.X = (GraphicsDevice.Viewport.Width - mapTexture.Width) / 2;
            mapPosition.Y = GraphicsDevice.Viewport.Height - mapTexture.Height;

            // Calculate the bounding rectangle of the person
            personBoundingRectangle = new Rectangle(
                (int)personPosition.X, (int)personPosition.Y,
                personTexture.Width, personTexture.Height);

            // Calculate the bounding rectangle of the person
            mapBoundingRectangle = new Rectangle(
                (int)mapPosition.X, (int)mapPosition.Y,
                personTexture.Width, personTexture.Height);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Get input
            KeyboardState keyboard = Keyboard.GetState();
            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);

            // Move player with arrow keys or d-pad
            if (keyboard.IsKeyDown(Keys.Left) || gamePad.DPad.Left == ButtonState.Pressed)
                personPosition.X -= personMoveSpeed;
            if (keyboard.IsKeyDown(Keys.Right) || gamePad.DPad.Right == ButtonState.Pressed)
                personPosition.X += personMoveSpeed;
            if (keyboard.IsKeyDown(Keys.Up) || gamePad.DPad.Up == ButtonState.Pressed)
                personPosition.Y -= personMoveSpeed;
            if (keyboard.IsKeyDown(Keys.Down) || gamePad.DPad.Down == ButtonState.Pressed)
                personPosition.Y += personMoveSpeed;

            // Prevent the person from moving off of the screen
            personPosition.X = MathHelper.Clamp(personPosition.X, graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Width - personTexture.Width);
            personPosition.Y = MathHelper.Clamp(personPosition.Y, graphics.GraphicsDevice.Viewport.Y, graphics.GraphicsDevice.Viewport.Height - personTexture.Height);

            // Update the person's bounding rectangle
            personBoundingRectangle.X = (int)personPosition.X;
            personBoundingRectangle.Y = (int)personPosition.Y;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            // Draw person
            spriteBatch.Draw(personTexture, personPosition, Color.White);
            spriteBatch.Draw(mapTexture, mapPosition, Color.White);
            spriteBatch.Draw(mapTexture, mapPosition, null, mapBoundingRectangle, Color.White, 0f, Vector2.Zero, 0,5f, SpriteEffects.None, 0f);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
