using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject _foodPrefab;
    [SerializeField] private float _spawnRadius = 10f;
    [SerializeField] private float _spawnInterval = 5f;
    [Header("Obstacle Check")]
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private float _checkRadius = 3.5f;
    private float _timer;
    private Vector3 _spawnPos;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnInterval)
        {
            SpawnFood();
            _timer = 0f;
        }
    }

    private void SpawnFood()
    {
        _spawnPos = transform.position + new Vector3(
            Random.Range(-_spawnRadius, _spawnRadius),
            3.6f,
            Random.Range(-_spawnRadius, _spawnRadius)
        );
        if (!Physics.CheckSphere(_spawnPos, _checkRadius, _obstacleLayer))
        {
            Instantiate(_foodPrefab, _spawnPos, Quaternion.identity);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_spawnPos, _checkRadius);
    }
}
