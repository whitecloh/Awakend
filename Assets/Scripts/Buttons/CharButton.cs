using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private ArmorType _armorType;
    [SerializeField]
    private GearSoccet _gearSoccet;
    [SerializeField]
    private Image _icon;

    private Armor _equippedArmor;

    public Armor EquippedArmor { get => _equippedArmor;}

    public void EquipArmor(Armor armor)
    {
        armor.Remove(); 

        if(_equippedArmor!=null)
        {
            Player.MyInstance.DequipGear(_equippedArmor);

            if (_equippedArmor!=armor)
            {
                armor.MySlot.AddItem(_equippedArmor);
            }
            
            UIBarManager.MyInstance.RefreshTooltip(_equippedArmor);
        }
        else
        {
            UIBarManager.MyInstance.HideTooltip();
        }
        _icon.enabled = true;
        _icon.sprite = armor.MyIcon;
        _icon.color = Color.white;
        this._equippedArmor = armor;
        this._equippedArmor.CharButton = this; 

        if(HandScript.Instance.MyMoveable == (armor as IMoveable))
        {
            HandScript.Instance.Drop();
        }

        if(_gearSoccet!=null)
        {
            _gearSoccet.Equip(_equippedArmor);
        }

        Player.MyInstance.EquipGear(armor);
    }
    public void DequipArmor()
    {
        _icon.color = Color.white;
        _icon.enabled = false;
        if(_gearSoccet!=null)
        {
            Player.MyInstance.DequipGear(_equippedArmor);
            _gearSoccet.Dequip();
        }
        else if(_equippedArmor!=null)
        {
            Player.MyInstance.DequipGear(_equippedArmor);
        }
        _equippedArmor.CharButton = null;
        _equippedArmor = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.Instance.MyMoveable is Armor)
            {
                Armor tmp = (Armor)HandScript.Instance.MyMoveable;

                if (tmp.ArmorType == _armorType)
                {
                    EquipArmor(tmp);
                }
                UIBarManager.MyInstance.RefreshTooltip(tmp);
            }
            else if (HandScript.Instance.MyMoveable == null && _equippedArmor != null)
            {
                HandScript.Instance.TakeMoveable(_equippedArmor);
                CharacterPanel.Instance.SelectedButton = this;
                _icon.color = Color.grey;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_equippedArmor!=null)
        {
            UIBarManager.MyInstance.ShowTooltip(new Vector2(0,0),transform.position,_equippedArmor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIBarManager.MyInstance.HideTooltip();
    }
}
