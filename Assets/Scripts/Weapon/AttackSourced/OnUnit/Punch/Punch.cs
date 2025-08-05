using UnityEngine;

public class Punch : OnUnitWeapon
{
    protected override void Start()
    {
        worldWeapon = null;
        uParams = GetComponentInParent<UnitParams>();
        currentAmmo = -999;
    }

    public override bool TryConsumeAmmo()
    {
        return true;
    }

    public override void Drop(Vector2 pos)
    {
        
    }
}
