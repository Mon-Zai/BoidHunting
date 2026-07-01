using UnityEngine;

public class IdleState : HunterBaseState
{
  
    public IdleState(Hunter hunter) : base(hunter)
    {
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void Update()
    {
        base.Update();
        hunter.RegenerateStamina(hunter.Settings.StaminaRecoveryRate, Time.deltaTime);
    }
}