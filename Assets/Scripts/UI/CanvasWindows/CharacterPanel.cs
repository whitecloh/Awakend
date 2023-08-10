using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private CharButton helmet, sword, cape, wings, shield, boots;

    public CharButton SelectedButton
    {
        get;set;
    }
    private static CharacterPanel instance;
    
    public static CharacterPanel Instance
    {
        get
        {
            if(instance==null)
            {
                instance = GameObject.FindObjectOfType<CharacterPanel>();
            }
            return instance;
        }
    }


    private void Update()
    {
        OpenCloseButton(); 
    }
    public void OpenCloseButton()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenClose();
        }
    }
    public void OpenClose()
    {
            if (_canvasGroup.alpha <= 0)
            {
                _canvasGroup.alpha = 1;
                _canvasGroup.blocksRaycasts = true;
            }
            else
            {
                _canvasGroup.alpha = 0;
                _canvasGroup.blocksRaycasts = false;
            }        
    }

    public void EquipArmor(Armor armor)
    {
        switch (armor.ArmorType)
        {
            case ArmorType.Helmet:
                helmet.EquipArmor(armor);
                break;
            case ArmorType.Shield:
                shield.EquipArmor(armor);
                break;
            case ArmorType.Sword:
                sword.EquipArmor(armor);
                break;
            case ArmorType.Cape:
                cape.EquipArmor(armor);
                break;
            case ArmorType.Wings:
                wings.EquipArmor(armor);
                break;
            case ArmorType.Boots:
                boots.EquipArmor(armor);
                break;
        }
    }
}