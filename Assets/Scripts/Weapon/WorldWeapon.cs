using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class WorldWeapon : MonoBehaviour
{
    //public WeaponData data; // ScriptableObject or �v���n�u�Q��

    public BaseWeapon baseWeapon; // ���Ƃ����Ƃ��̎Q�Ɨp�B�E�����Ƃ��ɂ������琶��

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

