using UnityEngine;

public class AttackSource : MonoBehaviour
{
    [SerializeField] float lifetime = 0.1f;
    public int Damage { get; set; }
    public int PenFlat {  get; set; }
    public int PenPer {  get; set; }

    private void Awake()
    {
        WeaponData data = GetComponentInParent<BaseWeapon>().data;
        Damage = data.baseDamage;
        PenFlat = data.penFlat;
        PenPer = data.penPer;
    }


    void Start()
    {

        Destroy(gameObject, lifetime);
    }

}
