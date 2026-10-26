using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Rudko_Jensen_Rasmus_INL3;

public class Barrel {
private readonly Texture2D texture;
public Vector2 position;
public float angle { get; private set; }
private float length => texture.Height; // length = height taken with non static
private Vector2 centerPoint => new Vector2(texture.Width / 2f, texture.Height / 2f);

public Barrel(Texture2D texture, Vector2 position) {
    this.texture = texture;
    this.position = position;
}

public void Rotate(float amount) {
    angle = MathHelper.Clamp(angle + amount, -MathHelper.PiOver2, 0f); // set new current angle between -2^pi and 0
}

public Vector2 GetDirection(){
    return new Vector2(MathF.Cos(angle), MathF.Sin(angle)); // where is it pointing
}

public Vector2 GetMuzzlePosition(){
    return position + GetDirection() * (length / 2f); // where bullet starts
}

public void Draw(SpriteBatch spriteBatch){
    spriteBatch.Draw(texture, position, null, Color.White, angle + MathHelper.PiOver2, centerPoint, 1f, SpriteEffects.None, 0f);
}
}
