using UnityEngine;

[CreateAssetMenu(fileName ="HealthsPotion",menuName ="Items/Potion",order =1)]
public class HealthsPotion : Item, IUseable
{
    [SerializeField]
    private int _healValue;
     
    public void Use()
    {
        if(Player.MyInstance.MyHealth.MyCurrentValue < Player.MyInstance.MyHealth.MyMaxValue)
        Remove();
        Player.MyInstance.GetHealth(_healValue);
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n��������������� {0} ��������.",_healValue);
    }
}
