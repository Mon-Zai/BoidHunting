using System.Collections.Generic;
using UnityEngine;

public enum StrategyType
{
    Hunter,
    Boid,
    Food
}

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private Hunter hunterPrefab;
    [SerializeField] private LayerMask hunterSpawnLayer;
    [SerializeField] private Transform hunterParent;
    [SerializeField] private Boid boidPrefab;
    [SerializeField] private BoidManager boidManager;
    [SerializeField] GameEventSO boidCaughtEvent;
    [SerializeField] private Food foodPrefab;
    [SerializeField] GameEventSO foodEatenEvent;
    [SerializeField] private Transform foodParent;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private StrategyType _initialStrategyType = StrategyType.Hunter;

    private ISpawnStrategy _currentStrategy;
    private Dictionary<StrategyType, ISpawnStrategy> _spawnStrategies = new();


    void Awake()
    {
        var hunterFactory = new HunterFactory(hunterPrefab, hunterParent);
        var boidFactory = new BoidFactory(boidPrefab, boidManager);
        var foodFactory = new FoodFactory(foodPrefab, foodParent);

        var hunterPool = new ObjectPool<Hunter>(hunterFactory.Create);
        var boidPool = new ObjectPool<Boid>(boidFactory.Create);
        var foodPool = new ObjectPool<Food>(foodFactory.Create);


        _spawnStrategies[StrategyType.Food] = new FoodSpawnStrategy(foodPool, groundLayer, foodEatenEvent);
        _spawnStrategies[StrategyType.Hunter] = new HunterSpawnStrategy(hunterPool, hunterSpawnLayer);
        _spawnStrategies[StrategyType.Boid] = new BoidSpawnStrategy(boidPool, groundLayer, boidCaughtEvent);
        SetStrategy(_initialStrategyType);
    }

    public void TrySpawn(Vector3 position, int layer)
    {
        if ((_currentStrategy.SpawnLayer() & (1 << layer)) != 0)
            _currentStrategy.Spawn(position);
        else
            Debug.Log("Hit detected but on wrong layer");
    }

    public void SetStrategy(StrategyType strategyType)
    {
        if (_spawnStrategies.TryGetValue(strategyType, out var strategy))
            _currentStrategy = strategy;
    }
}