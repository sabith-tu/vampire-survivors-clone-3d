using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using SABI.SOA;

[AddComponentMenu("_SABI/Invoke/InvokeBasedOnBoolSO")]
public class InvokeBasedOnBoolSO : MonoBehaviour
{
    [SerializeField] private BoolValueSO condition;
    [SerializeField] private bool useInverseOfCondition = false;
    [SerializeField] private bool invokeAutomatically = true;
    [SerializeField] private UnityEvent whatToInvoke;

    private void OnEnable()
    {
        if (invokeAutomatically) condition.OnValueChange += InvokeIt;
    }

    private void OnDisable()
    {
        if (invokeAutomatically) condition.OnValueChange -= InvokeIt;
    }

    [Button]
    public void InvokeIt()
    {
        if (useInverseOfCondition)
        {
            if (condition.GetValue() == false) whatToInvoke.Invoke();
        }
        else
        {
            if (condition.GetValue()) whatToInvoke.Invoke();
        }
    }
}