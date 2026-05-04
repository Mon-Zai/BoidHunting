using System;
using UnityEngine;

public class DefeatCondition : MonoBehaviour
{
    [SerializeField] private GameEventSO foodEatenEvent;
    [SerializeField] private GameEventSO boidCaughtEvent;
    [SerializeField] private int maxCaughtBoids = 10;
    [SerializeField] private int maxFoodEaten = 5;
    private int caughtBoids = 0;
    private int foodEaten = 0;
    public event Action<string> OnDefeatEvent;
    void Start()
    {
        foodEatenEvent.Subscribe(IncrementFoodEaten);
        boidCaughtEvent.Subscribe(IncrementCaughtBoids);
    }
    public void IncrementFoodEaten()
    {
        foodEaten++;
        CheckDefeat();
    }
    public void IncrementCaughtBoids()
    {
        caughtBoids++;
        CheckDefeat();
    }
    void CheckDefeat()
    {
        Debug.Log("Caught Boids: " + caughtBoids + "/" + maxCaughtBoids);
        Debug.Log("Food Eaten: " + foodEaten + "/" + maxFoodEaten);
        if (caughtBoids >= maxCaughtBoids)
        {
            OnDefeat("Boids");
        }
        else if (foodEaten >= maxFoodEaten)
        {
            OnDefeat("Food");
        }
    }
    void OnDefeat(string reason)
    {
        OnDefeatEvent?.Invoke(reason);
    }
    void OnDisable()
    {
        foodEatenEvent.Unsubscribe(IncrementFoodEaten);
        boidCaughtEvent.Unsubscribe(IncrementCaughtBoids);
    }
}
