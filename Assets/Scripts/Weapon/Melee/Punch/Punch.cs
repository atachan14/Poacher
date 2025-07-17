using UnityEngine;

public class Punch : MeleeAttack
{
    protected override void Start()
    {
        worldWeapon = null;
        currentAmmo = -999;
    }

    public override bool TryConsumeAmmo()
    {
        return true;
    }

    public override void Drop(Vector2 pos)
    {
        Debug.Log("RamboPunch‚ÍDrop‚Å‚«‚Ü‚¹‚ñB");
    }
}
