using System;
using UnityEngine;

[Serializable]
public class ItemAndChanceCouple
{
    public Item item;
    public float chance;
    [SerializeField]
    private Tools requiredTool;

    public int Tool => Array.IndexOf(Enum.GetValues(requiredTool.GetType()), requiredTool);
}