using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]
public class Armor : Item
{
    [SerializeField]
    private GameObject _armorPrefab;
    [SerializeField]
    private ArmorType _armorType;
    [SerializeField]
    private int _strength;
    [SerializeField]
    private int _stamina;
    [SerializeField]
    private int _intellect;

    public ArmorType ArmorType => _armorType;
    public GameObject ArmorPrefab => _armorPrefab;

    public int Strength { get => _strength; set => _strength = value; }
    public int Stamina { get => _stamina; set => _stamina = value; }
    public int Intellect { get => _intellect; set => _intellect = value; }

    public override string GetDescription()
    {
        string stats = string.Empty;

        if(_strength>0)
        {
            stats += string.Format("\n+{0} силы",_strength);
        }
        if (_stamina > 0)
        {
            stats += string.Format("\n+{0} ловкости", _stamina);
        }
        if (_intellect > 0)
        {
            stats += string.Format("\n+{0} интеллекта", _intellect);
        }
        return base.GetDescription() + stats;
    }

    public void Equip()
    {
        CharacterPanel.Instance.EquipArmor(this);
    }
}

public enum ArmorType
{
    Helmet,
    Shield,
    Sword,
    Cape,
    Wings,
    Boots
}
