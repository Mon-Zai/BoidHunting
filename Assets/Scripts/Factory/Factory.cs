using UnityEngine;

public class Factory<T> where T : MonoBehaviour
{
    protected T _prefab = null;
    protected Transform _parent = null;
    public virtual T Create()
    {
        return Object.Instantiate(_prefab, _parent);
    }
}