using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Rudko_Jensen_Rasmus_INL3;

public class Barrel {
    private readonly Texture2D texture;
    public Vector2 Position { get; set; }
    public float Angle { get; private set; }
    public float Length => texture.Height; // height = length set with non static

    public Barrel(Texture2D texture, Vector2 position){
        this.texture = texture;
        Position = position;
    }

    // Rotate between pointing left and pointing right.
    public void Rotate(float amount) {
        Angle = MathHelper.Clamp(Angle + amount, -MathHelper.PiOver2, 0f); // current angle between 90-0
    }

    // Calculate the direction in which the barrel points.
    public Vector2 GetDirection()
    {
        return new Vector2(
            MathF.Cos(Angle),
            MathF.Sin(Angle));
    }

    // Calculate the bullet's starting position at the barrel tip.
    public Vector2 GetMuzzlePosition()
    {
        return Position + GetDirection() * (Length / 2f);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            texture,
            Position,
            null,
            Color.White,
            Angle + MathHelper.PiOver2,
            new Vector2(texture.Width / 2f, texture.Height / 2f),
            1f,
            SpriteEffects.None,
            0f);
    }
}
