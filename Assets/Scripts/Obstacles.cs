using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public int hp = 100;

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Debug.Log("Cage Broken!");

            // TODO: �������I�o�A�G��������_���n�߂鏈���Ƃ�
            Destroy(gameObject);
        }
    }
}