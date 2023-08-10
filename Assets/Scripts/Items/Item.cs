using UnityEngine;

public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private int _stackSize;
    [SerializeField]
    private Quality _quality;
    [SerializeField]
    private string _title;

    private SlotScript _slot;

    private CharButton _charButton;
    public CharButton CharButton
    {
        get
        {
            return _charButton;
        }
        set
        {
            MySlot = null;
            _charButton = value;
        }
    }

    public string Title => _title;
    public Quality Quality => _quality;
    public int MyStackSize => _stackSize;
    public SlotScript MySlot
    {
        get
        {
            return _slot;
        }
        set
        {
            _slot = value;
        }
    }
    public Sprite MyIcon => _icon;

    public virtual string GetDescription()
    {

        return string.Format("<color={0}>{1}</color>",QualityColor.Colors[_quality],_title);
    }

    public void Remove()
    {
         if(MySlot!=null)
        {
            MySlot.RemoveItem(this);
        }
    }
}