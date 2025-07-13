using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    public static AssetsManager Instance { get; private set; }

    [Header("Prefab�n")]
    public GameObject damageText;

    [Header("ScriptableObject�n")]
    public PoacherDB poacherDB;
    public RoundDB roundDB;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }
}
