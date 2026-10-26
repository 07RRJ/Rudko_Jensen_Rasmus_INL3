
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rudko_Jensen_Rasmus_INL3;

public class Cannon{
private const float MoveSpeed = 200f; // up/down speed
private const float RotationSpeed = 1.5f; // amount the barrel rotates
private const float Margin = 40f; // from ui of the applications edge

private readonly Barrel barrel; // the ui part and aiming
private readonly Charge charge; // logic for space

private int screenHeight;

public Cannon(Texture2D barrelTexture, Viewport viewport){
    screenHeight = viewport.Height;

    float cannonY = screenHeight - Margin; // set cannon Y (bottom)
    barrel = new Barrel(barrelTexture, new Vector2(Margin, cannonY));

    charge = new Charge();
}

public void Update(float dT, KeyboardState kb, bool canCharge){
    Vector2 position = barrel.position;

    if (kb.IsKeyDown(Keys.W) || kb.IsKeyDown(Keys.Up)) // up
        position.Y -= MoveSpeed * dT;

    if (kb.IsKeyDown(Keys.S) || kb.IsKeyDown(Keys.Down)) // down
        position.Y += MoveSpeed * dT;

    position.Y = MathHelper.Clamp(position.Y, Margin, screenHeight - Margin); // min/max Y position

    barrel.position = position;

    if (kb.IsKeyDown(Keys.A) || kb.IsKeyDown(Keys.Left)) // left
        barrel.Rotate(-RotationSpeed * dT);

    if (kb.IsKeyDown(Keys.D) || kb.IsKeyDown(Keys.Right)) // right
        barrel.Rotate(RotationSpeed * dT);

    charge.Update(dT, canCharge && kb.IsKeyDown(Keys.Space));
}

public Bullet Fire(){
    if (charge.chargeAmount <= 0f)
        return null; // dont fire if no charge

    Vector2 position = barrel.GetMuzzlePosition();
    Vector2 speed = barrel.GetDirection() * charge.GetLaunchSpeed();

    charge.Reset(); // empty bar

    return new Bullet(position, speed);
}

public void Draw(SpriteBatch spriteBatch, Texture2D pixel) {
    barrel.Draw(spriteBatch);
    charge.Draw(spriteBatch, pixel);
}
}
