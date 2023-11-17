using DG.Tweening;
using SABI.SOA;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;

    [SerializeField] private Transform test;

    [SerializeField] private Transform bullet;

    [SerializeField] private FloatValueSO rateOfFire;

    private float _timeLeftForNextShoot = 0;

    private bool canFire = true;

    void Update()
    {
        float distance = 5;
        Vector2 joystickDirection = joystick.Direction;


        Look(joystickDirection.normalized * distance);
        Fire(joystickDirection);
    }

    void SetCanFireTrue() => canFire = true;

    void Look(Vector2 joystickDirection)
    {
        if (joystickDirection != Vector2.zero)
        {
            test.position = new Vector3(joystickDirection.x, 0, joystickDirection.y) + transform.position;
            transform.DOLookAt(test.position, 0.2f);
        }
    }

    void Fire(Vector2 joystickDirection)
    {
        if (_timeLeftForNextShoot <= 0)
        {
            if (joystickDirection.magnitude > 0.8f)
            {
                Instantiate(bullet, transform.position, transform.rotation);
                _timeLeftForNextShoot = rateOfFire.GetValue();
            }
        }
        else
        {
            _timeLeftForNextShoot -= Time.deltaTime;
        }
    }
}