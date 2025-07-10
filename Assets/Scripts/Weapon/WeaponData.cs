using UnityEngine;


[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite icon;
    public GameObject prefab;
    public int baseDamage;
    public float attackRate;
    // �D���ȃp�����[�^�������ɒǉ����Ă���OK
}
