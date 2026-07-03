public class SavesMenu : BaseScreen
{
    public void BTN_BackClick()
    {
        _screenManager.ShowScreen(_screenManager.PreviousScreen);
        _screenManager.HideScreen(ScreenName);
    }
}
