using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using SABI.SOA;

[AddComponentMenu("_SABI/Invoke/InvokeOnActionSOExecuted")]
public class InvokeOnActionSOExecuted : MonoBehaviour
{
    [Space(10)] [SerializeField] private bool isActive = true;

    [EnableIf("isActive")] [SerializeField]
    ActionSO actionSo;

    [Space(5)] [EnableIf("isActive")] [SerializeField]
    private UnityEvent whatToInvoke;

    private void OnEnable()
    {
        if (isActive) actionSo.action += InvokeIt;
    }


    private void OnDisable()
    {
        if (isActive) actionSo.action -= InvokeIt;
    }

    [Button]
    private void InvokeIt()
    {
        if (isActive) whatToInvoke.Invoke();
    }
}