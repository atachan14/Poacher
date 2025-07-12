using UnityEngine;
using UnityEngine.Windows;

public class SubmachineGun : BaseWeapon
{


    float lastFireTime;

    public override void Fire(Vector2 pos)
    {
        if (Time.time - lastFireTime < data.fireRate) return;
        lastFireTime = Time.time;

        Vector2 dir = (pos - (Vector2)transform.position).normalized;

        Vector2 firePos = (Vector2)transform.position + dir.normalized * 0.3f;

        ShowFireEffect(firePos);

        RaycastHit2D hit = Physics2D.Raycast(firePos, dir, data.range, data.hitMask);
        if (hit.collider != null)
        {
            // ƒ_ƒ[ƒWˆ—‚Æ‚©

            ShowHitEffect(hit.point);

            BaseDamageable damageable = hit.collider.GetComponentInParent<BaseDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(
                    rawDMG: data.baseDamage,
                    penFlat: data.penFlat,
                    penPer: data.penPer
                );
            }
        }

    }

    void ShowFireEffect(Vector2 firePos)
    {
        if (data.fireEffect != null)
        {

            Instantiate(data.fireEffect, firePos, Quaternion.identity);
        }
    }

    void ShowHitEffect(Vector2 hitPos)
    {
        if (data.hitEffect != null)
            Instantiate(data.hitEffect, hitPos, Quaternion.identity);
    }

}
