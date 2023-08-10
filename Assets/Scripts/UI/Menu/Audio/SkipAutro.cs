using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipAutro : MonoBehaviour
{
    public void SkipFirst()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void SkipLast()
    {
        SceneManager.LoadScene("CreditsScene");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
