
using UnityEngine;

public class BoidSpawnStrategy : ISpawnStrategy
{
    private ObjectPool<Boid> _boidPool = null;
    private LayerMask _spawnLayer;
    public BoidSpawnStrategy(ObjectPool<Boid> boidPool, LayerMask spawnLayer)
    {
        _boidPool = boidPool;
        _spawnLayer = spawnLayer;
    }
    public void Spawn(Vector3 position)
    {
        Boid boid = _boidPool.GetObject();
        position.y += 3.6f;
        boid.transform.position = position;
        boid.OnCaught += ReturnBoid;
    }
    private void ReturnBoid(Boid boid)
    {
        Debug.Log($"Returning boid {boid.name} to pool.");
        boid.OnCaught -= ReturnBoid;
        _boidPool.ReturnObject(boid);
    }
    public LayerMask SpawnLayer()
    {
        return _spawnLayer;
    }
}
