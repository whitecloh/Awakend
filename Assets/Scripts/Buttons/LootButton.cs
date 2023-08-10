using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LootButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Text _title;

    private LootWindow _lootWindow;
    public Image Icon => _icon;
    public Text Title => _title;

    public Item MyLoot { get; set; }

    private void Start()
    {
        _lootWindow = GetComponentInParent<LootWindow>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(InventoryScript.Instance.AddItem(MyLoot))
        {
            gameObject.SetActive(false);
            _lootWindow.TakeLoot(MyLoot);
            UIBarManager.MyInstance.HideTooltip();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIBarManager.MyInstance.ShowTooltip(new Vector2(1,0),transform.position,MyLoot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIBarManager.MyInstance.HideTooltip();
    }
}
