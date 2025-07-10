using UnityEngine;

public class MachineGun : BaseWeapon
{
    [SerializeField] GameObject fireEffect;
    [SerializeField] GameObject hitEffect;

    public float fireRate = 0.1f;
    public float range = 10f;
    [SerializeField] LayerMask hitMask;

    float lastFireTime;

    public override void Fire(Vector2 dir)
    {
        if (Time.time - lastFireTime < fireRate) return;
        lastFireTime = Time.time;

        Vector2 firePos = (Vector2)transform.position + dir.normalized * 0.5f;

        ShowFireEffect(firePos);

        RaycastHit2D hit = Physics2D.Raycast(firePos, dir, range, hitMask);
        if (hit.collider != null)
        {
            // ƒ_ƒ[ƒWˆ—‚Æ‚©
            Debug.Log($"Hit {hit.collider.name} at {hit.point}");
            ShowHitEffect(hit.point);
        }
        
    }

    void ShowFireEffect(Vector2 firePos)
    {
        if (fireEffect != null)
        {
            
            Instantiate(fireEffect, firePos, Quaternion.identity);
        }
    }

    void ShowHitEffect(Vector2 hitPos)
    {
        if (hitEffect != null)
            Instantiate(hitEffect, hitPos, Quaternion.identity);
    }

}
