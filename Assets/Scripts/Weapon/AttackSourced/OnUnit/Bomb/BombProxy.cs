using UnityEngine;
using System.Collections;

public class BombProxy : BaseAS
{
    [SerializeField] GameObject bombAS;

    BaseWeapon bw;
    Vector2 targetPos;

    // �p�����[�^�����p
    [SerializeField] float travelTime = 1.0f; // ��Ԃ̂ɂ����鎞��
    [SerializeField] float arcHeight = 2.0f;  // �A�[�`�̍���

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

            // ���`��Ԃ�X,Y�����߂�
            Vector2 flatPos = Vector2.Lerp(startPos, targetPos, t);

            // �������Fsin(�� * t) �������ɂ�����
            float height = Mathf.Sin(Mathf.PI * t) * arcHeight;

            transform.position = flatPos + Vector2.up * height;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        // ���e���ɍU������Prefab�𐶐�
        Quaternion rotation = Quaternion.identity;
        Instantiate(bombAS, targetPos, rotation).GetComponent<BaseAS>()?.Setup(bw,targetPos);

        // �����͏�����
        Destroy(gameObject);
    }
}
