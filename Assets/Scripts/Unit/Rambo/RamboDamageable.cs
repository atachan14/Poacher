using UnityEngine;

using System.Collections;

public class RamboDamageable : BaseDamageable
{
    [SerializeField] float reviveDuration = 5f;
    [SerializeField] float blinkInterval = 0.2f;
    [SerializeField] float blinkAlpha = 0.3f;

    bool isInvincible = false;
    SpriteRenderer[] renderers;
    Collider2D[] colliders;
    RamboInput input;

    protected override void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        colliders = GetComponentsInChildren<Collider2D>();
        input = GetComponent<RamboInput>();
        param = GetComponent<RamboParams>();
    }

    protected override void OnDead()
    {
        StartCoroutine(ReviveCoroutine());
    }

    IEnumerator ReviveCoroutine()
    {
        isInvincible = true;

        // 操作停止
        if (input != null) input.enabled = false;

        // コライダー無効化（敵や攻撃と当たらなくなる）
        foreach (var col in colliders)
            col.enabled = false;

        // 回復開始
        float timer = 0f;
        float startHP = param.CurrentHP;
        float endHP = param.MaxHP;
        bool visible = true;

        while (timer < reviveDuration)
        {
            param.CurrentHP = (int)Mathf.Lerp(startHP, endHP, timer / reviveDuration);

            // 点滅
            SetRenderersAlpha(visible ? blinkAlpha : 1f);
            visible = !visible;

            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        // 最終状態
        param.CurrentHP = (int)endHP;
        SetRenderersAlpha(1f);

        // コライダー再有効化
        foreach (var col in colliders)
            col.enabled = true;

        // 操作再開
        if (input != null) input.enabled = true;

        isInvincible = false;
    }

    void SetRenderersAlpha(float alpha)
    {
        foreach (var r in renderers)
        {
            var color = r.color;
            color.a = alpha;
            r.color = color;
        }
    }
}
