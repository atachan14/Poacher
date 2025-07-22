using UnityEngine;

public class RamboParams : UnitParams
{

    [Header("Movement Settings")]
    public float maxSpeed = 10f;
    public float additiveAccel = 0.1f; // 絶対に加速する下限値
    public float multiplierAccel = 0.05f; // 乗算でブースト感

    [Header("Damping Settings")]
    public float idleDamping = 2f;
    public float normalDamping = 0f;
    public float reverseDamping = 3f;

    [Header("Debug")]
    public float currentSpeed;  // Inspectorに速度表示（ReadOnly属性は後述）

}

