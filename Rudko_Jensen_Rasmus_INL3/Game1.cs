using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rudko_Jensen_Rasmus_INL3;

public class Game1 : Game{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D barrel;
    private int x = 0, y = 400, charge = 0, rotation = 0;
    private Vector2 spritePosition;
    private Vector2 spriteOrigin;
    public Game1(){
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize(){
        base.Initialize();
    }

    protected override void LoadContent(){
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        barrel = Content.Load<Texture2D>("barrel");

        Viewport viewport = _graphics.GraphicsDevice.Viewport;
    }

    protected override void Update(GameTime gameTime){
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
            if (y > 0)
                y--;

        if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
            if (y < 400)
                y++;

        if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            rotation--;

        if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D))
            rotation++;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime){
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(barrel, new Rectangle(x, y, 50, 100), Color.White, );

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}