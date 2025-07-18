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

        // HP割合に応じて白→赤に補間
        renderer.color = Color.Lerp(damagedColor, baseColor, hpPercent);
    }

}
