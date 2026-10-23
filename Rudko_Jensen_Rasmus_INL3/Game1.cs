using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Rudko_Jensen_Rasmus_INL3;

public class Game1 : Game {
private readonly GraphicsDeviceManager _graphics; // set graphics
private SpriteBatch _spriteBatch; // default to collect screen items
private Texture2D barrelImg; // my barrel img
private Texture2D pixels; // gatherd pixels to reuse
private Viewport viewport; // visible ui dimentions
private Cannon cannon; // moving logic
private Bullet bullet; // bullet logic > charge / relese
private Target target; // the target
private bool heldSpace; // if you charged up
private enum GameState{Ready, Flying} // states
private GameState state = GameState.Ready; // current state

public Game1() { // init
    _graphics = new GraphicsDeviceManager(this);

    _graphics.PreferredBackBufferWidth = 1024; // some ratio i found on stack
    _graphics.PreferredBackBufferHeight = 768;

    Content.RootDirectory = "Content"; // set content to content
}

protected override void LoadContent(){
    _spriteBatch = new SpriteBatch(GraphicsDevice);
    viewport = GraphicsDevice.Viewport;

    barrelImg = Content.Load<Texture2D>("barrel"); // load barrel

    pixels = new Texture2D(GraphicsDevice, 1, 1); // reusable
    pixels.SetData(new[] { Color.White }); // add it to set with default color

    cannon = new Cannon(barrelImg, viewport);
    target = new Target(viewport);
}

protected override void Update(GameTime gameTime){
    KeyboardState kb = Keyboard.GetState(); // take key inputs

    if (kb.IsKeyDown(Keys.Escape)) // if quit
        Exit();

    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; // delta time

    bool holdingSpace = kb.IsKeyDown(Keys.Space); // charge when no bullet
    bool spaceReleased = heldSpace && !holdingSpace;

    cannon.Update(dt, kb, state == GameState.Ready); // move and charge when ready

    if (state == GameState.Ready && spaceReleased) { // shoot if ready
        bullet = cannon.Fire();

        if (bullet != null) { // reset for new shot
            target.Reset();
            state = GameState.Flying;
        }
    }

    if (state == GameState.Flying && bullet != null) { // update while flying bullet
        bool inBounds = bullet.Update(dt, viewport);
        bool hitTarget = target.CheckHit(bullet);

        if (hitTarget || !inBounds) { // hit target
            bullet = null;
            state = GameState.Ready;
        }
    }

    heldSpace = holdingSpace; // save if held space

    base.Update(gameTime);
}

protected override void Draw(GameTime gameTime){
    GraphicsDevice.Clear(Color.CornflowerBlue);

    _spriteBatch.Begin(); // add things to be drawn

    cannon.Draw(_spriteBatch, pixels);
    target.Draw(_spriteBatch, pixels);

    if (bullet != null)
        bullet.Draw(_spriteBatch, pixels);

    _spriteBatch.End(); // collect

    base.Draw(gameTime); // draw collected on screen
}

protected override void UnloadContent() { // reset pixels
    pixels?.Dispose();
    base.UnloadContent();
}
}