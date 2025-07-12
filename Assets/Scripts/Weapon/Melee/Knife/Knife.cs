using UnityEngine;

public class Knife : BaseWeapon
{
    [SerializeField]GameObject attackSource;
    
    float lastFireTime;


    public override void Fire(Vector2 pos)
    {
        if (Time.time - lastFireTime < data.fireRate) return;
        lastFireTime = Time.time;

        Vector2 dir = (pos - (Vector2)transform.position).normalized;

        Vector2 firePos = (Vector2)transform.position + dir.normalized * 0.5f;

        Instantiate(attackSource, firePos, Quaternion.identity,transform);
    }
}
