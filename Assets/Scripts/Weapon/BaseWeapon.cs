using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public WeaponData data;
    WorldWeapon worldWeapon;

    private void Start()
    {
        worldWeapon = GetComponentInChildren<WorldWeapon>(true);
    }
    public abstract void Fire(Vector2 dir);

    public void Drop(Vector2 pos)
    {
        transform.position = pos;
        transform.parent = DropItemManager.Instance.transform;
        worldWeapon.gameObject.SetActive(true);
    }

    protected void SendDamageData(BaseDamageable damageable)
    {
        damageable.TakeDamage(
                   rawDMG: data.baseDamage,
                   penFlat: data.penFlat,
                   penPer: data.penPer,
                   uType: GetComponentInParent<UnitParams>().Type
               );
    }
}
