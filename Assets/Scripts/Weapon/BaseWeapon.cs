using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public WeaponData data;
    protected WorldWeapon worldWeapon;
    public float currentAmmo;

    protected virtual void Start()
    {
        worldWeapon = GetComponentInChildren<WorldWeapon>(true);
        currentAmmo = data.startAmmo;
    }
    public abstract void Fire(Vector2 dir);

    public virtual bool TryConsumeAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            return true;
        }
        else 
        {
            PlayNoAmmoEffect();
            return false; 
        }
    }

    public virtual void PlayNoAmmoEffect()
    {

    }

    public virtual void Drop(Vector2 pos)
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
