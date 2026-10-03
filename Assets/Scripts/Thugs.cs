using System.Collections;
using TMPro;
using UnityEngine;

public class Thugs : BaseShop
{
    [SerializeField]
    private ColouredFillBar DebtOMatic;

    [SerializeField]
    private TextMeshProUGUI text;
    
    [SerializeField]
    private float debt, maxDebt, beingLateMultiplier, debtIncreasePeriod;

    private bool waiting; 

    private void Start()
    {
        DebtOMatic.Init(maxDebt);
        UpdateUI();
        waiting = false;
    }

    private void Update()
    {
        if (!waiting)
        {
            StartCoroutine("UpdateDebt");
        }
    }

    private IEnumerator UpdateDebt()
    {
        waiting = true;
        yield return new WaitForSeconds(debtIncreasePeriod);
        debt *= beingLateMultiplier;
        UpdateUI();
        waiting = false;
    }

    public override void Spend(PlayerController player)
    {
        player.PayUp(ref debt);
        UpdateUI();
    }

    private void UpdateUI()
    {
        text.text = Mathf.Round(debt) + " $";
        DebtOMatic.UpdateLine(debt);
    }
}