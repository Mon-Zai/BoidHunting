using UnityEngine;

public class HunterFactory : Factory<Hunter>
{
    public HunterFactory(Hunter prefab, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
    }
    public override Hunter Create()
    {
        var hunter = base.Create();
        return hunter;
    }
}