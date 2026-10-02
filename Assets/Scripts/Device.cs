using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Device : MonoBehaviour
{
    [SerializeField]
    private Health health;

    [SerializeField]
    private List<ItemAndChanceCouple> drops;

    [SerializeField]
    private float dropDistance = 1.5f, decompileTime = 5f;

    private void Start()
    {
        health.OnDead += GetDestroyed;
    }

    private void GetDestroyed()
    {
        Drop((int)Tools.Hammer);
    }

    public void Drop(int tools)
    {
        foreach(var drop in drops)
            if( IsBitActive(tools, drop.Tool) && Random.Range(0, 100) < drop.chance)
                Instantiate(drop.item, transform.position + DropCoords(), transform.rotation);
        Destroy(gameObject);
    }

    private bool IsBitActive(int number, int bitIndex)
    {
        return (number & (1 << bitIndex)) != 0;
    }

    private Vector3 DropCoords()
    {
        float angle = Mathf.Deg2Rad * Random.Range(0f, 360f);
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * dropDistance;
    }

    private void OnDestroy()
    {
        health.OnDead -= GetDestroyed;
    }

    public float DecompileTime => decompileTime;
}
