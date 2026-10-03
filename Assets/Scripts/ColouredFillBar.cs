using UnityEngine;
using UnityEngine.UI;

public class ColouredFillBar : FillBar
{
    const float minRed = 0.64f;
    const float maxRed = 0.79f;
 
    const float minGreen = 0.66f;
    const float maxGreen = 0.27f;
 
    const float minBlue = 0.31f;
    const float maxBlue = 0.15f;
     
    [SerializeField]
    private Image sprite;
    
    public override void UpdateLine(float newValue)
    {
        line.sizeDelta = new Vector2((newValue / maxValue) * defaultWidth, defaultHeight);
        sprite.color = NewColor(newValue);
    }
    
    float lerp(float a, float b, float t)
    {
        return a + t * (b - a);
    }

    private Color NewColor(float newValue)
    {
        float colorPointFill = newValue / maxValue;

        float red = lerp(minRed, maxRed, colorPointFill);
        float green = lerp(minGreen, maxGreen, colorPointFill);
        float blue = lerp(minBlue, maxBlue, colorPointFill);

        return new Color(red, green, blue, 1f);
    }
    
    /*private Color NewColor(float newValue)
    {
        float colorPointFill = 2f * newValue / maxValue;
        float red = Mathf.Min (colorPointFill, 1f);
        float green = Mathf.Max(0, colorPointFill - 1f);
        return new Color(red, 1f - green, 0, 1f);
    }*/
}