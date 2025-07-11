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
    //    // 攻撃判定との衝突時にTakeDamage
    //    if (collision.gameObject.TryGetComponent(out AttackSource attack))
    //    {
    //        TakeDamage(attack.Damage);
    //    }
    //    else
    //    {
    //        Debug.Log("attackSouceがない");
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
        // 1. penPercent（例：30%）をfloatにして割合貫通
        float effectiveDef = param.Defense * (100f - penPercent) / 100f;

        // 2. 固定貫通で減らす（マイナス防御もあり得る）
        effectiveDef -= penFlat;

        // 3. LoLっぽい最終ダメージ計算
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
        //あとで
    }

    protected virtual void ShowAnotherEffect()
    {
        // 必要ならoverride
    }

    protected virtual void OnDead()
    {
        // 死亡演出 or 消滅
        Destroy(gameObject);
    }
}
