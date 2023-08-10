using System.Collections.Generic;
using UnityEngine;

public delegate void ItemCountChangedEvent(Item item);

public class InventoryScript : MonoBehaviour
{
    public event ItemCountChangedEvent itemCountChangedEvent;

    [SerializeField]
    private BagButton[] _bagButtons;
    [SerializeField]
    private Item[] _items;

    private SlotScript _fromSlot;
    private List<Bag> _bags = new List<Bag>();

    public List<Bag> MyBags => _bags;
    public SlotScript FromSlot
    {
        get
        {
            return _fromSlot;
        }
        set
        {
            _fromSlot = value;
            if (value != null)
            {
                _fromSlot.MyIcon.color = Color.gray;
            }
        }
    }

    private static InventoryScript instance;
    public static InventoryScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }
            return instance;
        }
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;
            foreach (Bag bag in _bags)
            {
                count += bag.MyBagScrtipt.MyEmptySlotsCount;
            }
            return count;
        }
    }
    public int MyCurrentSlotCount
    {
        get
        {
            int count = 0;
            foreach (Bag bag in _bags)
            {
                count += bag.MyBagScrtipt.MySlots.Count;
            }

            return count;
        }
    }
    public int MyFullSlotCount => MyCurrentSlotCount - MyEmptySlotCount;

    public bool CanAddBag
    {
        get
        {
            return _bags.Count < 1;
        }
    }

    private void Awake()
    {
           Bag bag = (Bag)Instantiate(_items[0]);
            bag.Initialize(16);
            bag.Use();
            OpenClose();
    }

    private bool PlaceInStack(Item item)
    {
        foreach (Bag bag in _bags)
        {
            foreach (SlotScript slot in bag.MyBagScrtipt.MySlots)
            {
                if (slot.StackItem(item))
                {
                    OnItemCountChanged(item);
                    return true;
                }
            }
        }
        return false;
    }
    private bool PlaceInEmpty(Item item)
    {
        foreach (Bag bag in _bags)
        {
            if (bag.MyBagScrtipt.AddItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }
        return false;
    }
    public void PlaceInSpecific(Item item,int slotIndex)
    {
        _bags[0].MyBagScrtipt.MySlots[slotIndex].AddItem(item);

    }

    public void AddBag(Bag bag)
    {
        foreach(BagButton bagButton in _bagButtons)
        {
            if(bagButton.MyBag==null)
            {
                bagButton.MyBag = bag;
                _bags.Add(bag);
                bag.MyBagButton = bagButton;
                break;
            }
        }
    }
    public void AddBag(Bag bag , BagButton bagButton)
    {
        _bags.Add(bag);
        bagButton.MyBag = bag;
    }
    public void AddBag(Bag bag,int bagIndex)
    {
        bag.SetupScript();
        MyBags.Add(bag);
        bag.MyBagScrtipt.MyBagIndex = bagIndex;
        bag.MyBagButton = _bagButtons[bagIndex];
        _bagButtons[bagIndex].MyBag = bag;
    }

    public void RemoveBag(Bag bag)
    {
        _bags.Remove(bag);
        Destroy(bag.MyBagScrtipt.gameObject);
    }
    public void SwapBags(Bag newBag,Bag oldBag)
    {
        int newSlotCount = (MyCurrentSlotCount - oldBag.Slots) + newBag.Slots;
        if(newSlotCount - MyFullSlotCount>=0)
        {
            List<Item> bagItems = oldBag.MyBagScrtipt.GetItems();

            RemoveBag(oldBag);
            newBag.MyBagButton = oldBag.MyBagButton;
            newBag.Use();

            foreach (Item item in bagItems)
            {
                if (item != newBag)
                {
                    AddItem(item);
                }
            }

            AddItem(oldBag);
            HandScript.Instance.Drop();
            Instance._fromSlot = null;
        }
    }


    public bool AddItem(Item item)
    {
        if(item.MyStackSize > 0)
        {
            if(PlaceInStack(item))
            {
                return true;
            }
        }
        return PlaceInEmpty(item);
    }

    public Stack<Item> GetItems(string type,int count)
    {
        Stack<Item> items = new Stack<Item>();
        foreach (Bag bag in _bags)
        {
            foreach (SlotScript slot in bag.MyBagScrtipt.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.Title == type)
                {
                    foreach(Item item  in slot.MyItems)
                    {
                        items.Push(item);
                    }
                    if(items.Count == count)
                    {
                        return items;
                    }
                }
            }
        }
        return items;
    }
    public int GetItemCount(string type)
    {
        int itemCount = 0;

        foreach(Bag bag in _bags)
        {
            foreach(SlotScript slot in bag.MyBagScrtipt.MySlots)
            {
                if(!slot.IsEmpty && slot.MyItem.Title == type)
                {
                    itemCount += slot.MyItems.Count;
                }
            }
        }
        return itemCount;
    }
    public List<SlotScript> GetAllItems()
    {
        List<SlotScript> slots = new List<SlotScript>();

        foreach(Bag bag in MyBags)
        {
            foreach(SlotScript slot in bag.MyBagScrtipt.MySlots)
            {
                if(!slot.IsEmpty)
                {
                    slots.Add(slot);
                }
            }
        }
        return slots;
    }
    public Stack<IUseable> GetUseables(IUseable type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        foreach (Bag bag in _bags)
        {
            foreach (SlotScript slot in bag.MyBagScrtipt.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
                {
                    foreach (Item item in slot.MyItems)
                    {
                        useables.Push(item as IUseable);
                    }
                }
            }
        }
        return useables;
    }

    public void OnItemCountChanged(Item item)
    {
        if(itemCountChangedEvent!=null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }

    public void OpenClose()
    {
        bool closedBag = _bags.Find(x => !x.MyBagScrtipt.IsOpen);
        foreach (Bag bag in _bags)
        {
            if (bag.MyBagScrtipt.IsOpen != closedBag)
            {
                bag.MyBagScrtipt.OpenClose();
            }
        }
    }

}
