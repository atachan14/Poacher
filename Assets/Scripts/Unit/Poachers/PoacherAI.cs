using UnityEngine;

public class PoacherAI : MonoBehaviour
{
    UnitParams uParams;
    BaseWeapon weapon;

    public float searchRadius = 5;
    float attackRange;
    Vector2 objectivePos = Vector2.zero;

    LayerMask unitLayerMask;
    Vector2 debugBestPoint;
    float wallFireRange = 3f;

    void Awake()
    {
        uParams = GetComponent<UnitParams>();
        weapon = GetComponentInChildren<BaseWeapon>();
        attackRange = weapon.data.range;
        wallFireRange = attackRange;
        unitLayerMask = LayerMask.GetMask("Unit");
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(debugBestPoint, 0.2f);
    }
    void Update()
    {

        UpdateObjectivePos();

        Vector2 myPos = transform.position;
        Vector2 dir = (objectivePos - myPos).normalized;
        float dist = Vector2.Distance(myPos, objectivePos);

        RaycastHit2D hit = RaycastIgnoreSelf(myPos, dir, dist, unitLayerMask);
        Debug.DrawRay(myPos, dir * dist, Color.red);

        if (hit.collider != null)
        {
            var hitParams = hit.collider.GetComponent<UnitParams>();
            if (hitParams != null && hitParams.Type == UnitType.Animal)
            {
                // Animalにヒット = 射線通ってる！撃つ or 歩く判定
                if (dist <= attackRange)
                {
                    Fire(objectivePos);
                }
                else
                {
                    Debug.Log("Animalまで射線通ってる歩き。");
                    MoveTowards(objectivePos);
                }
                return;
            }
        }

        // ここに来たら Animal以外に当たってる = 障害物がある
        // 他のApproachPos探す処理とかTryBreakWallに続く流れへ

        // ④-1: 遮られてるけど探索圏内
        var (found, approachPos, hitRambo) = FindApproachPosWithFallback(objectivePos);
        if (found)
        {
            if (hitRambo != null)
            {
                Vector2 ramboPos = hitRambo.transform.position;
                float ramboDist = Vector2.Distance(myPos, ramboPos);
                if (ramboDist <= attackRange)
                {
                    Fire(ramboPos);
                    return;
                }
            }
            MoveTowards(approachPos);
            return;
        }

        // ④-2: Poacherなら角度ずらす,Poacher以外なら撃つ。

        Debug.Log("4 - 2");
        var blocker = hit.collider.GetComponent<UnitParams>();
        if (blocker != null)
        {
            if (blocker.Type == UnitType.Poacher)
            {
                // 射線がPoacherなら撃たずに待機
                Debug.Log("4 - 2 - Poacherブロック → 待機");
            }
            else 
            {
                Debug.Log("4 - 2 - 2");
                Fire(hit.point);
            }
        }
    }

    void Fire(Vector2 pos)
    {
        if (weapon.currentAmmo != 0)
        {
            weapon.Fire(pos);
            Debug.Log("Fire");
        }
        else
        {
            Debug.Log("Drop");
            weapon.Drop(transform.position);
            weapon = GetComponentInChildren<BaseWeapon>();
            attackRange = weapon.data.range;
            wallFireRange = attackRange;
        }
    }

    void UpdateObjectivePos()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, searchRadius, unitLayerMask);
        float minDist = float.MaxValue;
        Vector2 closest = Vector2.zero;

        foreach (var col in hits)
        {
            var unit = col.GetComponent<UnitParams>();
            if (unit == null || unit.Type != UnitType.Animal) continue;

            float dist = Vector2.Distance(transform.position, col.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = col.transform.position;
            }
        }

        objectivePos = closest;
    }

    (bool found, Vector2 bestPos, UnitParams hitRambo) FindApproachPosWithFallback(Vector2 target)
    {
        Vector2 myPos = transform.position;
        int rayCount = 36;
        float currentDist = Vector2.Distance(myPos, target);
        float minDist = currentDist;
        Vector2 bestPoint = myPos;
        UnitParams hitRambo = null;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * 360f / rayCount;
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            RaycastHit2D hit = RaycastIgnoreSelf(myPos, dir, searchRadius, unitLayerMask);

            Vector2 point;
            if (hit.collider != null)
            {
                var unit = hit.collider.GetComponent<UnitParams>();
                if (unit != null && unit.Type == UnitType.Rambo)
                {
                    hitRambo = unit;
                }

                point = hit.point - dir * 0.1f;
            }
            else
            {
                point = myPos + dir * searchRadius;
            }

            float distToTarget = Vector2.Distance(point, target);
            if (distToTarget < minDist)
            {
                minDist = distToTarget;
                bestPoint = point;
            }

            Debug.DrawLine(myPos, point, Color.yellow, 1f);
        }

        float distToBest = Vector2.Distance(myPos, bestPoint);
        bool found = distToBest > wallFireRange ;

        debugBestPoint = bestPoint;

        return (found, bestPoint, hitRambo);
    }





    bool TryFindNonPoacherGap(Vector2 target, out Vector2 gapDir)
    {
        Vector2 myPos = transform.position;
        int searchAngleRange = 90;
        int steps = 10;

        for (int i = -steps; i <= steps; i++)
        {
            float angleOffset = i * (searchAngleRange / (float)steps);
            Vector2 baseDir = (target - myPos).normalized;
            Vector2 dir = Quaternion.Euler(0, 0, angleOffset) * baseDir;

            RaycastHit2D hit = RaycastIgnoreSelf(myPos, dir, attackRange, unitLayerMask);
            if (hit.collider == null) continue;

            var unit = hit.collider.GetComponent<UnitParams>();
            if (unit != null && unit.Type != UnitType.Poacher)
            {
                gapDir = dir;
                return true;
            }
        }

        gapDir = Vector2.zero;
        return false;
    }

    void MoveTowards(Vector2 pos)
    {
        Vector2 current = transform.position;
        Vector2 dir = (pos - current).normalized;
        Vector2 newPos = current + dir * uParams.MoveSpeed * Time.deltaTime;
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }

    RaycastHit2D RaycastIgnoreSelf(Vector2 origin, Vector2 direction, float distance, LayerMask mask)
    {
        Vector2 offsetOrigin = origin + direction.normalized * 0.3f;
        return Physics2D.Raycast(offsetOrigin, direction, distance - 0.3f, mask);
    }
}
