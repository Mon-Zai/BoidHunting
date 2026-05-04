using UnityEngine;

public class IUIStrategySelector : MonoBehaviour
{
    [SerializeField] private SpawnSystem spawnSystem;

    public void SetHunterStrategy() => spawnSystem.SetStrategy(StrategyType.Hunter);
    public void SetBoidStrategy()   => spawnSystem.SetStrategy(StrategyType.Boid);
    public void SetFoodStrategy()   => spawnSystem.SetStrategy(StrategyType.Food);
}
