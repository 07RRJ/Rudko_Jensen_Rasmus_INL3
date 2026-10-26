
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rudko_Jensen_Rasmus_INL3;

public class Charge{
private const float ChargeSpeed = 0.5f;
private const float MinSpeed = 200f;
private const float MaxSpeed = 1500f;

private const int barX = 20;
private const int barY = 20;
private const int barWidth = 200;
private const int barHeight = 20;

private readonly Rectangle grayBarRect; // background bar

public float chargeAmount; // amount of charge %, 0-1f
private int filledWidth; // start with 0 charge 
private Rectangle chargeBarRect; // shows charge amount

public Charge(){
    grayBarRect = new Rectangle(barX, barY, barWidth, barHeight); // set the charge bar
}

public void Update(float dT, bool spaceHeld){
    if (spaceHeld){
        chargeAmount = MathHelper.Min(chargeAmount + ChargeSpeed * dT, 1f); // increase charge within max until it reaches 1

        filledWidth = (int)(barWidth * chargeAmount); // charget to X amount of full
        chargeBarRect = new Rectangle(barX, barY, filledWidth, barHeight);
    }
}
public float GetLaunchSpeed(){
    return MathHelper.Min(MinSpeed + MaxSpeed * chargeAmount, MaxSpeed); // return charge between min/max speed based on charge
}

public void Reset(){
    chargeAmount = 0f; // reset charge
}

public void Draw(SpriteBatch spriteBatch, Texture2D pixel){
    spriteBatch.Draw(pixel, grayBarRect, Color.Gray);

    if (chargeAmount > 0) // if you have charge 
        spriteBatch.Draw(pixel, chargeBarRect, Color.Yellow);
}
}