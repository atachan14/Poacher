using UnityEngine;

public class PoacherMoveAndFire : MonoBehaviour
{
    UnitParams uParams;

    BaseWeapon weapon;
    float attackRange;
    public float searchRadius = 11f; // お好みで

    Vector3 objectivePos = Vector3.zero;
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

        var (approachPos, canAttackDir) = FindApproachPos(objectivePos);

        float dist = Vector3.Distance(transform.position, approachPos);

        if (canAttackDir)
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
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, searchRadius, animalLayerMask);

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



    (Vector3 approachPos, bool canAttackDirectly) FindApproachPos(Vector3 objectivePos)
    {
        // 直線でanimalを撃てるかチェック
        RaycastHit2D animalHit = Physics2D.Raycast(transform.position, (objectivePos - transform.position).normalized, attackRange,animalLayerMask);
        if (animalHit.collider != null)
        {
            return (objectivePos, true);
        }

        // ✅ まず直線で行けるかチェック（障害物がないなら直行）
        RaycastHit2D directHit = Physics2D.Raycast(transform.position, (objectivePos - transform.position).normalized, Vector2.Distance(transform.position, objectivePos), obstacleLayerMask);
        if (directHit.collider == null)
        {
            return (objectivePos,false);
        }

        // ❌ 直行できない → Raycastサークルで回避ルートを探す
        int rayCount = 36;
        float minDist = float.MaxValue;
        Vector3 bestPoint = transform.position;
        bool finalCanAttack = false;
        

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * 360f / rayCount;
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, searchRadius, animalLayerMask);

            

            Vector2 point;
            bool canAttack;
            if (hit.collider != null)
            {
                // 壁に当たった → 手前に補正
                point = hit.point - dir * 0.1f;
                canAttack = true;
            }
            else
            {
                // 壁なし → 最大距離までOK
                point = (Vector2)transform.position + dir * searchRadius;
                canAttack = false;
            }

            float dist = Vector2.Distance(point, objectivePos);
            if (dist < minDist)
            {
                minDist = dist;
                bestPoint = point;
                finalCanAttack = canAttack;
            }
        }
        return (bestPoint, finalCanAttack);
    }



    void MoveTowards(Vector3 pos)
    {
        Vector3 current = transform.position;
        pos.z = current.z; // Z固定してXY移動

        Vector3 dir = (pos - current).normalized;
        transform.position += dir * uParams.MoveSpeed * Time.deltaTime;
    }



}
