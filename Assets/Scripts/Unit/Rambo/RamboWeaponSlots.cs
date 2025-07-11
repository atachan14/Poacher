using UnityEngine;

public class RamboWeaponSlots : MonoBehaviour
{
    [SerializeField] int unlockedSlotCount = 2; // 最初は1スロだけ使える

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
    void Start()
    {
        RefreshSlotUI(); // 初期UI
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

        if (input.FirePressed)
            UseCurrentWeapon();
    }

    public void UseCurrentWeapon()
    {
        slots[currentSlotIndex]?.Fire(input.AimPos);
    }

    public void SwitchToSlot(int index)
    {
        if (index >= unlockedSlotCount) return; // 未アンロックスロットは無視
        currentSlotIndex = index;

        // 見た目の切り替えとかやるならここ
        OnSwitched(index);

        RefreshSlotUI(); // スロット変更後のUI更新
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

        currentWeapon.Drop(transform.position);

        slots[currentSlotIndex] = null;

        RefreshSlotUI(); // ドロップ後のUI更新
    }

    void TryPickWeapon()
    {
        var worldWeapon = SearchWeaponUnderFoot();
        if (worldWeapon == null) return;

        // スロットに何か入ってたら落とす
        if (slots[currentSlotIndex] != null)
            DropCurrentWeapon();

        // Transformの親をRamboWeaponSlotsに
        worldWeapon.Pick(transform);

        // スロットに登録
        slots[currentSlotIndex] = worldWeapon.baseWeapon;

        RefreshSlotUI(); // 取得後のUI更新
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

        RefreshSlotUI(); // アンロック後のUI更新
    }

    void RefreshSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            var weapon = slots[i];
            bool isUnlocked = (i < unlockedSlotCount);
            UI_SlotsManager.Instance.UpdateSlotUI(i, weapon, isUnlocked);
        }

        UI_SlotsManager.Instance.SetSelectedSlot(currentSlotIndex);
    }
}
