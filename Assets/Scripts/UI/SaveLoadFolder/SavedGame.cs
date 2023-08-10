using UnityEngine;
using UnityEngine.UI;

public class SavedGame : MonoBehaviour
{
    [SerializeField]
    private Text _dateTime;
    [SerializeField]
    private Image _health;
    [SerializeField]
    private Image _mana;
    [SerializeField]
    private Image _xp;
    [SerializeField]
    private Text _healthsText;
    [SerializeField]
    private Text _manaText;
    [SerializeField]
    private Text _xpText;
    [SerializeField]
    private Text _levelText;
    [SerializeField]
    private GameObject _visuals;

    [SerializeField]
    private int _index;

    public int Index => _index;

    public void ShowInfo(SaveData data)
    {
        _visuals.SetActive(true);

        _dateTime.text = "Date: " + data.MyDateTime.ToString("dd/MM/yyyy") + "- Time:" + data.MyDateTime.ToString("H:mm");

        _health.fillAmount = data.MyPlayerData.MyHealth / data.MyPlayerData.MyMaxHealth;
        _healthsText.text = data.MyPlayerData.MyHealth + "/" + data.MyPlayerData.MyMaxHealth;

        _mana.fillAmount = data.MyPlayerData.MyMana / data.MyPlayerData.MyMaxMana;
        _manaText.text = data.MyPlayerData.MyMana + "/" + data.MyPlayerData.MyMaxMana;

        _xp.fillAmount = data.MyPlayerData.MyXP / data.MyPlayerData.MyMaxXP;
        _xpText.text = data.MyPlayerData.MyXP + "/" + data.MyPlayerData.MyMaxXP;

        _levelText.text = data.MyPlayerData.MyLevel.ToString();
    }

    public void HideVisuals()
    {
        _visuals.SetActive(false);
    }
}
