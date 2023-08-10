using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiverWindow : Window
{

    [SerializeField]
    private Button _backBtn, _acceptBtn, _completeBtn;
    [SerializeField]
    private GameObject _questPrefab, _questDescription;
    [SerializeField]
    private Transform _questArea;

    private QuestGiver _questGiver;
    private Quest _selectedQuest;

    private List<GameObject> _quests = new List<GameObject>();

    private static QuestGiverWindow instance;
    public static QuestGiverWindow Instance 
    { 
        get {
            if(instance==null)
             {
                instance = FindObjectOfType<QuestGiverWindow>();
             }
            return instance;
            }
    }

    public void ShowQuest(QuestGiver questGiver)
    {
        _acceptBtn.gameObject.SetActive(false);
        _questGiver = questGiver;

        foreach(GameObject go in _quests)
        {
            Destroy(go);
        }
        _questArea.gameObject.SetActive(true);
        _questDescription.SetActive(false);


        foreach (Quest quest in questGiver.Quests)
        {
            if (quest == null) return;

            GameObject go = Instantiate(_questPrefab, _questArea);
            go.GetComponent<Text>().text = "["+ quest.Level + "]"+ quest.Title;
            

            go.GetComponent<QGQuestScript>().Quest = quest;

            _quests.Add(go);
            if(QuestLog.Instance.HasQuest(quest)&&quest.IsComplete)
            {
                go.GetComponent<Text>().text += "(C)";
            }
            else if(QuestLog.Instance.HasQuest(quest))
            {
                Color c = go.GetComponent<Text>().color;
                c.a = 0.5f;
                go.GetComponent<Text>().color = c;
            }
        }
    }
    public void ShowQuestInfo(Quest quest)
    {
        _selectedQuest = quest;

        if(QuestLog.Instance.HasQuest(quest) && quest.IsComplete)
        {
            _acceptBtn.gameObject.SetActive(false);
            _completeBtn.gameObject.SetActive(true);
        }
        else if(!QuestLog.Instance.HasQuest(quest))
        {
            _acceptBtn.gameObject.SetActive(true);
        }

        _backBtn.gameObject.SetActive(true);
        _questArea.gameObject.SetActive(false);
        _questDescription.SetActive(true);


        string objectives = string.Empty;
        foreach (Objective objective in quest.CollectObjectives)
        {
            objectives += objective.Type + ":" + objective.CurrentAmount + "/" + objective.Amount + "\n";
        }
        _questDescription.GetComponent<Text>().text = string.Format("{0}\n\n<size=15>{1}</size>\n\n", quest.Title, quest.Description);
    }

    public void CompleteQuest()
    {
        if(_selectedQuest.IsComplete)
        {

            for (int i =0;i<_questGiver.Quests.Length;i++)
            {
                if(_selectedQuest == _questGiver.Quests[i])
                {
                    _questGiver.CompletedQuests.Add(_selectedQuest.Title);
                    _questGiver.Quests[i] = null;
                    _selectedQuest.MyQuestGiver.UpdateQuestStatus();
                }
            }

            foreach(CollectObjective obj in _selectedQuest.CollectObjectives)
            {
                InventoryScript.Instance.itemCountChangedEvent -= new ItemCountChangedEvent(obj.UpdateItemCount);
                obj.Complete();
            }
            foreach (KillObjective obj in _selectedQuest.KillObjectives)
            {
                KillManager.Instance._killConfirmedEvent -= new KillConfirmed(obj.UpdateKillCount);
            }
            Player.MyInstance.GainXP(XpManager.CalculcateExp(_selectedQuest));

QuestLog.Instance.RemoveQuest(_selectedQuest.MyQuestScript);
            Back();
        }
    }
    public void Back()
    {
        _backBtn.gameObject.SetActive(false);
        _acceptBtn.gameObject.SetActive(false);
        _completeBtn.gameObject.SetActive(false);
        ShowQuest(_questGiver);
    }
    public void Accept()
    {
        QuestLog.Instance.AcceptQuest(_selectedQuest);
        Back();
    }

    public override void Open(NPC npc)
    {
        ShowQuest(npc as QuestGiver);
        base.Open(npc);
    }
    public override void Close()
    {
        _completeBtn.gameObject.SetActive(false);
        base.Close();
    }
}
