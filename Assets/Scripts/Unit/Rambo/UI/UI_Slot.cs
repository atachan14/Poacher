using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{
    [SerializeField] Image outer;
    [SerializeField] Image icon;
    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
        icon.enabled = (sprite != null);
    }

    public void SetSelected(bool isSelected)
    {
        outer.color = isSelected ? Color.white : Color.gray;
    }
}
