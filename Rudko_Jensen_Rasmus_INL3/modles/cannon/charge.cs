
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rudko_Jensen_Rasmus_INL3;

public class Charge{
private const float ChargeSpeed = 0.5f;
private const float MinSpeed = 200f;
private const float MaxSpeed = 800f;

public float Value { get; private set; }

// Increase charge while Space is held, up to 100%.
public void Update(float dt, bool spaceHeld){
    if (spaceHeld)
        Value = MathHelper.Min(Value + ChargeSpeed * dt, 1f);
}

// Convert the charge percentage into bullet speed.
public float GetLaunchSpeed(){
    return MathHelper.Lerp(MinSpeed, MaxSpeed, Value);
}

public void Reset(){
    Value = 0f;
}

public void Draw(SpriteBatch spriteBatch, Texture2D pixel){
    const int barWidth = 200;
    const int barHeight = 20;
    const int barX = 20;
    const int barY = 20;

    // Draw the empty bar.
    spriteBatch.Draw(pixel, new Rectangle(barX, barY, barWidth, barHeight), Color.Gray);

    // Fill the bar according to the current charge.
    int filledWidth = (int)(barWidth * Value);

    if (filledWidth > 0){
        spriteBatch.Draw(pixel, new Rectangle(barX, barY, filledWidth, barHeight), Color.Yellow);
    }
}
}
