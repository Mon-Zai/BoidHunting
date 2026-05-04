
using System;
using UnityEngine;

public class BoidSpawnStrategy : ISpawnStrategy
{
    private ObjectPool<Boid> _boidPool = null;
    private LayerMask _spawnLayer;
    private GameEventSO _boidCaughtEvent;

    public BoidSpawnStrategy(ObjectPool<Boid> boidPool, LayerMask spawnLayer, GameEventSO boidCaughtEvent)
    {
        _boidPool = boidPool;
        _spawnLayer = spawnLayer;
        _boidCaughtEvent = boidCaughtEvent;
    }
    public void Spawn(Vector3 position)
    {
        Boid boid = _boidPool.GetObject();
        position.y += 3.6f;
        boid.transform.position = position;
        boid.OnCaught += ReturnBoid;
        boid.OnCaught += OnVoidSpawn;
    }
    private void ReturnBoid(Boid boid)
    {
        Debug.Log($"Returning boid {boid.name} to pool.");
        boid.OnCaught -= ReturnBoid;
        boid.OnCaught -= OnVoidSpawn;
        _boidPool.ReturnObject(boid);


    }
    private void OnVoidSpawn(Boid boid)
    {
        _boidCaughtEvent.Raise();
    }
    public LayerMask SpawnLayer()
    {
        return _spawnLayer;
    }
}
