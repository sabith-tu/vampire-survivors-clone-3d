using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("_SABI/Invoke/InvokeCustomLoopUsingCoroutine")]
public class InvokeCustomLoopUsingCoroutine : MonoBehaviour
{
    [Space(10)] [SerializeField] private bool isActive = true;

    [EnableIf("isActive")] [SerializeField] [Range(0.1f, 3f)]
    private float delayInLoop = 0.25f;

    [Space(10)] [EnableIf("isActive")] [SerializeField]
    private UnityEvent whatToInvoke;

    private WaitForSeconds _waitForSeconds;

    private void OnEnable()
    {
        if (isActive)
        {
            StartCoroutine(nameof(CustomRoutine));
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator CustomRoutine()
    {
        _waitForSeconds = new WaitForSeconds(delayInLoop);
        while (true)
        {
            whatToInvoke.Invoke();
            yield return _waitForSeconds;
        }
    }
}