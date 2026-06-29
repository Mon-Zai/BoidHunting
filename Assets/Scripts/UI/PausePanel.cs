using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PausePanel : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;

    void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            _pauseButton.gameObject.SetActive(false);
        }
        else
        {
            _pauseButton.gameObject.SetActive(true);
        }
        Time.timeScale = 0f;

    }
    void OnDisable()
    {
        Time.timeScale = 1f;
    }
}