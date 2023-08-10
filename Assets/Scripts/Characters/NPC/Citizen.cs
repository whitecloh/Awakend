using UnityEngine;

public class Citizen : NPC
{
    [SerializeField]
    private Dialogue _dialogue;

    public override void Interact()
    {
            DialogueWindow.Instance.SetDialogue(_dialogue);
            base.Interact(); 
    }
    public override void StopInteract()
    {
        DialogueWindow.Instance.CloseDialogue();
        base.StopInteract();
    }
}
