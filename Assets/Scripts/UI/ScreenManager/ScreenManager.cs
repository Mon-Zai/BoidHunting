using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private Dictionary<string, IScreen> _screens = new Dictionary<string, IScreen>();
    [SerializeField] private int _openScreensCount = 0;
    [SerializeField] private string _currentScreen;
    [SerializeField] private string _previousScreen;
    public string PreviousScreen => _previousScreen;
    public Action<string> OnScreenChanged;
    public bool IsAnyScreenOpen => _openScreensCount > 0;

    void Awake()
    {
        BaseScreen[] screens = GetComponentsInChildren<BaseScreen>(true);

        foreach (BaseScreen screen in screens)
        {
            Debug.Log($"Registering screen: {screen.gameObject.name}");
            screen.Init(this);
            _screens.Add(screen.ScreenName, screen);

            screen.Hide();
        }
    }
    public void ShowScreen(string screenName)
    {
        if (_screens.TryGetValue(screenName, out IScreen screen))
        {
            _currentScreen = screenName;
            screen.Show();
            _openScreensCount++;
            OnScreenChanged?.Invoke(screenName);
        }
        else
        {
            Debug.LogWarning($"Screen '{screenName}' not found.");
        }
    }
    public void HideScreen(string screenName)
    {
        if (_screens.TryGetValue(screenName, out IScreen screen))
        {
            _previousScreen = _currentScreen;
            screen.Hide();
            _openScreensCount--;
            OnScreenChanged?.Invoke(_previousScreen);
        }
        else
        {
            Debug.LogWarning($"Screen '{screenName}' not found.");
        }
    }
}
