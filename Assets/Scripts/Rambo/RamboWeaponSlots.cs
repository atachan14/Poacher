using UnityEngine;

public class RamboWeaponSlots : MonoBehaviour
{
    [SerializeField] int unlockedSlotCount = 1; // �ŏ���1�X�������g����

    public BaseWeapon[] slots = new BaseWeapon[5];
    int currentSlotIndex = 0;

    int worldWeaponMask;

    RamboInput input;
    RamboState state;

    void Awake()
    {
        input = GetComponentInParent<RamboInput>();
        state = GetComponentInParent<RamboState>();
        worldWeaponMask = 1 << LayerMask.NameToLayer("WorldWeapon");
    }

    void Update()
    {
        for (int i = 0; i < unlockedSlotCount; i++)
        {
            if (input.WeaponSwitchPressed[i])
                SwitchToSlot(i);
        }

        if (input.DropPressed)
            DropCurrentWeapon();

        if (input.PickPressed)
            TryPickWeapon();

        if(input.FirePressed)
            UseCurrentWeapon();
    }

    public void UseCurrentWeapon()
    {
        slots[currentSlotIndex]?.Fire(state.AimDirection);
    }

    public void SwitchToSlot(int index)
    {
        if (index >= unlockedSlotCount) return; // ���A�����b�N�X���b�g�͖���
        currentSlotIndex = index;

        // �����ڂ̐؂�ւ��Ƃ����Ȃ炱��
        OnSwitched(index);
    }

    protected virtual void OnSwitched(int index)
    {
        // override�ŉ��o�p�Ɋg���ł���悤�ɂ��Ă���
        Debug.Log($"Switched to slot {index}");
    }

    void DropCurrentWeapon()
    {
        var currentWeapon = slots[currentSlotIndex];
        if (currentWeapon == null) return;

        currentWeapon.transform.position = transform.position;
        currentWeapon.transform.SetParent(DropItemManager.Instance.transform);
        currentWeapon.worldWeapon.gameObject.SetActive(true);

        slots[currentSlotIndex] = null;
    }

    void TryPickWeapon()
    {
        var worldWeapon = SearchWeaponUnderFoot();
        if (worldWeapon == null) return;

        // �X���b�g�ɉ��������Ă��痎�Ƃ�
        if (slots[currentSlotIndex] != null)
            DropCurrentWeapon();

        // Transform�̐e��RamboWeaponSlots��
        worldWeapon.baseWeapon.transform.SetParent(transform);
        worldWeapon.gameObject.SetActive(false);

        // �X���b�g�ɓo�^
        slots[currentSlotIndex] = worldWeapon.baseWeapon;
    }


    WorldWeapon SearchWeaponUnderFoot()
    {
        float pickupRadius = 1.0f;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, pickupRadius, 1 << LayerMask.NameToLayer("WorldWeapon"));

        WorldWeapon nearest = null;
        float nearestDistSqr = float.MaxValue;

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out WorldWeapon worldWeapon))
            {
                float distSqr = (worldWeapon.transform.position - transform.position).sqrMagnitude;
                if (distSqr < nearestDistSqr)
                {
                    nearest = worldWeapon;
                    nearestDistSqr = distSqr;
                }
            }
        }
        return nearest;
    }

    public void UnlockNextSlot()
    {
        if (unlockedSlotCount < slots.Length)
            unlockedSlotCount++;
    }

}
