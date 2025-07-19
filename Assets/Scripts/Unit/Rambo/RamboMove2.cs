using UnityEngine;

public class RamboMove2 : MonoBehaviour
{
    Rigidbody2D rb;
    RamboInput input;

    // 実験用パラメータ
    public float MoveSpeed = 3f;

    public float Acceleration = 1f;
    public float Deceleration = 15f;
    public float MaxDashSpeed = 8f;

    float lastSideInput = 0f;

    public float TurnAcceleration = 1.5f;
    public float TurnDeceleration = 2f;
    public float MaxTurnPower = 3f;
    public float TurnSharpness = 1.2f;


    // 状態
    bool isDashing = false;
    float dashSpeed = 0f;
    Vector2 dashDir = Vector2.zero;
    Vector2 dashOriginDir = Vector2.zero;
    float turnPower = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<RamboInput>();
    }

    void Update()
    {
        // 入力更新だけここで
        moveInput = input.MoveInput;
        dashHeld = Input.GetKey(KeyCode.Space);
    }

    Vector2 moveInput;
    bool dashHeld;

    void FixedUpdate()
    {
        if (dashHeld)
        {
            HandleDash();
        }
        else
        {
            HandleNormalMove();
        }
    }

    void HandleNormalMove()
    {
        // 通常移動：リニア
        rb.linearVelocity = moveInput * MoveSpeed;

        // ダッシュ解除
        isDashing = false;
        dashSpeed = 0f;
        turnPower = 0f;
    }

    void HandleDash()
    {
        if (!isDashing)
        {
            if (moveInput != Vector2.zero)
            {
                isDashing = true;
                dashOriginDir = moveInput.normalized;
                dashDir = dashOriginDir;
                dashSpeed = 0f;
                turnPower = 0f;
                lastSideInput = 0f;
            }
            else
            {
                return;
            }
        }

        // --- 加速 ---
        dashSpeed += Acceleration * Time.fixedDeltaTime;
        dashSpeed = Mathf.Min(dashSpeed, MaxDashSpeed);

        // --- 横入力による旋回 ---
        Vector2 perp = new Vector2(-dashOriginDir.y, dashOriginDir.x);
        float sideInput = Vector2.Dot(moveInput, perp); // -1左, +1右

        if (Mathf.Abs(sideInput) > 0.01f)
        {
            // 入力中：turnPowerが蓄積し続ける
            lastSideInput = Mathf.Sign(sideInput);
            turnPower += TurnAcceleration * Time.fixedDeltaTime;
            turnPower = Mathf.Min(turnPower, MaxTurnPower);
        }
        else
        {
            // 入力なし：turnPowerが徐々に減衰
            turnPower -= TurnDeceleration * Time.fixedDeltaTime;
            turnPower = Mathf.Max(turnPower, 0f);
        }

        if (turnPower > 0f)
        {
            float turnAmount = lastSideInput * turnPower * TurnSharpness;
            dashDir = Quaternion.Euler(0f, 0f, turnAmount) * dashDir;
            dashDir.Normalize();
        }

        // --- 移動適用 ---
        rb.linearVelocity = dashDir * dashSpeed;

        // --- ブレーキ ---
        if (!dashHeld)
        {
            dashSpeed -= Deceleration * Time.fixedDeltaTime;
            dashSpeed = Mathf.Max(dashSpeed, 0f);
            rb.linearVelocity = dashDir * dashSpeed;

            if (dashSpeed <= 0f)
            {
                isDashing = false;
                rb.linearVelocity = Vector2.zero;
            }
        }
    }


}

