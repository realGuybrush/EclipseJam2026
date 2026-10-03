using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private float price;

    [SerializeField]
    private Rigidbody2D body;

    private float dropTime;

    public void Init(Vector3 newCoord)
    {
        body.linearVelocity = 2f * new Vector2(newCoord.x - transform.position.x, newCoord.y - transform.position.y + 2f);
        StartCoroutine("WaitForFall", newCoord);
    }

    private IEnumerator WaitForFall(Vector3 newCoord)
    {
        yield return new WaitForSeconds(0.1f);
        body.linearVelocity = new Vector2(body.linearVelocity.x, 2f * (newCoord.y - transform.position.y));
        StartCoroutine("WaitForStop");
    }

    private IEnumerator WaitForStop()
    {
        yield return new WaitForSeconds(0.1f);
        body.linearVelocity = Vector2.zero;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null) return;
        player.GetMoney(price);
        MessageManager.Instance.ShowText(price + "$", Color.darkGreen);
        Destroy(gameObject);
    }
}
