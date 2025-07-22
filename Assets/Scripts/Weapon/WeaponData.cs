using UnityEngine;


[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite icon;
    public GameObject fireEffect;
    public GameObject hitEffect;

    public int baseDamage;
    public int penFlat;
    public int penPer;

    public float fireRate;
    public float range;
    public float startTime;
    public float lifetime;
    public float startAmmo;

    public LayerMask hitMask;
    
}
