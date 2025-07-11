using UnityEngine;

public class PoacherMoveAndFire : MonoBehaviour
{
    UnitParams uParams;

    BaseWeapon weapon;
    float attackRange;
    public float searchRadius = 11f; // お好みで

    Vector2 objectivePos = Vector2.zero;
    LayerMask obstacleLayerMask;
    LayerMask animalLayerMask;

    private void Awake()
    {
        uParams = GetComponent<UnitParams>();
        weapon = GetComponentInChildren<BaseWeapon>();
        attackRange = weapon.data.range;
        obstacleLayerMask = LayerMask.GetMask("Obstacle");
        animalLayerMask = LayerMask.GetMask("Animal");
    }

    void Start()
    {

    }

    void Update()
    {
        UpdateObjectivePos();

        var (canFire, fireTarget) = CheckCanFire();
        if (canFire)
        {
            weapon.Fire(fireTarget);
            return;
        }

        var (canWalkStraight, walkTarget) = CheckCanWalkStraight(objectivePos);
        if (canWalkStraight)
        {
            MoveTowards(walkTarget);
            return;
        }

        var (canPath, approachPos) = FindApproachPos(objectivePos);
        if (canPath)
        {
            MoveTowards(approachPos);
            return;
        }

        TryBreakWall(); // 後で定義
    }


    void UpdateObjectivePos()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, searchRadius, animalLayerMask);

        float minDist = float.MaxValue;
        Vector2 closest = objectivePos; // ← Vector2にする！

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance((Vector2)transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = hit.transform.position;
            }
        }

        objectivePos = closest;
    }

    (bool canFire, Vector3 target) CheckCanFire()
    {
        Vector2 myPos = transform.position; // ✅ これは Vector2 にキャストでもOK
        Vector2 dir = (objectivePos - myPos).normalized;

        RaycastHit2D hit = Physics2D.Raycast(myPos, dir, attackRange, animalLayerMask);
        if (hit.collider != null)
        {
            return (true, hit.point);
        }

        return (false, Vector3.zero);
    }


    (bool canWalk, Vector2 target) CheckCanWalkStraight(Vector2 target)
    {
        Vector2 myPos = transform.position;
        Vector2 dir = (target - myPos).normalized;
        float dist = Vector2.Distance(myPos, target);

        RaycastHit2D hit = Physics2D.Raycast(myPos, dir, dist, obstacleLayerMask);
        return (hit.collider == null, target);
    }



    (bool canApproach, Vector2 approachPos) FindApproachPos(Vector2 target)
    {
        Vector2 myPos = transform.position;
        int rayCount = 36;
        float minDist = float.MaxValue;
        Vector2 bestPoint = myPos;
        bool found = false;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * 360f / rayCount;
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            RaycastHit2D hit = Physics2D.Raycast(myPos, dir, searchRadius, obstacleLayerMask);

            Vector2 point = (hit.collider != null)
                ? hit.point - dir * 0.1f
                : myPos + dir * searchRadius;

            float dist = Vector2.Distance(point, target);
            if (dist < minDist)
            {
                minDist = dist;
                bestPoint = point;
                found = true;
            }
        }

        return (found, bestPoint);
    }


    void TryBreakWall()
    {
        // 最短で壁に向かって撃つ（後で専用壁破壊ロジック入れても良い）
        Vector2 myPos = transform.position;
        Vector2 dir = (objectivePos - myPos).normalized;

        RaycastHit2D hit = Physics2D.Raycast(myPos, dir, attackRange, obstacleLayerMask);
        if (hit.collider != null)
        {
            weapon.Fire(hit.point); // 壁をぶち壊す用のFire（実弾なら当たる）
        }
    }



    void MoveTowards(Vector2 pos)
    {
        Vector2 current = transform.position;
        Vector2 dir = (pos - current).normalized;
        Vector2 newPos = current + dir * uParams.MoveSpeed * Time.deltaTime;

        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }




}
