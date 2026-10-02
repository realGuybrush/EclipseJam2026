using System;
using UnityEngine;

public class Screwdriver : MonoBehaviour
{
    [SerializeField]
    private RectTransform line;
    
    private float decompileTimer, maxTimer, defaultWidth, defaultHeight;

    [SerializeField]
    private int instruments;
    
    public event Action OnFinish = delegate { };

    private void Awake()
    {
        defaultWidth = line.sizeDelta.x;
        defaultHeight = line.sizeDelta.y;
    }

    void Update()
    {
        if (decompileTimer > 0)
        {
            decompileTimer -= Time.deltaTime;
            UpdateLine();
            if (decompileTimer <= 0)
            {
                OnFinish?.Invoke();
                Stop();
            }
        }
    }

    private void UpdateLine()
    {
        line.sizeDelta = new Vector2((1f - decompileTimer / maxTimer) * defaultWidth, defaultHeight);
    }

    public void Upgrade(Tools type)
    {
        instruments += (int)type;
    }

    public void StartDecompiling(float time)
    {
        gameObject.SetActive(true);
        maxTimer = time;
        decompileTimer = maxTimer;
    }

    public void Stop()
    {
        gameObject.SetActive(false);
        decompileTimer = 0;
    }

    public bool Working => decompileTimer > 0;

    public int Instruments => instruments;
}
