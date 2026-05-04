using UnityEngine;

public class ScreensController : MonoBehaviour
{
    [SerializeField] private DefeatMenu DefeatMenu;
    [SerializeField] private DefeatCondition defeatCondition;
    [SerializeField] private VictoryMenu VictoryMenu;
    [SerializeField] private VictoryCondition victoryCondition;

    void Start()
    {
        defeatCondition.OnDefeatEvent += ShowDefeatMenu;
        victoryCondition.OnVictoryEvent += ShowVictoryMenu;
    }
    void ShowDefeatMenu(string reason)
    {
        DefeatMenu.ShowDefeatMenu(reason);
    }
    void ShowVictoryMenu()
    {
        VictoryMenu.ShowVictoryMenu();
    }
    void OnDisable()
    {
        defeatCondition.OnDefeatEvent -= ShowDefeatMenu;
        victoryCondition.OnVictoryEvent -= ShowVictoryMenu;
    }

}