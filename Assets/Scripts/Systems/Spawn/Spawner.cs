using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour, IInteractable
{
    [SerializeField] Transform defaultSpawnPoint;
    [SerializeField] private StrategyType _spawnType;
    [SerializeField] private SpawnSystem _spawnSystem;
    [SerializeField] private int _spawnLimit;
    [SerializeField] private int _currentSpawnCount = 0;
    [SerializeField] TextMeshPro spawnCountText;
    [SerializeField] TextMeshPro maxSpawnText;

    void Start()
    {
        if (maxSpawnText != null)
        {
            maxSpawnText.text = "Max\n" + _spawnLimit.ToString();
        }
        if (_currentSpawnCount == 0) return;
        for (int i = 0; i < _currentSpawnCount; i++)
        {
            _spawnSystem.SetStrategy(_spawnType);
            _spawnSystem.TrySpawn(defaultSpawnPoint.position, gameObject.layer);
        }
        UpdateUI();
    }
    public void Interact(GameObject interactor)
    {
        if (_currentSpawnCount >= _spawnLimit)
        {
            Debug.Log("Spawn limit reached");
            return;
        }
        _spawnSystem.SetStrategy(_spawnType);
        if (interactor.TryGetComponent(out InteractionHandler interactionController))
        {
            _spawnSystem.TrySpawn(interactionController.InteractionPoint, gameObject.layer);
            _currentSpawnCount++;
        }
        else
        {
            _spawnSystem.TrySpawn(defaultSpawnPoint.position, gameObject.layer);
            _currentSpawnCount++;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (spawnCountText != null)
        {
            spawnCountText.text = "Current Spawned\n" + _currentSpawnCount.ToString();
        }
    }
}
