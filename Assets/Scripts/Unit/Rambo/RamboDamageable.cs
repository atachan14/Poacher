using UnityEngine;

using System.Collections;

public class RamboDamageable : BaseDamageable
{
    [SerializeField] float reviveDuration = 5f;
    [SerializeField] float blinkInterval = 0.2f;
    [SerializeField] float blinkAlpha = 0.3f;

    SpriteRenderer[] renderers;
    Collider2D[] colliders;
    RamboMove2 ramboMove;
    RamboWeaponSlots ramboSlots;
    Rigidbody2D rb;

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<SpriteRenderer>();
        colliders = GetComponentsInChildren<Collider2D>();
        ramboMove = GetComponent<RamboMove2>();
        ramboSlots = GetComponentInChildren<RamboWeaponSlots>();
        param = GetComponent<RamboParams>();
    }

    protected override void OnDead()
    {
        StartCoroutine(ReviveCoroutine());
    }

    IEnumerator ReviveCoroutine()
    {
        rb.linearVelocity = Vector2.zero;
        ramboMove.enabled = false;
        ramboSlots.enabled = false;

        foreach (var col in colliders)
            col.enabled = false;

        float timer = 0f;
        float startHP = param.CurrentHP;
        float endHP = param.MaxHP;
        float blinkTimer = 0f;
        bool visible = true;

        while (timer < reviveDuration)
        {
            // 毎フレームHPを更新（滑らか）
            param.CurrentHP = (int)Mathf.Lerp(startHP, endHP, timer / reviveDuration);

            // 点滅処理はblinkIntervalごとに
            blinkTimer += Time.deltaTime;
            if (blinkTimer >= blinkInterval)
            {
                SetRenderersAlpha(visible ? blinkAlpha : 1f);
                visible = !visible;
                blinkTimer = 0f;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        param.CurrentHP = (int)endHP;
        SetRenderersAlpha(1f);

        foreach (var col in colliders)
            col.enabled = true;

        ramboMove.enabled = true;
        ramboSlots.enabled = true;
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
