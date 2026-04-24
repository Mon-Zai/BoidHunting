using UnityEngine;

public class HunterSpawnStrategy : ISpawnStrategy
{
    private ObjectPool<Hunter> _hunterPool = null;
    private LayerMask _spawnLayer;
    public HunterSpawnStrategy(ObjectPool<Hunter> hunterPool, LayerMask spawnLayer)
    {
        _hunterPool = hunterPool;
        _spawnLayer = spawnLayer;
    }
    public void Spawn(Vector3 position)
    {
        Hunter hunter = _hunterPool.GetObject();
        position.y += 3.6f;
        hunter.transform.position = position;
    }

    public LayerMask SpawnLayer()
    {
        return _spawnLayer;
    }
}