using UnityEngine;

public class RamboWeaponSlots : MonoBehaviour
{
    [SerializeField] int unlockedSlotCount = 1; // 最初は1スロだけ使える

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
        if (index >= unlockedSlotCount) return; // 未アンロックスロットは無視
        currentSlotIndex = index;

        // 見た目の切り替えとかやるならここ
        OnSwitched(index);
    }

    protected virtual void OnSwitched(int index)
    {
        // overrideで演出用に拡張できるようにしておく
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

        // スロットに何か入ってたら落とす
        if (slots[currentSlotIndex] != null)
            DropCurrentWeapon();

        // Transformの親をRamboWeaponSlotsに
        worldWeapon.baseWeapon.transform.SetParent(transform);
        worldWeapon.gameObject.SetActive(false);

        // スロットに登録
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
