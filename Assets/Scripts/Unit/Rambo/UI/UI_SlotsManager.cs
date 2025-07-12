using Unity.VisualScripting;
using UnityEngine;

public class UI_SlotsManager : MonoBehaviour
{
    static public UI_SlotsManager Instance { get; private set; }
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
            slots[index].SetIcon(lockedIcon);
        }
        else if (weapon == null)
        {
            slots[index].SetIcon(emptyIcon);
        }
        else
        {
            slots[index].SetIcon(GetWeaponIcon(weapon));
        }
    }

    Sprite GetWeaponIcon(BaseWeapon weapon)
    {
        // あんたの構造に応じてアイコンを取得
        return weapon.data.icon;
    }

    public void SetSelectedSlot(int index)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSelected(i == index);
        }
    }
}