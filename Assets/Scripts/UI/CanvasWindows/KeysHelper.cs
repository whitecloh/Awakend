using UnityEngine;

public class KeysHelper : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OpenClose();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }
    public void OpenClose()
    {
        if (_canvasGroup.alpha == 1)
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            Time.timeScale = 1f;
        }
        else
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            Time.timeScale = 0f;
        }
    }

    private void Close()
    {
        if (_canvasGroup.alpha == 1)
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            Time.timeScale = 1f;
        }
    }

}
