using UnityEngine;
using UnityEngine.InputSystem;

public class ClickSpawner : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private SpawnSystem spawnSystem;

    void Awake() => inputReader.SpawnStartedEvent += ClickToSpawn;

    void OnDisable() => inputReader.SpawnStartedEvent -= ClickToSpawn;

    public void ClickToSpawn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Pointer.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            spawnSystem.TrySpawn(hit.point, hit.collider.gameObject.layer);
        else
            Debug.Log("No hit detected");
    }
}