using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("_SABI/Invoke/InvokeManuallyWithButton")]
public class InvokeManuallyWithButton : MonoBehaviour
{
    [SerializeField] UnityEvent whatToInvoke;

    [Button]
    public void ManuelInvoke()
    {
        whatToInvoke.Invoke();
    }
}