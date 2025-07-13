using UnityEngine;
using TMPro;
using System.Collections;

public class DamageText : MonoBehaviour
{
    [SerializeField] float moveY = 1f;
    [SerializeField] float horizontalRange = 0.5f;
    TextMeshPro text;
    Vector3 velocity;

    void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    Color baseColor;

    public void Setup(int damage, UnitType uType)
    {
        text.text = damage.ToString();

        switch (uType)
        {
            case UnitType.Rambo:
                baseColor = Color.red;
                break;
            case UnitType.Poacher:
                baseColor = Color.white;
                break;
            case UnitType.Turret:
                baseColor = Color.blue;
                break;
            default:
                baseColor = Color.gray;
                break;
        }

        text.color = baseColor;

        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        float elapsed = 0f;
        Vector3 start = transform.position;

        float jumpHeight = 1.0f;
        float duration = 1.0f;
        float fadeDuration = 0.5f;

        float angle = Random.Range(-15f, 15f) * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)).normalized;

        float gravity = -2f * jumpHeight / Mathf.Pow(duration / 2f, 2);
        float initialVelocityY = -gravity * (duration / 2f);
        float initialVelocityX = direction.x * 1.5f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            float yOffset = initialVelocityY * elapsed + 0.5f * gravity * Mathf.Pow(elapsed, 2);
            float xOffset = initialVelocityX * elapsed;

            transform.position = start + new Vector3(xOffset, yOffset, 0f);

            // Fade out
            if (t > 1f - (fadeDuration / duration))
            {
                float fade = 1f - (t - (1f - (fadeDuration / duration))) / (fadeDuration / duration);
                text.color = new Color(baseColor.r, baseColor.g, baseColor.b, fade);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }


}
