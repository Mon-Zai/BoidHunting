using System;
using System.Collections.Generic;
using Pool;
using UnityEngine;
public class ObjectPool<T> where T : IPoolable
{
    private Func<T> _factoryMethod = null;
    private Queue<T> _objects = new();

    private bool _dynamic = true;

    public ObjectPool(Func<T> factoryMethod, int initialPoolSize = 10, bool dynamic = true)
    {
        _factoryMethod = factoryMethod;
        _dynamic = dynamic;
        for (int i = 0; i < initialPoolSize; i++)
        {
            T obj = _factoryMethod();
            obj.OnReturnToPool();
            _objects.Enqueue(obj);
        }
    }

    public T GetObject()
    {
        var result = default(T);

        if (_objects.Count > 0)
        {
            result = _objects.Dequeue();
        }
        else if (_dynamic)
        {
            result = _factoryMethod();
        }
        result.OnGetFromPool();
        Debug.Log($"Pool of {typeof(T).Name} has {_objects.Count} objects left.");
        return result;
    }

    public void ReturnObject(T obj)
    {
        obj.OnReturnToPool();
        _objects.Enqueue(obj);
    }
}
