using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SharedMenu", menuName = "ScriptableObjects/Menu", order = 1)]
public class SharedMenu : ScriptableObject
{
    public Action OnPlayAction;
    public void Subscribe(Action listener) => OnPlayAction += listener;
    public void Unsubscribe(Action listener) => OnPlayAction -= listener;
    public void OnPlayButton()
    {
        Time.timeScale = 1f;
        OnPlayAction?.Invoke();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void OnPickLevel(int sceneIndex)
    {
        Time.timeScale = 1f;
        OnPlayAction?.Invoke();
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void OnRestartButton()
    {
        Time.timeScale = 1f;
        OnPlayAction?.Invoke();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
