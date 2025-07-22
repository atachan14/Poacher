using System.Collections;
using UnityEngine;

public class R0Egen : PoacherGenerater
{
    IEnumerator SpawnPoachersRoutine(float offs)
    {
        int spawnCount = 20;
        float spawnDelay = 0.2f;
        float offset = offs;
        float spawnX = 15f;
        float minY = -offset;
        float maxY = offset;

        for (int i = 0; i < spawnCount; i++)
        {
            float y = Random.Range(minY, maxY);
            Vector2 spawnPos = new Vector2(spawnX, y);

            Instantiate(AssetsManager.Instance.poacherDB.knifePoacher, spawnPos, Quaternion.identity, transform);

            yield return new WaitForSeconds(spawnDelay);


        }
        // ★ここからMachinegunPoacherの処理★
        yield return new WaitForSeconds(5f); // 5秒待つ

        float offsetY = 1f;
        Vector2 centerPos = new Vector2(spawnX, 0f);
        Vector2 upPos = centerPos + Vector2.up * offsetY;
        Vector2 downPos = centerPos + Vector2.down * offsetY;

        Instantiate(AssetsManager.Instance.poacherDB.machinegunPoacher, upPos, Quaternion.identity, transform);
        Instantiate(AssetsManager.Instance.poacherDB.machinegunPoacher, downPos, Quaternion.identity, transform);

        // ↓↓↓ ここで全員が死ぬのを待つ
        yield return StartCoroutine(WaitUntilAllPoachersDead());

        // 全員消えたら次のWave
        var roundManager = GetComponentInParent<BaseRoundManager>();
        roundManager?.WaveStart(2);
    }
    IEnumerator WaitUntilAllPoachersDead()
    {
        // 子がいなくなるまで待つ
        while (transform.childCount > 0)
        {
            yield return null; // 1フレーム待つ
        }
    }

    protected override void Wave2()
    {
        base.Wave1();
        StartCoroutine(SpawnPoachersRoutine(4f));
    }


}
