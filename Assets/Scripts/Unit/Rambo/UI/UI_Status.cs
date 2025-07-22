using TMPro;
using UnityEngine;

public class UI_Status : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    RamboParams rParam;
    float maxHp;
    float hp;
    void Start()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();

        rParam = GetComponentInParent<RamboParams>();
       

    }

    void Update()
    {
        maxHp = rParam.MaxHP;
        hp = rParam.CurrentHP;
        textMeshProUGUI.text = $"{hp}/{maxHp}";
    }
}
