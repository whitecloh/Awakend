using UnityEngine;

public class MapUI : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            OpenClose();
        }
    }
    private void OpenClose()
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

}
