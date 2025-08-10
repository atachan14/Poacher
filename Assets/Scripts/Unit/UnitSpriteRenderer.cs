using UnityEngine;

public class UnitSpriteRenderer : MonoBehaviour
{
    [SerializeField] protected Sprite[] sprites = new Sprite[8];
    protected SpriteRenderer sr;

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual float GetFacingAngle()
    {
        // 継承先で方向を決めるための仮処理（0〜360度）
        return 0f;
    }

    void Update()
    {
        float angle = GetFacingAngle();
        int index = Mathf.RoundToInt(angle / 45f) % 8;
        sr.sprite = sprites[index];
    }
}
