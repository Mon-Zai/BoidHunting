using UnityEngine;

public class IdleState : HunterBaseState
{
  
    public IdleState(Hunter hunter) : base(hunter)
    {
    }
    public override void Update()
    {
        base.Update();
        staminaRecovery();
    }
    public void staminaRecovery()
    {
        if (hunter.Stamina >= hunter.Settings.MaxStamina) return;
        hunter.Stamina += Time.deltaTime * hunter.Settings.StaminaRecoveryRate;
        hunter.Stamina = Mathf.Clamp(hunter.Stamina, 0f, hunter.Settings.MaxStamina);
    }
}