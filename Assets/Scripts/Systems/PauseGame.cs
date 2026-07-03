using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private ScreenManager _screenManager;
    [SerializeField] private bool _isPlaying = true;

    void Awake()
    {
        _screenManager = GetComponent<ScreenManager>();
        _screenManager.OnScreenChanged += HandleScreenChanged;
    }

    private void HandleScreenChanged(string screenName)
    {
        if (_screenManager.IsAnyScreenOpen && _isPlaying)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    private void Pause()
    {
        Debug.Log("Pausing game");
        Time.timeScale = 0f;

    }
    private void Resume()
    {
        Debug.Log("Resuming game");
        Time.timeScale = 1f;
    }
}