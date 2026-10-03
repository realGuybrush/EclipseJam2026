using UnityEngine;

public class FillBar : MonoBehaviour
{
    [SerializeField]
    protected RectTransform line;

    protected float maxValue, defaultWidth, defaultHeight;
    private void Awake()
    {
        defaultWidth = line.sizeDelta.x;
        defaultHeight = line.sizeDelta.y;
    }

    public void Init(float MV)
    {
        maxValue = MV;
    }

    public virtual void UpdateLine(float newValue)
    {
        line.sizeDelta = new Vector2((1f - newValue / maxValue) * defaultWidth, defaultHeight);
    }
}
