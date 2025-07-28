using UnityEngine;
using UnityEngine.Windows;

public class RaycastWeapon : BaseWeapon
{



    public override void Fire(Vector2 pos)
    {

        if (!RequireCheck()) return;

        Vector2 dir = (pos - (Vector2)transform.position).normalized;

        Vector2 firePos = (Vector2)transform.position + dir.normalized * 0.3f;

        ShowFireEffect(firePos);

        RaycastHit2D hit = Physics2D.Raycast(firePos, dir, wData.range, wData.hitMask);
        if (hit.collider != null)
        {
            // É_ÉÅÅ[ÉWèàóùÇ∆Ç©

            ShowHitEffect(hit.point);

            BaseDamageable damageable = hit.collider.GetComponentInParent<BaseDamageable>();
            if (damageable != null)
            {
                SendDamageData(damageable);
            }
        }

    }

    void ShowFireEffect(Vector2 firePos)
    {
        if (wData.fireEffect != null)
        {

            Instantiate(wData.fireEffect, firePos, Quaternion.identity);
        }
    }

    void ShowHitEffect(Vector2 hitPos)
    {
        if (wData.hitEffect != null)
            Instantiate(wData.hitEffect, hitPos, Quaternion.identity);
    }

}
