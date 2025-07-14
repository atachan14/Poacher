
using UnityEngine;

public class RamboMove : MonoBehaviour
{
    RamboParams rParams;
    Rigidbody2D rb;
    Vector2 input;

    private void Start()
    {
        rParams = GetComponent<RamboParams>();
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
        rParams.currentSpeed = speed;
    }

    void ApplyDampingControl()
    {
        if (input.magnitude < 0.1f)
        {
            rb.linearDamping = rParams.idleDamping;
        }
        else if (rb.linearVelocity.magnitude > 0.1f)
        {
            float dot = Vector2.Dot(rb.linearVelocity.normalized, input);
            rb.linearDamping = (dot < 0f) ? rParams.reverseDamping : rParams.normalDamping;
        }
        else
        {
            rb.linearDamping = rParams.normalDamping;
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
                vel.x = velX * (1 + rParams.multiplierAccel) + rParams.additiveAccel * dirX;
            else
                vel.x *= (1 - rParams.multiplierAccel);
        }

        if (Mathf.Abs(input.y) > 0.1f)
        {
            float dirY = Mathf.Sign(input.y);
            float velY = vel.y;
            float dotY = Mathf.Sign(velY) == dirY ? 1f : -1f;

            if (dotY > 0f || Mathf.Abs(velY) < 0.1f)
                vel.y = velY * (1 + rParams.multiplierAccel) + rParams.additiveAccel * dirY;
            else
                vel.y *= (1 - rParams.multiplierAccel);
        }

        rb.linearVelocity = Vector2.ClampMagnitude(vel, rParams.maxSpeed);
    }

    void ApplyMaxSpeedControl()
    {
        if (rb.linearVelocity.magnitude > rParams.maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * rParams.maxSpeed;
        }
    }
}
