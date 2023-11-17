using System;
using UnityEngine;

namespace SABI.SOA
{
    [CreateAssetMenu(menuName = "SO/Base/IntValueSO")]
    public class IntValueSO : ScriptableObject
    {
        [SerializeField] private int value;
        [SerializeField] private bool invokeActionOnValueChange = true;

        public Action OnValueChange;

        public int GetValue()
        {
            return value;
        }

        public void SetValue(int newValue)
        {
            value = newValue;
            if (invokeActionOnValueChange) OnValueChange?.Invoke();
        }
    }
}