using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private DialogueNode[] _dialogueNodes;

    public DialogueNode[] DialogueNodes { get => _dialogueNodes; set => _dialogueNodes = value; }
}
