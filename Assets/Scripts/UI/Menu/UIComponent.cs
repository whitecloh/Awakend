using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class UIComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject _settingsPanel;

        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "MenuScene")
            {
                CloseSettings(); 
            }
        }
        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void ShowSettings()
        {
            _settingsPanel.GetComponent<CanvasGroup>().alpha = 1;
            _settingsPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        public void CloseSettings()
        {
            _settingsPanel.GetComponent<CanvasGroup>().alpha = 0;
            _settingsPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        public void StartNewGame()
        {
            SceneManager.LoadScene("FirstScene");
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPaused = true;
#elif UNITY_STANDALONE
        Application.Quit();
#endif
        }

    }
}
