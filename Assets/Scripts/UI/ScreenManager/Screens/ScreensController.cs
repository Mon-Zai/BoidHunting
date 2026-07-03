using UnityEngine;

public class ScreensController : MonoBehaviour
{
    [SerializeField] private DefeatMenu DefeatMenu;
    [SerializeField] private DefeatCondition defeatCondition;
    [SerializeField] private VictoryMenu VictoryMenu;
    [SerializeField] private VictoryCondition victoryCondition;
    [SerializeField] private ScreenManager _screenManager;
    void Start()
    {
        defeatCondition.OnDefeatEvent += ShowDefeatMenu;
        victoryCondition.OnVictoryEvent += ShowVictoryMenu;
    }
    void ShowDefeatMenu(string reason)
    {
        DefeatMenu.SetDefeatReason(reason);
        _screenManager.ShowScreen(DefeatMenu.ScreenName);
    }
    void ShowVictoryMenu()
    {
        _screenManager.ShowScreen(VictoryMenu.ScreenName);
    }
    void OnDisable()
    {
        defeatCondition.OnDefeatEvent -= ShowDefeatMenu;
        victoryCondition.OnVictoryEvent -= ShowVictoryMenu;
    }

}