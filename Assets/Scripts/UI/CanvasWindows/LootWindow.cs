using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootWindow : MonoBehaviour
{
    [SerializeField]
    private LootButton[] _lootButtons;
    [SerializeField]
    private Item[] _items;
    [SerializeField]
    private Button _nextButton, _previousButton;

    private CanvasGroup _canvasGroup;
    
    private List<List<Item>> pages = new List<List<Item>>();
    public List<Item> droppedLoot = new List<Item>();

    private int _pageIndex=0;

    public bool IsOpen
    { 
        get
        { return _canvasGroup.alpha > 0; } 
    }

    private static LootWindow instance;
    public static LootWindow Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<LootWindow>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void CreatePages(List<Item> items)
    {
        if (!IsOpen)
        {
            List<Item> page = new List<Item>();
            droppedLoot = items;
            for (int i = 0; i < items.Count; i++)
            {
                page.Add(items[i]);

                if (page.Count == 4 || i == items.Count - 1)
                {
                    pages.Add(page);
                    page = new List<Item>();
                }
            }

            AddLoot();
            OpenLootWindow();
        }
        else
        {
            CloseLootWindow();

            List<Item> page = new List<Item>();
            droppedLoot = items;
            for (int i = 0; i < items.Count; i++)
            {
                page.Add(items[i]);

                if (page.Count == 4 || i == items.Count - 1)
                {
                    pages.Add(page);
                    page = new List<Item>();
                }
            }
            AddLoot();
            OpenLootWindow();
        }
    }
    private void AddLoot()
    {

        if (pages.Count > 0)
        {
            _previousButton.gameObject.SetActive(_pageIndex > 0);
            _nextButton.gameObject.SetActive(pages.Count > 1 && _pageIndex < pages.Count-1);

            for (int i = 0; i < pages[_pageIndex].Count; i++)
            {
                if (pages[_pageIndex][i] != null)
                {
                    _lootButtons[i].Icon.sprite = pages[_pageIndex][i].MyIcon;
                    _lootButtons[i].MyLoot = pages[_pageIndex][i];

                    _lootButtons[i].gameObject.SetActive(true);

                    string title = string.Format("<color={0}>{1}</color>", QualityColor.Colors[pages[_pageIndex][i].Quality], pages[_pageIndex][i].Title);
                    _lootButtons[i].Title.text = title;
                }
            }
        }
    }

    public void ClearButtons()
    {
        foreach (LootButton button in _lootButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void TakeLoot(Item loot)
    {
        pages[_pageIndex].Remove(loot);
        droppedLoot.Remove(loot);

        if (pages[_pageIndex].Count ==0)
        {
            pages.Remove(pages[_pageIndex]);

            if(_pageIndex == pages.Count && _pageIndex>0)
            {
                _pageIndex--;
            }
            AddLoot();
        }
    }

    public void OpenLootWindow()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    public void NextPage()
    {
        if(_pageIndex<pages.Count-1)
        {
            _pageIndex++;
            ClearButtons();
            AddLoot();
        }
    }
    public void PreviousPage()
    {
        if(_pageIndex>0)
        {
            _pageIndex--;
            ClearButtons();
            AddLoot();
        }
    }

    public void CloseLootWindow()
    {
        _pageIndex = 0;
        pages.Clear();
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        ClearButtons();
    }

}
