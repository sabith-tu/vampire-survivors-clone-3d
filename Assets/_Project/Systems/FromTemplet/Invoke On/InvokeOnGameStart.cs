using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
[AddComponentMenu("_SABI/Invoke/InvokeOnGameStart")]
public class InvokeOnGameStart : MonoBehaviour
{
    [Space(10)] [SerializeField] private bool isActive = true;


    [Space(10)] [EnableIf("isActive")] [SerializeField]
    private bool shouldInvokeOnAwake = false;

    [ShowIf("shouldInvokeOnAwake")] [EnableIf("isActive")] [SerializeField]
    private UnityEvent OnAwake;

    [Space(10)] [EnableIf("isActive")] [SerializeField]
    private bool shouldInvokeOnStart = false;

    [ShowIf("shouldInvokeOnStart")] [EnableIf("isActive")] [SerializeField]
    private UnityEvent whatToInvoke;

    private void Awake()
    {
        if (isActive && shouldInvokeOnAwake) OnAwake.Invoke();
    }

    private void Start()
    {
        if (isActive && shouldInvokeOnStart) whatToInvoke.Invoke();
    }
}