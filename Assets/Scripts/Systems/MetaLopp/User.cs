using System;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    [SerializeField] private string _userName = string.Empty;
    [SerializeField] private int _energy = 10;
    [SerializeField] private int _maxEnergy = 10;
    [SerializeField] private int _currency = 0;
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private int timeToNextEnergy = 0;
    [SerializeField] private List<int> _ownedShopItemIds = new List<int>();

    public Action<int> OnEnergyChanged;
    public Action<int> OnCurrencyChanged;
    public Action<int> OnTimeToNextEnergyChanged;
    public Action<int> OnMaxEnergyChanged;
    public Action OnOwnedShopItemsChanged;

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

    public void SetMaxEnergy(int maxEnergy)
    {
        _maxEnergy = Mathf.Max(1, maxEnergy);
        _energy = Mathf.Clamp(_energy, 0, _maxEnergy);
        OnEnergyChanged?.Invoke(_energy);
        OnMaxEnergyChanged?.Invoke(_maxEnergy);
    }

    public bool OwnsShopItem(int itemId)
    {
        return _ownedShopItemIds.Contains(itemId);
    }

    public IReadOnlyList<int> GetOwnedShopItemIds()
    {
        return _ownedShopItemIds;
    }

    public bool TryBuyShopItem(int itemId, int price)
    {
        if (OwnsShopItem(itemId))
        {
            return false;
        }

        if (_currency < price)
        {
            return false;
        }

        SubtractCurrency(price);
        _ownedShopItemIds.Add(itemId);
        OnOwnedShopItemsChanged?.Invoke();
        return true;
    }

    public bool AddOwnedShopItem(int itemId)
    {
        if (OwnsShopItem(itemId))
        {
            return false;
        }

        _ownedShopItemIds.Add(itemId);
        OnOwnedShopItemsChanged?.Invoke();
        return true;
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
            TimeToNextEnergy = timeToNextEnergy,
            OwnedShopItemIds = new List<int>(_ownedShopItemIds)
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
        _ownedShopItemIds = data.OwnedShopItemIds != null
            ? new List<int>(data.OwnedShopItemIds)
            : new List<int>();

        if (notifyEvents)
        {
            OnEnergyChanged?.Invoke(_energy);
            OnCurrencyChanged?.Invoke(_currency);
            OnTimeToNextEnergyChanged?.Invoke(timeToNextEnergy);
            OnOwnedShopItemsChanged?.Invoke();
        }
    }
}