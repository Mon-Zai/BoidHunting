using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum StrategyType
{
    Hunter,
    Boid,
    Food
}
public class ClickSpawner : MonoBehaviour
{
    [SerializeField] InputReader inputReader = null;
    [SerializeField] Hunter hunterPrefab = null;
    [SerializeField] Transform hunterParent = null;
    [SerializeField] Boid boidPrefab = null;
    [SerializeField] BoidManager boidManager = null;
    [SerializeField] Food foodPrefab = null;
    [SerializeField] Transform foodParent = null;
    private Factory<Hunter> _hunterFactory = null;
    private Factory<Boid> _boidFactory = null;
    private Factory<Food> _foodFactory = null;
    private ObjectPool<Hunter> _hunterPool = null;
    private ObjectPool<Boid> _boidPool = null;
    private ObjectPool<Food> _foodPool = null;
    private Dictionary<StrategyType, ISpawnStrategy> _spawnStrategies = new();
    private ISpawnStrategy _currentStrategy = null;
    void Awake()
    {
        _hunterFactory = new HunterFactory(hunterPrefab, hunterParent);
        _boidFactory = new BoidFactory(boidPrefab, boidManager);
        _foodFactory = new FoodFactory(foodPrefab, foodParent);

        _hunterPool = new ObjectPool<Hunter>(_hunterFactory.Create, Entity.TurnOnOff);
        _boidPool = new ObjectPool<Boid>(_boidFactory.Create, Entity.TurnOnOff);
        _foodPool = new ObjectPool<Food>(_foodFactory.Create, Food.TurnOnOff);

        _spawnStrategies[StrategyType.Hunter] = new HunterSpawnStrategy(_hunterPool);
        _spawnStrategies[StrategyType.Boid] = new BoidSpawnStrategy(_boidPool);
        _spawnStrategies[StrategyType.Food] = new FoodSpawnStrategy(_foodPool);

        SetSpawnStrategy(StrategyType.Boid);

        inputReader.SpawnStartedEvent += ClickToSpawn;

    }
    public void ClickToSpawn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Ground") _currentStrategy.Spawn(hit.point);
        }
        else
        {
            Debug.Log("No hit detected");
        }
    }
    public void SetSpawnStrategy(StrategyType strategyType)
    {
        if (_spawnStrategies.TryGetValue(strategyType, out var strategy))
        {
            _currentStrategy = strategy;
        }
    }
    void OnDisable()
    {
        inputReader.SpawnStartedEvent -= ClickToSpawn;
    }
}