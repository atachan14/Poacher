using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class WorldWeapon : MonoBehaviour
{
    //public WeaponData data; // ScriptableObject or プレハブ参照

    public BaseWeapon baseWeapon; // 落としたときの参照用。拾ったときにここから生成

    private void Awake()
    {
        baseWeapon = GetComponentInParent<BaseWeapon>();
        ApllyIcon();
    }

    void ApllyIcon()
    {
        GetComponent<SpriteRenderer>().sprite = baseWeapon.data.icon;
    }

    public void Pick(Transform t)
    {
        baseWeapon.transform.position = t.position;
        baseWeapon.transform.SetParent(t);
        gameObject.SetActive(false);
    }
}

