using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RamboMove : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float additiveAccel = 0.2f; // 絶対に加速する下限値
    [SerializeField] float multiplierAccel = 0.05f; // 乗算でブースト感

    [Header("Damping Settings")]
    [SerializeField] float idleDamping = 2f;
    [SerializeField] float normalDamping = 0f;
    [SerializeField] float reverseDamping = 3f;

    [Header("Debug")]
    [SerializeField] float currentSpeed;  // Inspectorに速度表示（ReadOnly属性は後述）

    Rigidbody2D rb;
    Vector2 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        ShowCurrentSpeed();
        ApplyDampingControl();
        ApplySpeedControl();
        ApplyMaxSpeedControl();
    }

    void ShowCurrentSpeed()
    {
        float speed = rb.linearVelocity.magnitude;
        currentSpeed = speed;
    }

    void ApplyDampingControl()
    {
        if (input.magnitude < 0.1f)
        {
            rb.linearDamping = idleDamping;
        }
        else if (rb.linearVelocity.magnitude > 0.1f)
        {
            float dot = Vector2.Dot(rb.linearVelocity.normalized, input);
            rb.linearDamping = (dot < 0f) ? reverseDamping : normalDamping;
        }
        else
        {
            rb.linearDamping = normalDamping;
        }
    }

    void ApplySpeedControl()
    {
        Vector2 vel = rb.linearVelocity;
        Vector2 accel = Vector2.zero;

        if (Mathf.Abs(input.x) > 0.1f)
        {
            float dirX = Mathf.Sign(input.x);
            float velX = vel.x;
            float dotX = Mathf.Sign(velX) == dirX ? 1f : -1f;

            if (dotX > 0f || Mathf.Abs(velX) < 0.1f)
                vel.x = velX * (1 + multiplierAccel) + additiveAccel * dirX;
            else
                vel.x *= (1 - multiplierAccel);
        }

        if (Mathf.Abs(input.y) > 0.1f)
        {
            float dirY = Mathf.Sign(input.y);
            float velY = vel.y;
            float dotY = Mathf.Sign(velY) == dirY ? 1f : -1f;

            if (dotY > 0f || Mathf.Abs(velY) < 0.1f)
                vel.y = velY * (1 + multiplierAccel) + additiveAccel * dirY;
            else
                vel.y *= (1 - multiplierAccel);
        }

        rb.linearVelocity = Vector2.ClampMagnitude(vel, maxSpeed);
    }

    void ApplyMaxSpeedControl()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
}
