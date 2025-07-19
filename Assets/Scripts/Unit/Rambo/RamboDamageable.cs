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

        // �����~
        if (input != null) input.enabled = false;

        // �R���C�_�[�������i�G��U���Ɠ�����Ȃ��Ȃ�j
        foreach (var col in colliders)
            col.enabled = false;

        // �񕜊J�n
        float timer = 0f;
        float startHP = param.CurrentHP;
        float endHP = param.MaxHP;
        bool visible = true;

        while (timer < reviveDuration)
        {
            param.CurrentHP = (int)Mathf.Lerp(startHP, endHP, timer / reviveDuration);

            // �_��
            SetRenderersAlpha(visible ? blinkAlpha : 1f);
            visible = !visible;

            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        // �ŏI���
        param.CurrentHP = (int)endHP;
        SetRenderersAlpha(1f);

        // �R���C�_�[�ėL����
        foreach (var col in colliders)
            col.enabled = true;

        // ����ĊJ
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
