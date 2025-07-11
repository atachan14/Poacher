using UnityEngine;

public class UnitParams : MonoBehaviour
{
    [SerializeField] UnitConfig uData;

    public int MaxHP { get; private set; }
    public int CurrentHP { get; set; }

    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public float PenFlat { get; private set; }
    public float PenPer { get; private set; }
    public float MoveSpeed { get; private set; }


    void Awake()
    {
        // ScriptableObjectから値をコピー
        MaxHP = uData.maxHP;
        CurrentHP = MaxHP;

        Attack = uData.attack;
        Defense = uData.defense;
        PenFlat = uData.penFlat;
        PenPer = uData.penPer;
        MoveSpeed = uData.moveSpeed;
    }

   
}
