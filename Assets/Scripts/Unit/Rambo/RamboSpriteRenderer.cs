using UnityEngine;

public class RamboSpriteRenderer : UnitSpriteRenderer
{
    RamboInput input;
    private void Start()
    {
        input = GetComponent<RamboInput>();
    }
    protected override float GetFacingAngle()
    {
        Vector2 dir = input.AimPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0f) angle += 360f;
        return angle;
    }

}
