using System;
using UnityEngine;

namespace SABI.SOA
{
    [CreateAssetMenu(menuName = "SO/Base/FloatValueSO")]
    public class FloatValueSO : ScriptableObject
    {
        [SerializeField] private float value;
        [SerializeField] private bool invokeActionOnValueChange = true;

        public Action OnValueChange;

        public float GetValue()
        {
            return value;
        }

        public void SetValue(float newValue)
        {
            value = newValue;
            if (invokeActionOnValueChange) OnValueChange?.Invoke();
        }
    }
}