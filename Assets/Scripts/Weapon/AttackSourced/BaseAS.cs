using UnityEngine;
using UnityEngine.Analytics;

public class BaseAS : MonoBehaviour
{
    protected float lifetime;
    public int Damage { get; set; }
    public int PenFlat { get; set; }
    public int PenPer { get; set; }

    public UnitParams uParams { get; set; }
    protected Vector2 TargetPos { get; private set; }

    public virtual void Setup(BaseWeapon bw, Vector2 pos)
    {

        lifetime = bw.wData.lifetime;
        Damage = bw.wData.baseDamage * bw.uParams.Attack / 100;
        PenFlat = bw.wData.penFlat;
        PenPer = bw.wData.penPer;

        uParams = bw.GetComponentInParent<UnitParams>();

        TargetPos = pos;

        Destroy(gameObject, lifetime);
    }

   
}
