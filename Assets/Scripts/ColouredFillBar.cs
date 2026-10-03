using UnityEngine;
using UnityEngine.UI;

public class ColouredFillBar : FillBar
{
    [SerializeField]
    private Image sprite;
    
    public override void UpdateLine(float newValue)
    {
        line.sizeDelta = new Vector2((newValue / maxValue) * defaultWidth, defaultHeight);
        sprite.color = NewColor(newValue);
    }

    private Color NewColor(float newValue)
    {
        float colorPointFill = 2f * newValue / maxValue;
        float red = Mathf.Min (colorPointFill, 1f);
        float green = Mathf.Max(0, colorPointFill - 1f);
        return new Color(red, 1f - green, 0, 1f);
    }
}