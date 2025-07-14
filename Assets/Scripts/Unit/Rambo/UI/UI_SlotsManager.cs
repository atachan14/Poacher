using UnityEngine;

public class UI_SlotsManager : MonoBehaviour
{
    public static UI_SlotsManager Instance { get; private set; }

    [SerializeField] Sprite emptyIcon;
    [SerializeField] Sprite lockedIcon;

    UI_Slot[] slots;

    private void Awake()
    {
        Instance = this;
        slots = GetComponentsInChildren<UI_Slot>();
    }

    public void UpdateSlotUI(int index, BaseWeapon weapon, bool isUnlocked)
    {
        if (index < 0 || index >= slots.Length) return;

        if (!isUnlocked)
        {
            slots[index].SetLocked(lockedIcon);
        }
        else
        {
            slots[index].SetWeapon(weapon, emptyIcon);
        }
    }

    public void SetSelectedSlot(int index)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSelected(i == index);
        }
    }
}
