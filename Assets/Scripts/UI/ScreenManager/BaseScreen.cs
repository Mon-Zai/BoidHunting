using UnityEngine;

public class BaseScreen : MonoBehaviour, IScreen
{
    protected ScreenManager _screenManager;
    private string _screenName = string.Empty;
    public string ScreenName => _screenName;
    public void Init(ScreenManager screenManager)
    {
        _screenManager = screenManager;
         _screenName = GetType().Name;
    }
    public virtual void Show() => gameObject.SetActive(true);

    public virtual void Hide() => gameObject.SetActive(false);
}