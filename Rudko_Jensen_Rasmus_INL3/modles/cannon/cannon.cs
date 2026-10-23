
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rudko_Jensen_Rasmus_INL3;

public class Cannon
{
    private const float MoveSpeed = 200f;
    private const float RotateSpeed = 1.5f;
    private const float Margin = 40f;

    private readonly Barrel barrel;
    private readonly Charge charge;

    private readonly int screenHeight;
    private readonly float cannonX;

    public Cannon(Texture2D barrelTexture, Viewport viewport)
    {
        screenHeight = viewport.Height;

        // Keep the cannon near the left edge.
        cannonX = Margin;

        // Start near the bottom of the screen.
        float cannonY = screenHeight - Margin;

        barrel = new Barrel(
            barrelTexture,
            new Vector2(cannonX, cannonY));

        charge = new Charge();
    }

    public void Update(float dt, KeyboardState kb, bool canCharge)
    {
        Vector2 position = barrel.Position;

        // Move only along the Y-axis.
        if (kb.IsKeyDown(Keys.W) || kb.IsKeyDown(Keys.Up))
            position.Y -= MoveSpeed * dt;

        if (kb.IsKeyDown(Keys.S) || kb.IsKeyDown(Keys.Down))
            position.Y += MoveSpeed * dt;

        // Keep the cannon on screen.
        position.Y = MathHelper.Clamp(
            position.Y,
            Margin,
            screenHeight - Margin);

        // X never changes, so the cannon moves vertically only.
        position.X = cannonX;
        barrel.Position = position;

        // Rotate the barrel independently of the cannon's position.
        if (kb.IsKeyDown(Keys.A) || kb.IsKeyDown(Keys.Left))
            barrel.Rotate(-RotateSpeed * dt);

        if (kb.IsKeyDown(Keys.D) || kb.IsKeyDown(Keys.Right))
            barrel.Rotate(RotateSpeed * dt);

        // Charge only when the game allows another shot.
        charge.Update(
            dt,
            canCharge && kb.IsKeyDown(Keys.Space));
    }

    public Bullet Fire()
    {
        // Do not fire if the charge bar is empty.
        if (charge.Value <= 0f)
            return null;

        Vector2 position = barrel.GetMuzzlePosition();
        Vector2 velocity = barrel.GetDirection() * charge.GetLaunchSpeed();

        charge.Reset();

        return new Bullet(position, velocity);
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
    {
        barrel.Draw(spriteBatch);
        charge.Draw(spriteBatch, pixel);
    }
}
