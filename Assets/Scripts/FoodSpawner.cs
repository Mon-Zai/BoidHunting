using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _foodPrefab;
    [SerializeField] private float _spawnRadius = 10f;
    [SerializeField] private float _spawnInterval = 5f;
    private float _timer;

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
        Vector3 spawnPosition = transform.position + new Vector3(
            Random.Range(-_spawnRadius, _spawnRadius),
            3.6f,
            Random.Range(-_spawnRadius, _spawnRadius)
        );
        Instantiate(_foodPrefab, spawnPosition, Quaternion.identity);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }
}
