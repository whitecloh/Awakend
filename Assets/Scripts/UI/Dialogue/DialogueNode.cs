using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _parent;
    [SerializeField]
    private string _answer;
    [SerializeField]
    private string _text;

    public string Name { get => _name; set => _name = value; }
    public string Parent { get => _parent; set => _parent = value; }
    public string Answer { get => _answer; set => _answer = value; }
    public string Text { get => _text; set => _text = value; }
}
