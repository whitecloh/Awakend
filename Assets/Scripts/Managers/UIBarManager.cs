using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIBarManager : MonoBehaviour
{
    [SerializeField]
    private Image[] _castBars;
    [SerializeField]
    private FireBall _fireBall;
    [SerializeField]
    private GameObject _tooltip;
    [SerializeField]
    private CharacterPanel _characterPanel;
    [SerializeField]
    private RectTransform _tooltipRect;
    [SerializeField]
    private Text _strengthTxt, _staminaTxt, _intellectTxt;
    private Text _tooltipText;

    private static UIBarManager instance;

    public static UIBarManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIBarManager>();
            }
            return instance;
        }
    }

    private void Start()
    {
        _tooltipText = _tooltip.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            InventoryScript.Instance.OpenClose();
        }
    }
    public IEnumerator Progress(int index,float cd)
    {
        float rate = 1.0f / cd;
        float progress = 0f;
        while (progress <= 1)
        {
            _castBars[index].fillAmount = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;

            yield return null;
        }
        _castBars[index].fillAmount = 0;
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.enabled=true;
            clickable.MyIcon.enabled=true;
        }
        else
        {
            clickable.MyStackText.enabled = false;
            clickable.MyIcon.enabled=true;
        }

        if (clickable.MyCount == 0)
        {
            clickable.MyIcon.enabled=false;
            clickable.MyStackText.enabled=false;
        }
    }

    public void ShowTooltip(Vector2 pivot , Vector3 position , IDescribable description)
    {
        _tooltipRect.pivot = pivot;
        _tooltip.SetActive(true);
        _tooltip.transform.position = position;
        _tooltipText.text = description.GetDescription();
    }
    public void HideTooltip()
    {

        _tooltip.SetActive(false);
    }

    public void RefreshTooltip(IDescribable description)
    {
        _tooltipText.text = description.GetDescription();
    }


    public void UpdateStatsText(int strength,int stamina,int intellect)
    {
        _strengthTxt.text = "Сила: " + strength.ToString();
        _staminaTxt.text = "Ловкость: " + stamina.ToString();
        _intellectTxt.text = "Интеллект: " + intellect.ToString();
    }
}
