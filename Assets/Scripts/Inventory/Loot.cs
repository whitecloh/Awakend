using UnityEngine;

[System.Serializable]
public class Loot
{
    [SerializeField]
    private Item _item;

    [SerializeField]
    private float _dropChance;

    public Item MyItem => _item;
    public float MyDropChance => _dropChance;
}
