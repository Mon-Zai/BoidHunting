using System;
using UnityEngine;

public class VictoryCondition : MonoBehaviour
{
    [SerializeField] private GameEventSO victoryEvent;
    [SerializeField] public int CurrencyReward = 1;
    public event Action OnVictoryEvent;
    private void Start()
    {
        victoryEvent.Subscribe(OnVictory);
    }
    private void OnVictory()
    {
        Debug.Log("Victory!");
        OnVictoryEvent?.Invoke();
    }
    void OnDisable()
    {
        victoryEvent.Unsubscribe(OnVictory);
    }
}