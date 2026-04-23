using System;
using System.Collections.Generic;
using UnityEngine;
public class ObjectPool<T>
{
    private Func<T> _factoryMethod = null;
    private Action<T, bool> _turnOnOffCallback = null;

    private Queue<T> _objects = new();

    private bool _dynamic = true;

    public ObjectPool(Func<T> factoryMethod, Action<T, bool> turnOnOffCallback, int initialPoolSize = 10, bool dynamic = true)
    {
        _factoryMethod = factoryMethod;
        _turnOnOffCallback = turnOnOffCallback;
        _dynamic = dynamic;
        for (int i = 0; i < initialPoolSize; i++)
        {
            T obj = _factoryMethod();
            _turnOnOffCallback(obj, false);
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

        _turnOnOffCallback(result, true);
        Debug.Log($"Pool of {typeof(T).Name} has {_objects.Count} objects left.");
        return result;
    }

    public void ReturnObject(T obj)
    {
        _turnOnOffCallback(obj, false);
        _objects.Enqueue(obj);
    }
}
