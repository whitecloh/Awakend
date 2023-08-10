using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWindow : Window
{

    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _answerButton;
    [SerializeField]
    private Transform _answerPos;

    private Dialogue _dialogue;
    private DialogueNode _currenNode;

    private static DialogueWindow instance;
    public static DialogueWindow Instance
    {
        get
        {
            if(instance==null)
            {
                instance = FindObjectOfType<DialogueWindow>();
            }
            return instance;  
        }
    }

    private List<DialogueNode> answers = new List<DialogueNode>();
    private List<GameObject> buttons = new List<GameObject>();

    public void SetDialogue(Dialogue dialogue)
    {
        _text.text = string.Empty;
        _dialogue = dialogue;
        _currenNode = dialogue.DialogueNodes[0];

        StartCoroutine(RunDialodue(_currenNode.Text));
    }

    private IEnumerator RunDialodue(string dialogue)
    {
        for(int i=0;i<dialogue.Length;i++)
        {
            _text.text += dialogue[i];
            yield return new WaitForSeconds(_speed);
        }
        ShowAnswers();
    }

    private void ShowAnswers()
    {
        answers.Clear();

        foreach(DialogueNode node in _dialogue.DialogueNodes)
        {
            if(node.Parent == _currenNode.Name)
            {
                answers.Add(node);
            }
        }
        if(answers.Count>0)
        {
            _answerPos.gameObject.SetActive(true);
            foreach(DialogueNode node in answers)
            {
                GameObject go = Instantiate(_answerButton, _answerPos);
                buttons.Add(go);
                go.GetComponentInChildren<Text>().text = node.Answer;
                go.GetComponent<Button>().onClick.AddListener(delegate { PickAnswer(node); });
            }
        }
        else
        {
            _answerPos.gameObject.SetActive(true);
            GameObject go = Instantiate(_answerButton, _answerPos);
            buttons.Add(go);
            go.GetComponentInChildren<Text>().text = "”йти";
            go.GetComponent<Button>().onClick.AddListener(delegate { CloseDialogue(); });
        }
    }

    private void PickAnswer(DialogueNode node)
    {
        _currenNode = node;
        Clear();
        StartCoroutine(RunDialodue(_currenNode.Text));
    }

    private void Clear()
    {
        _text.text = string.Empty;
        _answerPos.gameObject.SetActive(false);

        foreach(GameObject go in buttons)
        {
            Destroy(go);
        }
        buttons.Clear();
    }

    public void CloseDialogue()
    {
        Close();
        Clear();
    }
}
