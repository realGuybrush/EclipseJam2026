public class HammerShop : Shop
{
    protected override void ActUponPlayer(PlayerController player)
    {
        player.MaxPower();
    }
}