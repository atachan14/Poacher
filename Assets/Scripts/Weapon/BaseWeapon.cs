using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public WeaponData data;
    WorldWeapon worldWeapon;

    private void Start()
    {
        worldWeapon = GetComponentInChildren<WorldWeapon>();
    }
    public abstract void Fire(Vector2 dir);

    public void Drop(Vector2 pos)
    {
        transform.position = pos;
        transform.parent = DropItemManager.Instance.transform;
        worldWeapon.gameObject.SetActive(true);
    }

   
}
