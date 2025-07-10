using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public WorldWeapon worldWeapon;
    public Sprite icon;

    private void Start()
    {
        worldWeapon = GetComponentInChildren<WorldWeapon>();
    }
    public abstract void Fire(Vector2 dir);
}
