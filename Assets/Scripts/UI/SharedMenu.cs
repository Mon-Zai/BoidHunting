using UnityEngine;

[CreateAssetMenu(fileName = "SharedMenu", menuName = "ScriptableObjects/Menu", order = 1)]
public class SharedMenu : ScriptableObject
{
    public void OnPlay()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void OnRestartButton()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
