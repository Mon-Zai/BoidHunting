using System;
using UnityEngine;

public class User : MonoBehaviour
{
    [SerializeField] private string _userName = string.Empty;
    [SerializeField] private int _energy = 10;
    [SerializeField] private int _maxEnergy = 10;
    [SerializeField] private int _currency = 0;
    [SerializeField] private int _currentLevel = 1;

    [SerializeField] private int timeToNextEnergy = 0;

    public Action<int> OnEnergyChanged;
    public Action<int> OnCurrencyChanged;
    public Action<int> OnTimeToNextEnergyChanged;
    public void SetUserName(string userName)
    {
        _userName = userName;
    }
    public void AddEnergy(int amount)
    {
        _energy = Mathf.Min(_energy + amount, _maxEnergy);
        if (_energy == _maxEnergy)
        {
            SetTimeToNextEnergy(0);
        }
        OnEnergyChanged?.Invoke(_energy);
    }
    public void SubtractEnergy(int amount)
    {
        _energy = Mathf.Max(_energy - amount, 0);
        OnEnergyChanged?.Invoke(_energy);
    }
    public void AddCurrency(int amount)
    {
        _currency += amount;
        OnCurrencyChanged?.Invoke(_currency);
    }
    public void SubtractCurrency(int amount)
    {
        _currency = Mathf.Max(_currency - amount, 0);
        OnCurrencyChanged?.Invoke(_currency);
    }
    public int GetEnergy()
    {
        return _energy;
    }
    public int GetCurrency()
    {
        return _currency;
    }
    public int GetMaxEnergy()
    {
        return _maxEnergy;
    }
    public void SetTimeToNextEnergy(int seconds)
    {
        timeToNextEnergy = seconds;
        OnTimeToNextEnergyChanged?.Invoke(timeToNextEnergy);
    }
    public int GetTimeToNextEnergy()
    {
        return timeToNextEnergy;
    }
    public UserData ToData()
    {
        return new UserData
        {
            UserName = _userName,
            Energy = _energy,
            MaxEnergy = _maxEnergy,
            Currency = _currency,
            CurrentLevel = _currentLevel,
            TimeToNextEnergy = timeToNextEnergy
        };
    }

    public void LoadFromData(UserData data, bool notifyEvents = true)
    {
        if (data == null) return;

        _userName = data.UserName;
        _energy = data.Energy;
        _maxEnergy = data.MaxEnergy;
        _currency = data.Currency;
        _currentLevel = data.CurrentLevel;
        timeToNextEnergy = data.TimeToNextEnergy;

        if (notifyEvents)
        {
            OnEnergyChanged?.Invoke(_energy);
            OnCurrencyChanged?.Invoke(_currency);
            OnTimeToNextEnergyChanged?.Invoke(timeToNextEnergy);
        }
    }
}
