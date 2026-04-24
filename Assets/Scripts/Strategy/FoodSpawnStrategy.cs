
using UnityEngine;

public class FoodSpawnStrategy : ISpawnStrategy
{
    private ObjectPool<Food> _foodPool = null;
    private LayerMask _spawnLayer;
    public FoodSpawnStrategy(ObjectPool<Food> foodPool, LayerMask spawnLayer)
    {
        _foodPool = foodPool;
        _spawnLayer = spawnLayer;
    }
    public void Spawn(Vector3 position)
    {
        Food food = _foodPool.GetObject();
        position.y += 3.6f;
        food.transform.position = position;
        food.OnConsumed += ReturnFood;
    }
    private void ReturnFood(Food food)
    {
        Debug.Log($"Returning food {food.name} to pool.");
        _foodPool.ReturnObject(food);
        food.OnConsumed -= ReturnFood;
    }
    public LayerMask SpawnLayer()
    {
        return _spawnLayer;
    }
}
