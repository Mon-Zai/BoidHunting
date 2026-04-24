using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boid : AIEntity<BoidSettings>
{
    private BoidManager manager;
    private float currentPersuedCooldown = 0f;
    public BoidSettings Settings => _settings;
    private HashSet<Food> _foods = new();
    private Hunter _hunterTarget;
    public Hunter HunterTarget => _hunterTarget;
    public Food FoodTarget => _foods.OrderBy(f => Vector3.Distance(transform.position, f.transform.position)).FirstOrDefault();
    public bool NeighborsInRange => manager.GetNeighbors(this, Settings.NeighborRadius).Count > 0;
    public event Action<Boid> OnCaught;
    public void Init(BoidManager boidManager)
    {
        manager = boidManager;
    }
    protected override void Update()
    {
        base.Update();
        PursuedTimer();
        EatFood();
        manager.CheckBounds(this);
    }
    private void PursuedTimer()
    {
        if (currentPersuedCooldown > 0f)
        {
            currentPersuedCooldown -= Time.deltaTime;
            if (currentPersuedCooldown <= 0f)
            {
                _hunterTarget = null;
            }
        }
    }
    public List<Boid> GetNeighbors()
    {
        return manager.GetNeighbors(this, Settings.NeighborRadius);
    }
    public void AddFoodTarget(Food food)
    {
        _foods.Add(food);
    }
    public void EatFood()
    {
        Food foodTarget = FoodTarget;
        if (foodTarget == null) return;
        if (Vector3.Distance(transform.position, foodTarget.transform.position) <= Settings.ConsumeRadius)
        {
            foodTarget.BeConsumed();
        }
    }
    public void RemoveFoodTarget(Food food)
    {
        _foods.Remove(food);
    }
    public void SetHunterTarget(Hunter hunter)
    {
        _hunterTarget = hunter;
        currentPersuedCooldown = Settings.persuedCooldown;
    }
    public void GetCaught()
    {
        manager.RemoveBoid(this);
        OnCaught?.Invoke(this);
    }
    public override void Reset()
    {
        _hunterTarget = null;
        _foods.Clear();
        base.Reset();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Settings.NeighborRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Settings.SlowingRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Settings.WanderRadius);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, Settings.SeparationRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Settings.ConsumeRadius);
        if (FoodTarget != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, FoodTarget.transform.position);
        }
        if (HunterTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, HunterTarget.transform.position);
        }
    }
}
