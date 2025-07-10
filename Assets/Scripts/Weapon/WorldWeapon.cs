using UnityEngine;

public class WorldWeapon : MonoBehaviour
{
    //public WeaponData data; // ScriptableObject or プレハブ参照

    public BaseWeapon baseWeapon; // 落としたときの参照用。拾ったときにここから生成

    private void Awake()
    {
        baseWeapon = GetComponentInParent<BaseWeapon>();
    }

}

