using UnityEngine;

public class GunEffects : MonoBehaviour
{

    [SerializeField] float lifetime = 0.1f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
