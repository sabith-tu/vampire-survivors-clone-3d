using Redcode.Extensions;
using UnityEngine;

public class ZombiMoveTowards : MonoBehaviour
{
    [SerializeField] private int speed = 100;
    private Rigidbody _rigidbody;
    private Vector3 playerPosition;
    private Vector3 thisPosition;
    private int randomSpeedOffset;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        randomSpeedOffset = Random.Range(0, 100);
        speed += randomSpeedOffset;
        
    }


    private void FixedUpdate()
    {
        playerPosition = PlayerSingletonReferences.Instance.transform.position;
        thisPosition = transform.position;
        _rigidbody.velocity =
            (playerPosition - thisPosition).normalized * speed * Time.fixedDeltaTime;

        transform.LookAt(playerPosition.WithY(thisPosition.y));
    }
}