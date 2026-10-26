using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Rudko_Jensen_Rasmus_INL3;

public class Game1 : Game {
private GraphicsDeviceManager _graphics;
private SpriteBatch _spriteBatch;
private Viewport viewport; // visible ui

private Texture2D texture; // store my barrel img
private Texture2D pixels; // gatherd pixels to reuse

private Cannon cannon; // moving logic
private Bullet bullet; // bullet logic > charge / relese
private Target target; // the target

private bool heldSpace; // if you charged up

private enum GameState{Ready, Flying} // states of bullet
private GameState state = GameState.Ready; // current state

public Game1() { // init
    _graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content"; // set to gather content from "Content" folder
}

protected override void LoadContent(){
    _spriteBatch = new SpriteBatch(GraphicsDevice);
    viewport = GraphicsDevice.Viewport;

    texture = Content.Load<Texture2D>("barrel"); // load barrel img

    pixels = new Texture2D(GraphicsDevice, 1, 1); // reusable to draw with
    pixels.SetData(new[] { Color.White }); // add it to set with default color

    cannon = new Cannon(texture, viewport);
    target = new Target(viewport);
}

protected override void Update(GameTime gameTime){
    KeyboardState kB = Keyboard.GetState(); // take key inputs

    if (kB.IsKeyDown(Keys.Escape)) // if quit, quit
        Exit();

    float dT = (float)gameTime.ElapsedGameTime.TotalSeconds; // delta time

    bool holdingSpace = kB.IsKeyDown(Keys.Space); // if you press space
    bool spaceReleased = heldSpace && !holdingSpace; // if you let go of space

    cannon.Update(dT, kB, state == GameState.Ready); // move and charge when ready

    if (state == GameState.Ready && spaceReleased) { // shoot if ready and relesed
        bullet = cannon.Fire();

        if (bullet != null) { // reset for new shot
            target.Reset();
            state = GameState.Flying;
        }
    }

    else if (state == GameState.Flying && bullet != null) { // update while flying bullet
        bool inBounds = bullet.Update(dT, viewport); // on vissible screen
        bool hitTarget = target.CheckHit(bullet); // bullet in target boundry

        if (hitTarget || !inBounds) { // reset bullet if either is true
            bullet = null;
            state = GameState.Ready;
        }
    }

    heldSpace = holdingSpace; // save space pressed state

    base.Update(gameTime);
}

protected override void Draw(GameTime gameTime){
    GraphicsDevice.Clear(Color.CornflowerBlue);

    _spriteBatch.Begin(); // start things to be drawn

    cannon.Draw(_spriteBatch, pixels); // add cannnon to it
    target.Draw(_spriteBatch, pixels); // the target

    if (bullet != null)
        bullet.Draw(_spriteBatch, pixels); // if bullet add it too screen pixels 

    _spriteBatch.End(); // collect

    base.Draw(gameTime); // draw collected on screen
}
}