using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
[AddComponentMenu("_SABI/Invoke/InvokeWithChance")]
public class InvokeWithChance : MonoBehaviour
{
    [SerializeField] [Range(0.01f, 1f)] private float chance = 0.5f;

    [SerializeField] private UnityEvent whatToInvoke;

    [Button]
    public void InvokeIt()
    {
        if (Chance.GetRandomChance(chance)) whatToInvoke.Invoke();
    }
}