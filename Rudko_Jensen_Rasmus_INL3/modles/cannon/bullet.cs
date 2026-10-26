
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rudko_Jensen_Rasmus_INL3;

public class Bullet{
private const float Gravity = 500f;
// private const float AirResistance = 1f;
private const float Radius = 8f;
private const int Diameter = 16;
private Rectangle rect;
private Vector2 speed;
public Vector2 position;

public Bullet(Vector2 position, Vector2 speed){
    this.position = position;
    this.speed = speed;

    rect = new Rectangle((int)(position.X - Radius), (int)(position.Y - Radius), Diameter, Diameter);
}

public bool Update(float dT, Viewport viewport){ 
    speed.Y += Gravity * dT; // increase Y
    if (speed.X > 10)
        speed.X -= 50 * dT; 
    position += speed * dT; // increase position based on speed which has been effected by gravity

    rect.X = (int)(position.X - Radius);
    rect.Y = (int)(position.Y - Radius);

    return position.Y <= viewport.Height && position.X <= viewport.Width; // returnn if on screen or not (the bullet cant move back so no need for that, also shooting up and letting it fall down is okay)
}

public bool Intersects(Rectangle rectangle){
    float closestX = MathHelper.Clamp(position.X, rectangle.Left, rectangle.Right); // take either bullet X inside of target or target < X >
    float closestY = MathHelper.Clamp(position.Y, rectangle.Top, rectangle.Bottom); // same but for Y

    float dx = position.X - closestX; // actual x disstance, it is negative if it is not far enough
    float dy = position.Y - closestY; // same for y

    return dx * dx + dy * dy < Radius * Radius; // use version of pythagoras to see if bullet overlaps
}

public void Draw(SpriteBatch spriteBatch, Texture2D pixel){
    spriteBatch.Draw(pixel, rect, Color.Black);
}
}
