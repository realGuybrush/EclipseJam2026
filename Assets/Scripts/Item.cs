using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private float price;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null) return;
        player.GetMoney(price);
        Destroy(gameObject);
    }
}
