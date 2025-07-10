using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public WorldWeapon worldWeapon;

    private void Start()
    {
        worldWeapon = GetComponentInChildren<WorldWeapon>();
    }
    public abstract void Fire(Vector2 dir);
}
