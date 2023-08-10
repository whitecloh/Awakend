using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPC
{
    [SerializeField]
    private Quest[] _quests;
    [SerializeField]
    private Sprite _questionSprite,_exclamationSprite;
    [SerializeField]
    private SpriteRenderer _statusRenderer;
    [SerializeField]
    private int _questGiverId;

    public Quest[] Quests => _quests;
    private List<string> _completedQuests = new List<string>();
    public List<string> CompletedQuests
    {
        get
        {
            return _completedQuests;
        }
        set
        {
            _completedQuests = value;

            foreach(string title in _completedQuests)
            {
                for(int i=0;i<_quests.Length;i++)
                {
                    if(_quests[i]!=null && _quests[i].Title == title)
                    {
                        _quests[i] = null;
                    }
                }
            }
        }
    }

    public int QuestGiverId => _questGiverId;

    private void Awake()
    {
            foreach(Quest quest in _quests)
        {
            quest.MyQuestGiver = this;
        }
    }
    public void UpdateQuestStatus()
    {
        int count = 0;
        foreach(Quest quest in _quests)
        {
            if(quest!=null)
            {
                if(quest.IsComplete&&QuestLog.Instance.HasQuest(quest))
                {
                    _statusRenderer.sprite = _exclamationSprite;
                    break;
                }
                else if(!QuestLog.Instance.HasQuest(quest))
                {
                    _statusRenderer.sprite = _questionSprite;
                    break;
                }
                else if (!quest.IsComplete && QuestLog.Instance.HasQuest(quest))
                {
                    _statusRenderer.sprite = _exclamationSprite;
                    break;
                }

            }
            else
            {
                count++;
                if(count == _quests.Length)
                {
                    if (QuestGiverId == 1)
                    {
                        GrassLocationComplete.Instance.OpenDoors();
                        QuestGiverWindow.Instance.Close();
                    }
                    if(QuestGiverId==2)
                    {
                        SandLocationScript.Instance.OnComplete();
                        DialogueWindow.Instance.CloseDialogue();
                        QuestGiverWindow.Instance.Close();
                    }
                    if(QuestGiverId==3)
                    {
                        DirtLocationScript.Instance.OnCompleteBearQuest();
                        QuestGiverWindow.Instance.Close();
                    }
                    _statusRenderer.enabled = false;
                }
            }
        }
    }
}
