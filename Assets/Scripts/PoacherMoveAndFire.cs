using UnityEngine;

public class PoacherMoveAndFire : MonoBehaviour
{
    UnitParams uParams;

    BaseWeapon weapon;
    float attackRange;
    public float searchRadius = 11f; // お好みで

    Vector3 objectivePos = Vector3.zero;
    LayerMask obstacleLayerMask;

    private void Awake()
    {
        uParams = GetComponent<UnitParams>();
        weapon = GetComponentInChildren<BaseWeapon>();
        attackRange = weapon.data.range;
        obstacleLayerMask = LayerMask.GetMask("Obstacle","Animal","Zero");
    }

    void Start()
    {

    }

    void Update()
    {
        UpdateObjectivePos(); 

        Vector3 approachPos = FindApproachPos(objectivePos);

        float dist = Vector3.Distance(transform.position, approachPos);

        if (dist <= attackRange)
        {
            weapon.Fire(approachPos);
            Debug.Log(dist);
        }
        else
        {
            MoveTowards(approachPos);
        }

    }

    void UpdateObjectivePos()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, searchRadius, LayerMask.GetMask("Animal"));

        float minDist = float.MaxValue;
        Vector3 closest = objectivePos; // 今のまま

        foreach (var hit in hits)
        {
            float dist = Vector3.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = hit.transform.position;
            }
        }

        objectivePos = closest;
    }



    Vector3 FindApproachPos(Vector3 objectivePos)
    {
        // ✅ まず直線で行けるかチェック（障害物がないなら直行）
        RaycastHit2D directHit = Physics2D.Raycast(transform.position, (objectivePos - transform.position).normalized, Vector2.Distance(transform.position, objectivePos), obstacleLayerMask);
        if (directHit.collider == null)
        {
            return objectivePos;
        }

        // ❌ 直行できない → Raycastサークルで回避ルートを探す
        int rayCount = 36;
        float minDist = float.MaxValue;
        Vector3 bestPoint = transform.position;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * 360f / rayCount;
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, searchRadius, obstacleLayerMask);

            Vector2 point;
            if (hit.collider != null)
            {
                // 壁に当たった → 手前に補正
                point = hit.point - dir * 0.1f;
            }
            else
            {
                // 壁なし → 最大距離までOK
                point = (Vector2)transform.position + dir * searchRadius;
            }

            float dist = Vector2.Distance(point, objectivePos);
            if (dist < minDist)
            {
                minDist = dist;
                bestPoint = point;
            }
        }
        return bestPoint;
    }



    void MoveTowards(Vector3 pos)
    {
        Vector3 current = transform.position;
        pos.z = current.z; // Z固定してXY移動

        Vector3 dir = (pos - current).normalized;
        transform.position += dir * uParams.MoveSpeed * Time.deltaTime;
    }



}
