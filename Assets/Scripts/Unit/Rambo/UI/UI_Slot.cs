using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{
    [SerializeField] Image outer;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI ammoText;

    BaseWeapon weapon;

    public void SetLocked(Sprite lockedIcon)
    {
        weapon = null;
        icon.sprite = lockedIcon;
        icon.enabled = (lockedIcon != null);
        ammoText.text = ""; // •\Ž¦‚µ‚È‚¢
    }

    public void SetWeapon(BaseWeapon weapon, Sprite emptyIcon)
    {
        this.weapon = weapon;

        if (weapon == null)
        {
            icon.sprite = emptyIcon;
            ammoText.text = "";
        }
        else
        {
            icon.sprite = weapon.data.icon;
            ammoText.text = $"{weapon.currentAmmo}/{weapon.data.startAmmo}";
        }

        icon.enabled = (icon.sprite != null);
    }

    public void SetSelected(bool isSelected)
    {
        outer.color = isSelected ? Color.white : Color.gray;
    }

    private void Update()
    {
        if (weapon != null)
        {
            ammoText.text = $"{weapon.currentAmmo}/{weapon.data.startAmmo}";
        }
    }
}
