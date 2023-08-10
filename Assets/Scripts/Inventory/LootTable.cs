using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    [SerializeField]
    private Loot[] _loot;

    public List<Item> _droppedItems = new List<Item>();

    private bool _rolled =false;


    public void ShowLoot()
    {
        if (!_rolled)
        {
            RollLoot();
        }
        LootWindow.Instance.CreatePages(_droppedItems);
    }
    private void RollLoot()
    {
        foreach(Loot item in _loot )
        {
            int roll = Random.Range(0, 100);

            if(roll<=item.MyDropChance)
            {
                _droppedItems.Add(item.MyItem);
            }
        }
        _rolled = true;
    }

    public void Interact()
    {
        ShowLoot();
    }
}
