using TMPro;
using UnityEngine;

public class UserUI : MonoBehaviour
{
    [SerializeField] private  TextMeshProUGUI _energyText;
    [SerializeField] private TextMeshProUGUI _currencyText;
    [SerializeField] private TextMeshProUGUI _TimeText;

    public void SetEnergyText(int energy, int maxEnergy = 10)
    {
        if (_energyText == null)
            _energyText = transform.Find("EnergyText").GetComponent<TextMeshProUGUI>();

        _energyText.text = $"{energy} / {maxEnergy}";
    }
    public void SetCurrencyText(int currency)
    {
        if (_currencyText == null)
            _currencyText = transform.Find("CurrencyText").GetComponent<TextMeshProUGUI>();

        _currencyText.text = $"{currency}$";
    }
    public void SetTimeText(int seconds)
    {
        if (seconds <= 0)
        {
            _TimeText.text = "";
            return;
        }
        _TimeText.text = $"{seconds}s";
    }
}
