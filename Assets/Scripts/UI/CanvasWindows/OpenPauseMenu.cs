using UnityEngine;

public class OpenPauseMenu : MonoBehaviour
{
    [SerializeField]
    private PauseMenu _menu;

    private void Update()
    {
        OpenPauseMenue();
    }

    public void OpenPauseMenue()
    {
        if (_menu.gameObject.activeSelf) return;

        var key = Input.GetKeyDown(KeyCode.Escape);

        if (key)
        {
            _menu.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        
    }
}
