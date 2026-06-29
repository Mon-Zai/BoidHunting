using System;
using System.Collections.Generic;
using UnityEngine;
using Pool;

public class Food : MonoBehaviour, IResetable, IPoolable, IInteractable
{
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private LayerMask _boidLayer;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private bool isPickedUp = false;
    [SerializeField] private Transform firstParent;
    private HashSet<Boid> _boidsInRange = new();
    public event Action<Food> OnConsumed;
    void Start()
    {
        firstParent = transform.parent;
    }
    void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _detectionRadius, _boidLayer);
        foreach (var collider in colliders)
        {
            Boid boid = collider.GetComponent<Boid>();
            if (boid == null) continue;
            Vector3 directionToBoid = boid.transform.position - transform.position;
            if (Physics.Raycast(transform.position, directionToBoid, directionToBoid.magnitude, _obstacleLayer))
                continue;
            boid.AddFoodTarget(this);
            _boidsInRange.Add(boid);
        }
    }
    public void RemoveBoid(Boid boid)
    {
        if (_boidsInRange.Contains(boid))
        {
            _boidsInRange.Remove(boid);
            boid.RemoveFoodTarget(this);
        }
    }
    public void BeConsumed()
    {
        foreach (var boid in _boidsInRange)
        {
            boid.RemoveFoodTarget(this);
        }
        AudioManager.Instance.Play(AudioManager.SoundType.EAT);
        OnConsumed?.Invoke(this);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
    public void Reset()
    {
        _boidsInRange.Clear();
    }
    public virtual void OnGetFromPool() { Reset(); gameObject.SetActive(true); }
    public virtual void OnReturnToPool() { gameObject.SetActive(false); }

    public void Interact(GameObject interactor)
    {
        if (isPickedUp)
        {
            isPickedUp = false;
            transform.SetParent(firstParent);
        }
        else
        {
            isPickedUp = true;
            transform.SetParent(interactor.transform.Find("HoldPoint"));
            transform.localPosition = Vector3.zero;
            AudioManager.Instance.Play(AudioManager.SoundType.PICKUP);
        }
    }
}
