using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private Vector3 followOffset;

    private void LateUpdate()
    {
        transform.position = followOffset + followTarget.position;
    }
}