using UnityEngine;

public class HunterSpawnStrategy : ISpawnStrategy
{
    private ObjectPool<Hunter> _hunterPool = null;
    private Transform[] _waypoints = null;
    public HunterSpawnStrategy(ObjectPool<Hunter> hunterPool)
    {
        _hunterPool = hunterPool;
    }
    public void Spawn(Vector3 position)
    {
        if(_waypoints == null)
        {
            Debug.LogError("Waypoints not set for HunterSpawnStrategy");
            return;
        }
        Hunter hunter = _hunterPool.GetObject();
        hunter.SetPatrolWaypoints(_waypoints);
        hunter.transform.position = position;
    }
    public void SetWaypoints(Transform[] waypoints)
    {
        _waypoints = waypoints;
    }
}