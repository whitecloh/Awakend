using UnityEngine;

public class CloseOpenSaveLoad : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    public virtual void Open()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }
    public virtual void Close()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }
}
