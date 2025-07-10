using UnityEngine;

public class RamboInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 AimPos { get; private set; }
    public bool FirePressed { get; private set; }

    public bool[] WeaponSwitchPressed = new bool[5];

    public bool DropPressed { get; private set; }
    public bool PickPressed { get; private set; }
    

    void Update()
    {
        MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        FirePressed = Input.GetMouseButton(0);

        for (int i = 0; i < WeaponSwitchPressed.Length; i++)
            WeaponSwitchPressed[i] = Input.GetKeyDown(KeyCode.Alpha1 + i);

        DropPressed = Input.GetKeyDown(KeyCode.G);
        PickPressed = Input.GetKeyDown(KeyCode.F);
        AimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
