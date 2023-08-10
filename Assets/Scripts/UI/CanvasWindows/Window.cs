using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;
    private NPC _npc;

    public virtual void Open(NPC npc)
    {
        _npc = npc;
        _npc.GetComponent<Collider>().enabled = false;
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }
    public virtual void Close()
    {
        if (_npc != null)
        {
            _npc._isInteracting = false;
        _npc.GetComponent<Collider>().enabled = true;
    }
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _npc = null;
    }
}
