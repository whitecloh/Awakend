using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    public IMoveable MyMoveable { get; set; }
    private static HandScript instance;
    private Image _icon;

    private Vector3 _offset;

    private void Start()
    {
        _offset = new Vector3(35, 10, 0);
        _icon = GetComponent<Image>();
    }
    private void Update()
    {
        _icon.transform.position = Input.mousePosition + _offset ;
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && Instance.MyMoveable != null)
        {
            DeleteItem();
        }
    }
    public static HandScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }
            return instance;
        }
    }
    public void TakeMoveable(IMoveable moveable)
    {
        this.MyMoveable = moveable;
        _icon.sprite = moveable.MyIcon;
        _icon.enabled=true;
    }
    public IMoveable Put()
    {
        IMoveable tmp = MyMoveable;
        MyMoveable = null;
        _icon.enabled=false;
        return tmp;
    }
    public void Drop()
    {
        MyMoveable = null;
        _icon.enabled=false;
        InventoryScript.Instance.FromSlot = null;
    }
    public void DeleteItem()
    {

        if (MyMoveable is Item && MyMoveable is not QuestItem)
        {
            Item item = (Item)MyMoveable;

            if (item.MySlot!=null)
            {
                item.MySlot.Clear();
            }
            else if(item.CharButton!=null)
            {
                item.CharButton.DequipArmor();
            }
        }
        Drop();
        InventoryScript.Instance.FromSlot = null;
    }
}
