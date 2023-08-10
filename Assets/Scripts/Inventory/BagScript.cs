using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _slotPrefab;

    private CanvasGroup _canvasGroup;

    private List<SlotScript> _slots = new List<SlotScript>(); 

    public List<SlotScript> MySlots => _slots;

    public bool IsOpen => _canvasGroup.alpha > 0;

    public int MyEmptySlotsCount
    {
        get
        {
            int count = 0;

            foreach (SlotScript slots in MySlots)
            {
                if (slots.IsEmpty)
                {
                    count++;
                }
            }
            return count;
        }
    }

    public int MyBagIndex { get; set; }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public bool AddItem(Item item)
    {
        foreach(SlotScript slot in _slots )
        {
            if(slot.IsEmpty)
            {
                slot.AddItem(item);
                return true;
            }
        }
        return false;
    }

    public void AddSlots(int slotCount)
    {
        for(int i=0;i<slotCount;i++)
        {
            SlotScript slot = Instantiate(_slotPrefab, transform).GetComponent<SlotScript>();
            slot.MyIndex = i;
            slot.MyBag = this;
            _slots.Add(slot);
        }
    }

    public List<Item> GetItems()
    {
        List<Item> items = new List<Item>();
        foreach (SlotScript slot in _slots)
        {
            if (!slot.IsEmpty)
            {
                foreach (Item item in slot.MyItems)
                {
                    items.Add(item);
                }
            }
        }
        return items;
    }

    public void OpenClose()
    {
        _canvasGroup.alpha = _canvasGroup.alpha > 0 ? 0 : 1;
        _canvasGroup.blocksRaycasts = _canvasGroup.blocksRaycasts == true ? false : true;
    }

}
