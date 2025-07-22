using UnityEngine;

public class OnUnitWeapon : BaseWeapon
{
    [SerializeField] GameObject attackSource;

    public Vector2 TargetPos { get; private set; }

    public override void Fire(Vector2 pos)
    {
        if (!RequireCheck()) return;

        TargetPos = pos;

        Vector2 dir = (pos - (Vector2)transform.position).normalized;
        Vector2 firePos = (Vector2)transform.position + dir * 0.5f;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        var obj = Instantiate(attackSource, firePos, rotation,transform);
        var asComp = obj.GetComponent<BaseAS>();
        if (asComp != null)
        {
            asComp.Setup(this, pos); // –¾Ž¦“I‚É“n‚·
        }
    }
}
