using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
    public Quest MyQuest { get; set; }
    private bool _markedComplete=false;

    public void Select()
    {
        GetComponent<Text>().color = Color.red;
        QuestLog.Instance.ShowDescription(MyQuest);
    }
    public void Deselect()
    {
        GetComponent<Text>().color = Color.white;
    }

    public void IsComplete()
    {
        if(MyQuest.IsComplete&&!_markedComplete)
        {
            _markedComplete = true;
            GetComponent<Text>().text = "[" + MyQuest.Level + "]" + MyQuest.Title + "(Выполнено)";
            MessageFeedManager.Instance.WriteMessage(string.Format("{0}(Выполнено)", MyQuest.Title));
        }
        else if(!MyQuest.IsComplete)
        {
            _markedComplete = false;
            GetComponent<Text>().text = "[" + MyQuest.Level + "]" + MyQuest.Title;
        }
    }
}
