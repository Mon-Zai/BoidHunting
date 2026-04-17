using UnityEngine;

[CreateAssetMenu(fileName = "HunterSettings", menuName = "AI/Hunter Settings")]
public class HunterSettings : AISettings
{
    [Header("Hunter Attributes")]
    public float MaxStamina = 100f;
    public float StaminaConsumptionRate = 10f;
    public float StaminaRecoveryRate = 5f;
    public float DetectionRange = 10f;
    public float KillRange = 1f;
    public float KillStaminaCost = 20f;
    [Range(0f, 360f)]
    public float FieldOfViewAngle = 120f;
}
