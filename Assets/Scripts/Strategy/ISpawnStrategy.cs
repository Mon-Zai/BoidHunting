
using UnityEngine;

public interface ISpawnStrategy
{
    void Spawn(Vector3 position);
    LayerMask SpawnLayer();
}
