using UnityEngine;

public class RamboState : MonoBehaviour
{
    public Vector2 AimDirection { get; private set; }

    RamboInput input;
    Transform self;

    void Awake()
    {
        input = GetComponent<RamboInput>();
        self = transform;
    }

    void Update()
    {
        AimDirection = (input.AimPos - (Vector2)self.position).normalized;
        // 必要になったら、上下左右の方向区分もここで計算しちゃってよい
    }
}