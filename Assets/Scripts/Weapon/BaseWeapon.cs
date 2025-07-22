using System.Collections;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public WeaponData data;
    protected WorldWeapon worldWeapon;
    public float currentAmmo;
    protected float lastFireTime;

    protected virtual void Start()
    {
       
        worldWeapon = GetComponentInChildren<WorldWeapon>(true);
        currentAmmo = data.startAmmo;
    }
    public abstract void Fire(Vector2 dir);

    protected bool RequireCheck()
    {
        if (Time.time - lastFireTime < data.fireRate) return false;
        lastFireTime = Time.time;

        if (!TryConsumeAmmo()) return false;

        return true;
    }


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
        transform.parent = DropItemManager.Instance.transform;
        worldWeapon.gameObject.SetActive(true);
        worldWeapon.MarkAsDropped();

        Vector2 start = transform.position;
        Vector2 dir = ((Vector2)pos - start).normalized;
        Vector2 target = start + dir * 0.3f;

        StartCoroutine(SlideToPosition(target, 0.1f)); // 0.1秒かけてスルッと
    }

    IEnumerator SlideToPosition(Vector2 targetPos, float duration)
    {
        Vector2 startPos = transform.position;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            transform.position = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }

        transform.position = targetPos; // 最終位置でピタッと止める
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
