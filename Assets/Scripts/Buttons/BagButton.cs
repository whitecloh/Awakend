using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagButton : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private Sprite _full, _empty;

    private Bag _bag;

    public Bag  MyBag
    {
        get
        {
            return _bag;
        }
        set
        {
            if(value!=null)
            {
                GetComponent <Image>().sprite = _full;
            }
            else
            {
                GetComponent<Image>().sprite = _empty;
            }
            _bag = value;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(InventoryScript.Instance.FromSlot != null && HandScript.Instance.MyMoveable!=null && HandScript.Instance.MyMoveable is Bag)
            {
                if(MyBag!=null)
                {
                    InventoryScript.Instance.SwapBags(MyBag, HandScript.Instance.MyMoveable as Bag);
                }
                else
                {
                    Bag tmp = (Bag)HandScript.Instance.MyMoveable;
                    tmp.MyBagButton = this;
                    tmp.Use();
                    MyBag = tmp;
                    HandScript.Instance.Drop();
                    InventoryScript.Instance.FromSlot = null;
                }
            }
            else if(Input.GetKey(KeyCode.LeftShift))
            {
                HandScript.Instance.TakeMoveable(MyBag);
            }
            else if (_bag != null)
            {
                _bag.MyBagScrtipt.OpenClose();
            }
        }
    }

    public void RemoveBag()
    {
        InventoryScript.Instance.RemoveBag(MyBag);
        MyBag.MyBagButton = null;

        foreach (Item item in MyBag.MyBagScrtipt.GetItems())
        {
            InventoryScript.Instance.AddItem(item);
        }
        MyBag = null;
    }
}
