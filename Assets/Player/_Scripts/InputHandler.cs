using SABI.Player;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private PlayerMover playerMover;
    private Vector2 _joystickInputValue;

    private void Update()
    {
        Vector2 newdirection = new Vector2(joystick.Direction.x, joystick.Direction.y * 0.5f);
        playerMover.SetInputVector(joystick.Direction);
    }
}