using UnityEngine;

public class OnSpotWeapon : BaseWeapon
{
    [SerializeField]GameObject attackSource;




    public override void Fire(Vector2 pos)
    {
        if (Time.time - lastFireTime < data.fireRate) return;
        lastFireTime = Time.time;

        if (!TryConsumeAmmo()) return;

        Vector2 dir = (pos - (Vector2)transform.position).normalized;

        Vector2 firePos = (Vector2)transform.position + dir * data.range;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Instantiate(attackSource, firePos, rotation, transform);
    }

}
