using UnityEngine;

public class Shop : BaseShop
{
    [SerializeField]
    private Tools tool;

    [SerializeField]
    private float price;

    public override void Spend(PlayerController player)
    {
        if (player.TryToPay(price))
        {
            ActUponPlayer(player);
            gameObject.SetActive(false);
        }
    }

    protected virtual void ActUponPlayer(PlayerController player)
    {
        player.UpgradeScrewdriver(tool);
    }
}