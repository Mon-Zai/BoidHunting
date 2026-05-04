
using System;
using UnityEngine;

public class FoodSpawnStrategy : ISpawnStrategy
{
    private ObjectPool<Food> _foodPool = null;
    private LayerMask _spawnLayer;
    private GameEventSO _foodConsumedEvent;
    public FoodSpawnStrategy(ObjectPool<Food> foodPool, LayerMask spawnLayer, GameEventSO foodConsumedEvent)
    {
        _foodPool = foodPool;
        _spawnLayer = spawnLayer;
        _foodConsumedEvent = foodConsumedEvent;
    }
    public void Spawn(Vector3 position)
    {
        Food food = _foodPool.GetObject();
        position.y += 1.6f;
        food.transform.position = position;
        food.OnConsumed += ReturnFood;
        food.OnConsumed += OnVoidSpawn;
    }
    private void ReturnFood(Food food)
    {
        Debug.Log($"Returning food {food.name} to pool.");
        _foodPool.ReturnObject(food);
        food.OnConsumed -= ReturnFood;
        food.OnConsumed -= OnVoidSpawn;
    }
    private void OnVoidSpawn(Food food)
    {
        _foodConsumedEvent.Raise();
    }
    public LayerMask SpawnLayer()
    {
        return _spawnLayer;
    }
}
