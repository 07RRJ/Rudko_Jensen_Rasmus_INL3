
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rudko_Jensen_Rasmus_INL3;

public class Target{
public Rectangle rect;
public bool IsHit;

public Target(Viewport viewport){
    rect = new Rectangle(viewport.Width - 80, viewport.Height - 120, 60, 80);
}

public bool CheckHit(Bullet bullet){
    if (IsHit || !bullet.Intersects(rect)) // if not 
        return false;

    IsHit = true;
    return true;
}

public void Reset(){
    IsHit = false;
}

public void Draw(SpriteBatch spriteBatch, Texture2D pixel){
    Color color = IsHit ? Color.Lime : Color.Red;
    spriteBatch.Draw(pixel, rect, color);
}
}
