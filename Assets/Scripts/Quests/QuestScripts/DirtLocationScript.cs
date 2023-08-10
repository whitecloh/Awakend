using UnityEngine;

public class DirtLocationScript : MonoBehaviour
{
    [SerializeField]
    private Transform _goblinStackParent;
    [SerializeField]
    private Collider _quest;
    [SerializeField]
    private GameObject _dialogue;
    [SerializeField]
    private GameObject _bearButton;
    [SerializeField]
    private GameObject _bear;

    private static DirtLocationScript instance;
    public static DirtLocationScript Instance
    {
        get
        {
            if(instance==null)
            {
                instance = FindObjectOfType<DirtLocationScript>();
            }
            return instance;
         }
    }
    private void Awake()
    {
        _dialogue.SetActive(false);
        _quest.enabled = false;
       Player.MyInstance.GetComponent<BearAction>().enabled = false;
        _bearButton.SetActive(false);
    }
    private void Update()
    {
        OpenQuest();
    }

    private void OpenQuest()
    {
        if (_goblinStackParent.childCount <= 0)
        {
            _quest.enabled = true;
            return;
        }
    }

    public void OnCompleteBearQuest()
    {
        Player.MyInstance.GetComponent<BearAction>().enabled = true;
        _bearButton.SetActive(true);
        _dialogue.SetActive(true);
        Destroy(_bear);
    }

}
