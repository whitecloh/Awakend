using UnityEngine;
using UnityEngine.UI;

public class CombatTextManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabCT;

    private static CombatTextManager instance;
    public static CombatTextManager Instance
    {
        get
        {
            if(instance==null)
            {
                instance = FindObjectOfType<CombatTextManager>();
            }
            return instance;
        }
    }

    public void CreateText(Vector3 posisiton , string text,SCtype type)
    {
        Text sct = Instantiate(_prefabCT, transform).GetComponent<Text>();
        sct.transform.position = posisiton;
        string before = string.Empty;
        string after=string.Empty;
        switch (type)
        {
            case SCtype.Damage:
                before = "-";
                sct.color = Color.red;
                break;
            case SCtype.Heal:
                before = "+";
                sct.color = Color.green;
                break;
            case SCtype.XP:
                before = "+";
                after = "XP";
                sct.color = Color.blue;
                break;
        }
        sct.text = before + text+after;
    }
}

public enum SCtype
{
    Damage,
    Heal,
    XP
}
