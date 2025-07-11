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
        // �K�v�ɂȂ�����A�㉺���E�̕����敪�������Ōv�Z��������Ă悢
    }
}