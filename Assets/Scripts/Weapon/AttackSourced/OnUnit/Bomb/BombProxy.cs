using UnityEngine;
using System.Collections;

public class BombProxy : BaseAS
{
    [SerializeField] GameObject bombAS;

    BaseWeapon bw;
    Vector2 targetPos;

    // パラメータ調整用
    [SerializeField] float travelTime = 1.0f; // 飛ぶのにかかる時間
    [SerializeField] float arcHeight = 2.0f;  // アーチの高さ

    public override void Setup(BaseWeapon bw, Vector2 pos)
    {
        this.bw = bw;
        this.targetPos = pos;
        transform.parent = null;

        StartCoroutine(FlyToTarget());
    }

    IEnumerator FlyToTarget()
    {
        
        Vector2 startPos = transform.position;
        float timer = 0f;

        while (timer < travelTime)
        {
            float t = timer / travelTime;

            // 線形補間でX,Yを求める
            Vector2 flatPos = Vector2.Lerp(startPos, targetPos, t);

            // 放物線：sin(π * t) を高さにかける
            float height = Mathf.Sin(Mathf.PI * t) * arcHeight;

            transform.position = flatPos + Vector2.up * height;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        // 着弾時に攻撃判定Prefabを生成
        Quaternion rotation = Quaternion.identity;
        Instantiate(bombAS, targetPos, rotation).GetComponent<BaseAS>()?.Setup(bw,targetPos);

        // 自分は消える
        Destroy(gameObject);
    }
}
