using UnityEngine;

public class GearSoccet : MonoBehaviour
{
    public void Equip(Armor armor)
    {
        int childCount = transform.childCount;
        if (childCount != 0)
        {
            Dequip();
        }
        Instantiate(armor.ArmorPrefab,transform);
    }
    public void Dequip()
    {
        var armor = transform.GetChild(0).gameObject;
        armor.SetActive(false);
        Destroy(armor);
    }
}
