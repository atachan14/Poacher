using UnityEngine;

public class Punch : OnSpotWeapon
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
        
    }
}
