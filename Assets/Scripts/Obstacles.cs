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

            // TODO: “®•¨‚ª˜IoA“G‚ª“®•¨‚ð‘_‚¢Žn‚ß‚éˆ—‚Æ‚©
            Destroy(gameObject);
        }
    }
}