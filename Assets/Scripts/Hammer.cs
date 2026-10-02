using System.Collections;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField]
    private float defaultDamage, frenzyMultiplier, frenzyTime;

    private float damage;

    private void Awake()
    {
        damage = defaultDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var device = other.GetComponent<Health>();
        if (device == null) return;
        device.TakeDamage(damage);
    }

    public void StartFrenzy()
    {
        StartCoroutine("Frenzy");
    }

    private IEnumerator Frenzy()
    {
        damage = defaultDamage * frenzyMultiplier;
        yield return new WaitForSeconds(frenzyTime);
        damage = defaultDamage;
    }

    public float Damage { get => defaultDamage;
        set => defaultDamage = value;
    }
}
