using System;
using UnityEngine;

public class Screwdriver : MonoBehaviour
{

    [SerializeField]
    private FillBar fillBar;
    
    private float decompileTimer, maxTimer;

    [SerializeField]
    private int instruments;
    
    public event Action OnFinish = delegate { };

    void Update()
    {
        if (decompileTimer > 0)
        {
            decompileTimer -= Time.deltaTime;
            fillBar.UpdateLine(decompileTimer);
            if (decompileTimer <= 0)
            {
                OnFinish?.Invoke();
                Stop();
            }
        }
    }

    public void Upgrade(Tools type)
    {
        instruments += (int)type;
    }

    public void StartDecompiling(float time)
    {
        gameObject.SetActive(true);
        maxTimer = time;
        fillBar.Init(maxTimer);
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
