using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Rudko_Jensen_Rasmus_INL3;
public class Game1 : Game{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D barrel;
    private Vector2 barrelPosition;
    private Vector2 barrelCenter;
    private float rotation;
    private float charge;
    Viewport viewport;
    public Game1(){
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize(){
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        barrel = Content.Load<Texture2D>("barrel");

        viewport = _graphics.GraphicsDevice.Viewport;

        barrelCenter.X = barrel.Width / 2;
        barrelCenter.Y = barrel.Height / 2;

        barrelPosition.X = barrel.Width;
        barrelPosition.Y = viewport.Height - barrel.Height;
    }
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
            if (barrelPosition.Y > 0)
                barrelPosition.Y -= elapsed * 100;

        if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
            if (barrelPosition.Y < viewport.Height)
                barrelPosition.Y += elapsed * 100;

        if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            rotation -= elapsed;

        if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            rotation += elapsed;

        if (Keyboard.GetState().IsKeyDown(Keys.Space))
            charge += elapsed;
        else


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime){
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(barrel, barrelPosition, null, Color.White, rotation, barrelCenter, 1.0f, SpriteEffects.None, 0f);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}