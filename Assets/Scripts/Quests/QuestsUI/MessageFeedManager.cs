using UnityEngine;
using UnityEngine.UI;

public class MessageFeedManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _messagePrefab;

    private static MessageFeedManager _instance;
    public static MessageFeedManager Instance
    {
        get
        {
            if(_instance ==null)
            {
                _instance = FindObjectOfType<MessageFeedManager>();
            }
            return _instance;
        }
    }

    public void WriteMessage(string message)
    {
        GameObject go = Instantiate(_messagePrefab, transform);
        go.GetComponent<Text>().text = message;
        go.transform.SetAsFirstSibling();
        Destroy(go,3f);
    }
}
