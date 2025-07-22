using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class R0Ngen : PoacherGenerater
{
    protected override void Wave1()
    {
        base.Wave1();
        StartCoroutine(SpawnPoachersRoutine(0.5f));
    }

    IEnumerator SpawnPoachersRoutine(float offs)
    {
        int spawnCount = 20;
        float spawnDelay = 0.2f;
        float spawnY = 15f;
        float offset = offs;
        float minX = -offset;
        float maxX = offset;

        for (int i = 0; i < spawnCount; i++)
        {
            float x = Random.Range(minX, maxX);
            Vector2 spawnPos = new Vector2(x, spawnY);

            Instantiate(AssetsManager.Instance.poacherDB.knifePoacher, spawnPos, Quaternion.identity, transform);

            yield return new WaitForSeconds(spawnDelay);


        }
        // ����������MachinegunPoacher�̏�����
        yield return new WaitForSeconds(5f); // 5�b�҂�

        float offsetX = 1f;
        Vector2 centerPos = new Vector2(0f, spawnY);
        Vector2 leftPos = centerPos + Vector2.left * offsetX;
        Vector2 rightPos = centerPos + Vector2.right * offsetX;

        Instantiate(AssetsManager.Instance.poacherDB.machinegunPoacher, leftPos, Quaternion.identity, transform);
        Instantiate(AssetsManager.Instance.poacherDB.machinegunPoacher, rightPos, Quaternion.identity, transform);

        // ������ �����őS�������ʂ̂�҂�
        yield return StartCoroutine(WaitUntilAllPoachersDead());

        // �S���������玟��Wave
        var roundManager = GetComponentInParent<BaseRoundManager>();
        roundManager?.NextWave();
    }
    IEnumerator WaitUntilAllPoachersDead()
    {
        // �q�����Ȃ��Ȃ�܂ő҂�
        while (transform.childCount > 0)
        {
            yield return null; // 1�t���[���҂�
        }
    }

    protected override void Wave2()
    {
        base.Wave1();
        StartCoroutine(SpawnPoachersRoutine(4f));
    }


}



