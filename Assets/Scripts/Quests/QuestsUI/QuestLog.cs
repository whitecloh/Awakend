using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField]
    private GameObject _questPrefab;
    [SerializeField]
    private Transform _questParent;
    [SerializeField]
    private Text _questDescription;
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private Text _questCountTxt;
    [SerializeField]
    private int _maxCount;

    private int _currentCount=0;

    private Quest _selectedQuest;

    private List<QuestScript> _questScripts = new List<QuestScript>();
    private List<Quest> _quests = new List<Quest>();

    private static QuestLog instance;
    public static QuestLog Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestLog>();
            }
            return instance;
        }

    }

    public List<Quest> Quests { get => _quests; set => _quests = value; }

    private void Start()
    {
        _questCountTxt.text = _currentCount + "/" + _maxCount;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            OpenClose();
        }
    }

    public void AcceptQuest(Quest quest)
    {
        if (_currentCount >= _maxCount) return;

        _currentCount++;
        _questCountTxt.text = _currentCount + "/" + _maxCount;

        foreach (CollectObjective obj in quest.CollectObjectives)
        {
            InventoryScript.Instance.itemCountChangedEvent += new ItemCountChangedEvent(obj.UpdateItemCount);
            obj.UpdateItemCount();
        }
        foreach(KillObjective ko in quest.KillObjectives)
        {
            KillManager.Instance._killConfirmedEvent += new KillConfirmed(ko.UpdateKillCount);
        }
        _quests.Add(quest);

        GameObject go = Instantiate(_questPrefab,_questParent);
        QuestScript qs = go.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;        
        qs.MyQuest = quest;
        _questScripts.Add(qs);

        go.GetComponent<Text>().text = quest.Title;
        CheckCompletion();
    }
    public void UpdateSelected()
    {
        ShowDescription(_selectedQuest);
    }

    public void ShowDescription(Quest quest)
    {
        if (quest == null) return;
        if (_selectedQuest != null && _selectedQuest!=quest)
        {
            _selectedQuest.MyQuestScript.Deselect();
        }
        string objectives = string.Empty;

        _selectedQuest = quest;
        string title = quest.Title;
        foreach(Objective objective in quest.CollectObjectives)
        {
            objectives += objective.Type + ":" + objective.CurrentAmount + "/" + objective.Amount + "\n";
        }
        foreach (Objective objective in quest.KillObjectives)
        {
            objectives += objective.Type + ":" + objective.CurrentAmount + "/" + objective.Amount + "\n";
        }
        _questDescription.text = string.Format("{0}\n\n<size=22>{1}</size>\n\n÷ÂÎË:\n\n<size=20>{2}</size>", title,quest.Description,objectives);
    }

    public void CheckCompletion()
    {
        foreach(QuestScript qs in _questScripts)
        {
            qs.MyQuest.MyQuestGiver.UpdateQuestStatus();
            qs.IsComplete();
        }
    }

    public void OpenClose()
    {
        if(_canvasGroup.alpha ==1)
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
        }
        else
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
        }
    }
    public void Close()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    public void AbandonQuest()
    {
        foreach (CollectObjective obj in _selectedQuest.CollectObjectives)
        {
            InventoryScript.Instance.itemCountChangedEvent -= new ItemCountChangedEvent(obj.UpdateItemCount);
        }
        foreach (KillObjective obj in _selectedQuest.KillObjectives)
        {
            KillManager.Instance._killConfirmedEvent -= new KillConfirmed(obj.UpdateKillCount);
        }
        RemoveQuest(_selectedQuest.MyQuestScript);
    }
    public void RemoveQuest(QuestScript quest)
    {
        _questScripts.Remove(quest);
        Destroy(quest.gameObject);
        _quests.Remove(quest.MyQuest);
        _questDescription.text = string.Empty;
        _selectedQuest = null;
        _currentCount--;
        _questCountTxt.text = _currentCount + "/" + _maxCount;
        quest.MyQuest.MyQuestGiver.UpdateQuestStatus();
        quest = null;

    }

    public bool HasQuest(Quest quest)
    {
        return _quests.Exists(x => x.Title == quest.Title);
    }
}
