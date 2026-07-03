using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : BaseScreen
{
    [SerializeField] private SavesMenu _savesMenu;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Toggle toggle;
    public void TOGGLE_CheckToggleValue()
    {
        AudioManager.Instance.IsSoundEnabled = toggle.isOn;
        AudioManager.Instance.MusicSource.mute = !toggle.isOn;
    }

    public void BTN_OnPauseClick()
    {
        Debug.Log("Pause button clicked");
        bool willShow = !gameObject.activeSelf;
        if (willShow)
        {
            _screenManager.ShowScreen(ScreenName);
        }
        else
        {
            _screenManager.HideScreen(ScreenName);
        }
    }
    public void BTN_DataClick()
    {
        _screenManager.ShowScreen(_savesMenu.ScreenName);
        _screenManager.HideScreen(ScreenName);
    }
    public override void Show()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            _menuButton.gameObject.SetActive(false);
        }
        else
        {
            _menuButton.gameObject.SetActive(true);
        }
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }
}