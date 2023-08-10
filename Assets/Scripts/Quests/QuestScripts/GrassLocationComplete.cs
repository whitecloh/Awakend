using UnityEngine;

public class GrassLocationComplete : MonoBehaviour
{
    [SerializeField]
    private GameObject _closedDoor;
    [SerializeField]
    private GameObject _openedDoor;
    [SerializeField]
    private GameObject _woodDoor;
    [SerializeField]
    private Transform _skeletonsParent;
    [SerializeField]
    private QuestGiver _npcQuest;

    public bool SkeletonsCount
    {
        get
        {
            return _skeletonsParent.childCount<=0;
        }
    }
    private static GrassLocationComplete instance;
    public static GrassLocationComplete Instance
    {
        get
        {
            if(instance==null)
            {
                instance = FindObjectOfType<GrassLocationComplete>();
            }
            return instance;
         }
    }

    private bool isMessage;

    private void Awake()
    {
        _closedDoor.SetActive(false);
        _npcQuest.GetComponent<Collider>().enabled = false;
    }
    public void OpenDoors()
    {
        Destroy(_openedDoor);
        _closedDoor.SetActive(true);
    }

    private void Update()
    {
        if(SkeletonsCount&&!isMessage)
        {
            Message();
        }
    }

    private void Message()
    {
        isMessage = true;
        OpenWoodDoors();
        MessageFeedManager.Instance.WriteMessage("Город зачищен!");
        MessageFeedManager.Instance.WriteMessage("Подойдите к старцу");
        _npcQuest.GetComponent<Collider>().enabled = true;
    }

    public void OpenWoodDoors()
    {
        _woodDoor.SetActive(false);
    }
}
