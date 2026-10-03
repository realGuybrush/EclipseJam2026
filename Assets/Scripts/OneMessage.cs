using System.Collections;
using TMPro;
using UnityEngine;

public class OneMessage : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;

    [SerializeField]
    private TextMeshProUGUI text;
    
    public void ShowMessage(Vector3 newPos, string newText, Color newColor)
    {
        gameObject.SetActive(true);
        transform.position = Camera.main.WorldToScreenPoint(newPos);
        body.linearVelocity = 20f * Vector3.up;
        text.text = newText;
        text.color = newColor;
        StartCoroutine("TurnOff");
    }

    private IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(1.5f);
        body.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
