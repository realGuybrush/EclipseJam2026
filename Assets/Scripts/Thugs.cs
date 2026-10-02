using UnityEngine;

public class Thugs : BaseShop
{
    [SerializeField]
    private float debt;
    public override void Spend(PlayerController player)
    {
        player.PayUp(ref debt);
    }
}