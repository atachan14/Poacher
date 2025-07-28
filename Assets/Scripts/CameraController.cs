using UnityEngine;

public enum CameraMode
{
    rambo,
    gameOver
}

public class CameraController : MonoBehaviour
{
    CameraMode mode = CameraMode.rambo; 

    // Update is called once per frame
    void Update()
    {
        
    }
}
