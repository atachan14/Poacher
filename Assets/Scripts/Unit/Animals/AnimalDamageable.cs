using UnityEngine;

public class AnimalDamageable : BaseDamageable
{
    protected override void OnDead()
    {

        GameManager.Instance.GameOver(gameObject);
    }
}
