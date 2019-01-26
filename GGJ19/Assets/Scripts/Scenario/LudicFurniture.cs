using UnityEngine;

public class LudicFurniture : LudicItem
{
    public SpriteRenderer baseSprite;
    public SpriteRenderer ludicSprite;
    private Color baseColor = new Color(1, 1, 1, 1);
    private Color ludicColor = new Color(1, 1, 1, 1);

    protected override void SetupItem()
    {
        SetupLudic(1f);
    }

    protected override void UpdateLudicMeter(float ludicMeter)
    {
        SetupLudic(ludicMeter);
    }

    private void SetupLudic(float ludicMeter)
    {
        baseColor.a = 1 - ludicMeter;
        ludicColor.a = ludicMeter;
        baseSprite.color = baseColor;
        ludicSprite.color = ludicColor;
    }
}