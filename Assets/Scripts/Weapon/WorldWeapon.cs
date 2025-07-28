using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class WorldWeapon : MonoBehaviour
{
    //public WeaponData data; // ScriptableObject or プレハブ参照

    public BaseWeapon baseWeapon; // 落としたときの参照用。拾ったときにここから生成
    public float pickupCooldown = 0.2f; // 0.2秒間は拾えない
    float dropTime;

    public void MarkAsDropped()
    {
        dropTime = Time.time;
    }

    public bool CanBePicked()
    {
        return Time.time > dropTime + pickupCooldown;
    }

    private void Awake()
    {
        baseWeapon = GetComponentInParent<BaseWeapon>();
        ApllyIcon();
    }

    void ApllyIcon()
    {
        GetComponent<SpriteRenderer>().sprite = baseWeapon.wData.icon;
    }

    public void Pick(Transform t)
    {
        baseWeapon.Pick(t);
       
        gameObject.SetActive(false);
    }
}

