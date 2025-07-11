using UnityEngine;

public class BaseDamageable : MonoBehaviour
{
    protected UnitParams param;

    private void Awake()
    {
        param = GetComponent<UnitParams>();
    }
 

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // �U������Ƃ̏Փˎ���TakeDamage
    //    if (collision.gameObject.TryGetComponent(out AttackSource attack))
    //    {
    //        TakeDamage(attack.Damage);
    //    }
    //    else
    //    {
    //        Debug.Log("attackSouce���Ȃ�");
    //    }
    //}

    public virtual void TakeDamage(int rawDMG,int penFlat,int penPer)
    {
        int finalDMG = CalcDamage(rawDMG,penFlat,penPer);
        param.CurrentHP -= finalDMG;

        ShowDamageEffect();
        ShowAnotherEffect();

        if (param.CurrentHP <= 0)
        {
            OnDead();
        }
    }

   

    int CalcDamage(int rawDamage,  int penFlat, int penPercent)
    {
        // 1. penPercent�i��F30%�j��float�ɂ��Ċ����ђ�
        float effectiveDef = param.Defense * (100f - penPercent) / 100f;

        // 2. �Œ�ђʂŌ��炷�i�}�C�i�X�h������蓾��j
        effectiveDef -= penFlat;

        // 3. LoL���ۂ��ŏI�_���[�W�v�Z
        float finalDamage;
        if (effectiveDef >= 0)
        {
            finalDamage = rawDamage * 100f / (100f + effectiveDef);
        }
        else
        {
            finalDamage = rawDamage * (2f - 100f / (100f - effectiveDef));
        }

        return Mathf.RoundToInt(finalDamage);
    }

    protected void ShowDamageEffect()
    {
        //���Ƃ�
    }

    protected virtual void ShowAnotherEffect()
    {
        // �K�v�Ȃ�override
    }

    protected virtual void OnDead()
    {
        // ���S���o or ����
        Destroy(gameObject);
    }
}
