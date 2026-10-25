
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rudko_Jensen_Rasmus_INL3;

public class Bullet{
private const float Gravity = 500f;
private const float Radius = 8f;
private Vector2 velocity;
public Vector2 Position { get; private set; }

public Bullet(Vector2 position, Vector2 velocity){
    Position = position;
    this.velocity = velocity;
}

// Move the bullet and return whether it is still on screen.
public bool Update(float dt, Viewport viewport){
    velocity.Y += Gravity * dt;
    Position += velocity * dt;

    return Position.X >= 0 && Position.X <= viewport.Width && Position.Y >= 0 && Position.Y <= viewport.Height;
}

// Check whether the circular bullet overlaps a rectangle.
public bool Intersects(Rectangle rectangle){
    float closestX = MathHelper.Clamp(Position.X, rectangle.Left, rectangle.Right);

    float closestY = MathHelper.Clamp(Position.Y, rectangle.Top, rectangle.Bottom);

    float dx = Position.X - closestX;
    float dy = Position.Y - closestY;

    return dx * dx + dy * dy < Radius * Radius;
}

public void Draw(SpriteBatch spriteBatch, Texture2D pixel){
    int diameter = (int)(Radius * 2);

    spriteBatch.Draw(
        pixel,
        new Rectangle(
            (int)(Position.X - Radius),
            (int)(Position.Y - Radius),
            diameter,
            diameter),
        Color.Black);
}
}
