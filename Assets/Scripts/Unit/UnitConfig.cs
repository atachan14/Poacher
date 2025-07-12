using UnityEngine;

[CreateAssetMenu(menuName = "Params/UnitData")]
public class UnitConfig : ScriptableObject
{
    public UnitType type;
    public int maxHP;
    public int attack;
    public int defense;
    public float penFlat;   // 貫通（固定値）
    public float penPer;  // 貫通（割合）
    public float moveSpeed;
}
