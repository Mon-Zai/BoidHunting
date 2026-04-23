using UnityEngine;

public class BoidFactory : Factory<Boid>
{
    BoidManager _manager = null;
    public BoidFactory(Boid prefab, BoidManager manager)
    {
        _prefab = prefab; 
        _manager = manager;
        _parent = manager.transform;
    }
    public override Boid Create()
    {
        var boid = base.Create();
        _manager.AddBoid(boid);
        boid.Init(_manager);
        return boid;
    }
}