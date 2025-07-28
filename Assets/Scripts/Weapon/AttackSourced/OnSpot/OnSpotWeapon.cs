using System.Collections;
using UnityEngine;

public class OnSpotWeapon : BaseWeapon
{
    [SerializeField] GameObject fireEffectPrefab;  // OnSpotAS�I�Ȃ��
    [SerializeField] GameObject attackSourcePrefab; // �ŏI�I��BaseAS

    Vector2 targetPos;

    public override void Fire(Vector2 pos)
    {
        RequireCheck(); // Ammo��N�[���^�C���̊m�F

        targetPos = pos; // �}�E�X�ʒu��ۑ� 

        // ���o�I�ȃG�t�F�N�g���o���i���̏� or �}�E�X�����ɂ�����Əo���j
        Vector2 dir = (pos - (Vector2)transform.position).normalized;
        Vector2 firePos = (Vector2)transform.position + dir * 0.5f;

        Instantiate(fireEffectPrefab, firePos, Quaternion.identity);

        // �w��b����ɍU������Prefab�𐶐�
        StartCoroutine(DelayedAttack());
    }

    IEnumerator DelayedAttack()
    {
        yield return new WaitForSeconds(wData.startTime);  // data��ScriptableObject��WeaponData�Ƃ�

        Instantiate(attackSourcePrefab, targetPos, Quaternion.identity,transform);
    }

}
