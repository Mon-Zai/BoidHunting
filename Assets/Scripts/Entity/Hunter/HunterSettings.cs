using UnityEngine;

[CreateAssetMenu(fileName = "HunterSettings", menuName = "AI/Hunter Settings")]
public class HunterSettings : AISettings
{
    [Header("Hunter Attributes")]
    public float MaxStamina = 100f;
    public float StaminaConsumptionRate = 10f;
    public float StaminaRecoveryRate = 5f;
    public float DetectionRange = 10f;
    [Range(0f, 180f)]
    public float FieldOfViewAngle = 120f;

}
