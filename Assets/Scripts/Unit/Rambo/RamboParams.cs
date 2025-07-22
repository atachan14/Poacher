using UnityEngine;

public class RamboParams : UnitParams
{

    [Header("Movement Settings")]
    public float maxSpeed = 10f;
    public float additiveAccel = 0.1f; // ��΂ɉ������鉺���l
    public float multiplierAccel = 0.05f; // ��Z�Ńu�[�X�g��

    [Header("Damping Settings")]
    public float idleDamping = 2f;
    public float normalDamping = 0f;
    public float reverseDamping = 3f;

    [Header("Debug")]
    public float currentSpeed;  // Inspector�ɑ��x�\���iReadOnly�����͌�q�j

}

