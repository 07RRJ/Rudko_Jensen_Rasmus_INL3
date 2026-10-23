
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rudko_Jensen_Rasmus_INL3;

public class Target
{
    public Rectangle Bounds { get; }
    public bool IsHit { get; private set; }

    public Target(Viewport viewport)
    {
        Bounds = new Rectangle(
            viewport.Width - 80,
            viewport.Height - 120,
            60,
            80);
    }

    // Return true only when a new hit occurs.
    public bool CheckHit(Bullet bullet)
    {
        if (IsHit || !bullet.Intersects(Bounds))
            return false;

        IsHit = true;
        return true;
    }

    public void Reset()
    {
        IsHit = false;
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
    {
        Color color = IsHit ? Color.Lime : Color.Red;
        spriteBatch.Draw(pixel, Bounds, color);
    }
}
