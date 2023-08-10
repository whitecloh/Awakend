using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    private Button _exitButton;
    [SerializeField]
    private Button _unpauseButton;
    [SerializeField]
    private Button _startMenuButton;
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private AudioClip _musicMenu;

    private void Awake()
    {
        _exitButton.onClick.AddListener(ExitGame);
        _unpauseButton.onClick.AddListener(OpenClose);
        _startMenuButton.onClick.AddListener(ToStartMenu);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenClose();
        }
    }
    private void ToStartMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    private void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPaused = true;
#elif UNITY_STANDALONE
        Application.Quit();
#endif
    }

    private void OpenClose()
    {
        if(_canvasGroup.alpha==1)
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
