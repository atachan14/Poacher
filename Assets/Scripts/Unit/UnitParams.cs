using UnityEngine;
public enum UnitType
{
    Rambo,
    Poacher,
    Animal,
    Fence,
   
}
public class UnitParams : MonoBehaviour
{
    [SerializeField] UnitConfig config;

    public UnitType Type { get; set; }
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
        Type = config.type;
        MaxHP = config.maxHP;
        CurrentHP = MaxHP;

        Attack = config.attack;
        Defense = config.defense;
        PenFlat = config.penFlat;
        PenPer = config.penPer;
        MoveSpeed = config.moveSpeed;
    }

   
}
