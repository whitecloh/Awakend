using UnityEngine;

public class SaveLoadMenu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        Close();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }
    public void Open()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }
    public void Close()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

}
