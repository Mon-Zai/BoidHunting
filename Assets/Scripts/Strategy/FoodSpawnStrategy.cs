
using UnityEngine;

public class FoodSpawnStrategy : ISpawnStrategy
{
    private ObjectPool<Food> _foodPool = null;
    public FoodSpawnStrategy(ObjectPool<Food> foodPool)
    {
        _foodPool = foodPool;

    }
    public void Spawn(Vector3 position)
    {
        Food food = _foodPool.GetObject();
        food.transform.position = position;
    }
}
