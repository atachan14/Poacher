using UnityEngine;

public class PoacherSpriteRenderer : UnitSpriteRenderer
{
    PoacherAI ai;
    private void Start()
    {
        ai = GetComponent<PoacherAI>();
    }
    protected override float GetFacingAngle()
    {
        Vector2 dir = ai.objectivePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0f) angle += 360f;
        return angle;
    }
}
