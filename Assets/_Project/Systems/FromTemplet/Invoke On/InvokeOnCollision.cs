using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("_SABI/Invoke/InvokeOnCollision")]
public class InvokeOnCollision : MonoBehaviour
{
    [Space(10)] [SerializeField] private bool isActive = true;

    [EnableIf("isActive")] [SerializeField]
    private bool usePlayerTag = true;

    [EnableIf("isActive")] [HideIf("usePlayerTag")] [SerializeField]
    private string tagToCheck;

    [EnableIf("isActive")] [SerializeField]
    private bool shouldLog = false;

    [PropertySpace(SpaceAfter = 10)] [EnableIf("isActive")] [ShowIf("shouldLog")] [SerializeField]
    private string customMessage;

    [EnableIf("isActive")] [SerializeField] [TabGroup("Trigger")]
    private bool isTriggerEnterActive;

    [ShowIf("isTriggerEnterActive")] [SerializeField] [TabGroup("Trigger")]
    private UnityEvent InvokeOnTriggerEnter;

    [EnableIf("isActive")] [SerializeField] [TabGroup("Trigger")]
    private bool isTriggerExitActive;

    [ShowIf("isTriggerExitActive")] [SerializeField] [TabGroup("Trigger")]
    private UnityEvent InvokeOnTriggerExit;

    [EnableIf("isActive")] [SerializeField] [TabGroup("Collision")]
    private bool isColliderEnterActive;

    [ShowIf("isColliderEnterActive")] [SerializeField] [TabGroup("Collision")]
    private UnityEvent InvokeOnColliderEnter;

    [EnableIf("isActive")] [SerializeField] [TabGroup("Collision")]
    private bool isColliderExitActive;

    [ShowIf("isColliderExitActive")] [SerializeField] [TabGroup("Collision")]
    private UnityEvent InvokeOnColliderExit;

    [Space(5)] [SerializeField] private bool shouldDestroyCollided = false;

    private void Start()
    {
        if (usePlayerTag && isActive) tagToCheck = "Player";
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isActive == false) return;
        if (other.collider.CompareTag(tagToCheck) == false) return;
        if (isColliderEnterActive == false) return;

        if (shouldLog)
            Debug.Log(
                $" Collision Enter with tag {tagToCheck} from game object {other.gameObject.name} : {customMessage} ");
        InvokeOnColliderEnter.Invoke();

        if (shouldDestroyCollided) Destroy(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive == false) return;
        if (other.CompareTag(tagToCheck) == false) return;
        if (isTriggerEnterActive == false) return;

        if (shouldLog)
            Debug.Log(
                $" Trigger Enter with tag {tagToCheck}  from game object {other.gameObject.name} : {customMessage} ");
        InvokeOnTriggerEnter.Invoke();

        if (shouldDestroyCollided) Destroy(other.gameObject);
    }

    private void OnCollisionExit(Collision other)
    {
        if (isActive == false) return;
        if (other.collider.CompareTag(tagToCheck) == false) return;
        if (isColliderExitActive == false) return;

        if (shouldLog)
            Debug.Log(
                $" Collision Exit with tag {tagToCheck}  from game object {other.gameObject.name} : {customMessage} ");
        InvokeOnColliderExit.Invoke();

        if (shouldDestroyCollided) Destroy(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (isActive == false) return;
        if (other.CompareTag(tagToCheck) == false) return;
        if (isTriggerExitActive == false) return;

        if (shouldLog)
            Debug.Log(
                $" Trigger Exit with tag {tagToCheck}  from game object {other.gameObject.name} : {customMessage} ");
        InvokeOnTriggerExit.Invoke();

        if (shouldDestroyCollided) Destroy(other.gameObject);
    }
}