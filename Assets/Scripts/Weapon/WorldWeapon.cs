using UnityEngine;

public class WorldWeapon : MonoBehaviour
{
    //public WeaponData data; // ScriptableObject or �v���n�u�Q��

    public BaseWeapon baseWeapon; // ���Ƃ����Ƃ��̎Q�Ɨp�B�E�����Ƃ��ɂ������琶��

    private void Awake()
    {
        baseWeapon = GetComponentInParent<BaseWeapon>();
    }

}

