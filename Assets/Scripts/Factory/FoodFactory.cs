using UnityEngine;

public class FoodFactory : Factory<Food>
{
    public FoodFactory(Food prefab, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
    }
}