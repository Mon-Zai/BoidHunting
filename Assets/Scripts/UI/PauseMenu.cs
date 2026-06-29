using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    public void OnPauseClick()
    {
        bool willShow = !_pauseMenu.activeSelf;
        _pauseMenu.SetActive(willShow);
    }
}