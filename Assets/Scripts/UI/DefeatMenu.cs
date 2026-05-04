using TMPro;
using UnityEngine;

public class DefeatMenu : MonoBehaviour
{
    [SerializeField] private GameObject _defeatMenu;
    [SerializeField] private TextMeshProUGUI _defeatBoids;
    [SerializeField] private TextMeshProUGUI _defeatFood;

    public void ShowDefeatMenu(string reason)
    {
        Time.timeScale = 0f;

        _defeatMenu.SetActive(true);
        if (reason == "Boids")
        {
            _defeatBoids.gameObject.SetActive(true);
            _defeatFood.gameObject.SetActive(false);
        }
        else if (reason == "Food")
        {
            _defeatBoids.gameObject.SetActive(false);
            _defeatFood.gameObject.SetActive(true);
        }
    }

}