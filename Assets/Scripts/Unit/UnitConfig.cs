using UnityEngine;

[CreateAssetMenu(menuName = "Params/UnitData")]
public class UnitConfig : ScriptableObject
{
    public UnitType type;
    public int maxHP;
    public int attack;
    public int defense;
    public float penFlat;   // �ђʁi�Œ�l�j
    public float penPer;  // �ђʁi�����j
    public float moveSpeed;
}
