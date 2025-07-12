using UnityEngine;

public class BuildingDamageable : BaseDamageable
{
    protected override void ShowAnotherEffect()
    {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer == null || param == null) return;

        float hpPercent = (float)param.CurrentHP / param.MaxHP;

        Color baseColor = Color.white;
        Color damagedColor = Color.red;

        // HPŠ„‡‚É‰‚¶‚Ä”’¨Ô‚É•âŠÔ
        renderer.color = Color.Lerp(damagedColor, baseColor, hpPercent);
    }

}
