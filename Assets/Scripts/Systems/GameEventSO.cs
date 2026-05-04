using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/GameEvent")]
public class GameEventSO : ScriptableObject
{
    private event Action _onRaised;

    public void Raise() => _onRaised?.Invoke();
    public void Subscribe(Action listener) => _onRaised += listener;
    public void Unsubscribe(Action listener) => _onRaised -= listener;
}