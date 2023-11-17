using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("_SABI/Invoke/InvokeWithDelay")]
public class InvokeWithDelay : MonoBehaviour
{
    [Space(10)] [SerializeField] private bool isActive = true;

    [Space(10)] [EnableIf("isActive")] [SerializeField]
    private float delay;

    [Space(5)] [EnableIf("isActive")] [SerializeField]
    private UnityEvent whatToInvoke;

    [Button]
    public void InvokeGivenEventWithDelay()
    {
        if (isActive) Invoke(nameof(InvokeEvent), delay);
    }

    void InvokeEvent()
    {
        whatToInvoke.Invoke();
    }
}