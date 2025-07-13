using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    public static AssetsManager Instance { get; private set; }

    [Header("Prefabån")]
    public GameObject damageText;

    [Header("ScriptableObjectån")]
    public PoacherDB poacherDB;
    public RoundDB roundDB;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }
}
