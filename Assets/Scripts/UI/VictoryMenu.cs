using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject _victoryMenu;
    public void ShowVictoryMenu()
    {
        Time.timeScale = 0f;
        _victoryMenu.SetActive(true);
    }
}