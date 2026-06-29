using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UserUIController : MonoBehaviour
{
    [SerializeField] private UserUI _userUI;
    [SerializeField] private User _user;

    [SerializeField] private Button _addRewardButton;

    private void Start()
    {
        _userUI = GetComponent<UserUI>();
        _user = PersistentRoot.Instance.GetComponentInChildren<User>();
        _user.OnEnergyChanged += UpdateEnergyText;
        _user.OnCurrencyChanged += UpdateCurrencyText;
        _user.OnTimeToNextEnergyChanged += _userUI.SetTimeText;

        _userUI.SetTimeText(_user.GetTimeToNextEnergy());
        UpdateEnergyText(_user.GetEnergy());
        UpdateCurrencyText(_user.GetCurrency());

        Scene scene = SceneManager.GetActiveScene();
        if(scene.buildIndex == 0)
        {
            _addRewardButton.gameObject.SetActive(true);
        }
        else
        {
            _addRewardButton.gameObject.SetActive(false);
        }
    }

    private void UpdateEnergyText(int energy)
    {
        _userUI.SetEnergyText(energy);
    }

    private void UpdateCurrencyText(int currency)
    {
        _userUI.SetCurrencyText(currency);
    }
    private void OnDisable()
    {
        _user.OnEnergyChanged -= UpdateEnergyText;
        _user.OnCurrencyChanged -= UpdateCurrencyText;
        _user.OnTimeToNextEnergyChanged -= _userUI.SetTimeText;
    }
}