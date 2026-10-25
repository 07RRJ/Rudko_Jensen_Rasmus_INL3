
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

private readonly int screenHeight;

public Cannon(Texture2D barrelTexture, Viewport viewport){
    screenHeight = viewport.Height;

    float cannonY = screenHeight - Margin; // set cannon Y (bottom)
    barrel = new Barrel(barrelTexture, new Vector2(Margin, cannonY));

    charge = new Charge();
}

public void Update(float dt, KeyboardState kb, bool canCharge){
    Vector2 position = barrel.position;

    if (kb.IsKeyDown(Keys.W) || kb.IsKeyDown(Keys.Up)) // up
        position.Y -= MoveSpeed * dt;

    if (kb.IsKeyDown(Keys.S) || kb.IsKeyDown(Keys.Down)) // down
        position.Y += MoveSpeed * dt;

    position.Y = MathHelper.Clamp(position.Y, Margin, screenHeight - Margin);

    // X never changes, so the cannon moves vertically only.
    position.X = Margin;
    barrel.position = position;

    // Rotate the barrel independently of the cannon's position.
    if (kb.IsKeyDown(Keys.A) || kb.IsKeyDown(Keys.Left))
        barrel.Rotate(-RotationSpeed * dt);

    if (kb.IsKeyDown(Keys.D) || kb.IsKeyDown(Keys.Right))
        barrel.Rotate(RotationSpeed * dt);

    // Charge only when the game allows another shot.
    charge.Update(dt, canCharge && kb.IsKeyDown(Keys.Space));
}

public Bullet Fire(){
    // Do not fire if the charge bar is empty.
    if (charge.Value <= 0f)
        return null;

    Vector2 position = barrel.GetMuzzlePosition();
    Vector2 velocity = barrel.GetDirection() * charge.GetLaunchSpeed();

    charge.Reset();

    return new Bullet(position, velocity);
}

public void Draw(SpriteBatch spriteBatch, Texture2D pixel) {
    barrel.Draw(spriteBatch);
    charge.Draw(spriteBatch, pixel);
}
}
