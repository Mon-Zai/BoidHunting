
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BoidSpawnStrategy : ISpawnStrategy
{
    private ObjectPool<Boid> _boidPool = null;
    public BoidSpawnStrategy(ObjectPool<Boid> boidPool)
    {
        _boidPool = boidPool;
    }
    public void Spawn(Vector3 position)
    {
        Boid boid = _boidPool.GetObject();
        boid.transform.position = position;
        boid.OnCaught += ReturnBoid;
    }
    private void ReturnBoid(Boid boid)
    {
        Debug.Log($"Returning boid {boid.name} to pool.");
        boid.OnCaught -= ReturnBoid;
        _boidPool.ReturnObject(boid);
    }
}
