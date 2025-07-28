using System.Collections;
using UnityEngine;

public class OnSpotWeapon : BaseWeapon
{
    [SerializeField] GameObject fireEffectPrefab;  // OnSpotAS的なやつ
    [SerializeField] GameObject attackSourcePrefab; // 最終的なBaseAS

    Vector2 targetPos;

    public override void Fire(Vector2 pos)
    {
        RequireCheck(); // Ammoやクールタイムの確認

        targetPos = pos; // マウス位置を保存 

        // 視覚的なエフェクトを出す（その場 or マウス方向にちょっと出す）
        Vector2 dir = (pos - (Vector2)transform.position).normalized;
        Vector2 firePos = (Vector2)transform.position + dir * 0.5f;

        Instantiate(fireEffectPrefab, firePos, Quaternion.identity);

        // 指定秒数後に攻撃判定Prefabを生成
        StartCoroutine(DelayedAttack());
    }

    IEnumerator DelayedAttack()
    {
        yield return new WaitForSeconds(wData.startTime);  // dataはScriptableObjectのWeaponDataとか

        Instantiate(attackSourcePrefab, targetPos, Quaternion.identity,transform);
    }

}
