using System;
using UnityEngine;


namespace SABI.SOA
{
    [CreateAssetMenu(menuName = "SO/Base/boolValueSO")]
    public class BoolValueSO : ScriptableObject
    {
        [SerializeField] private bool value;
        [SerializeField] private bool invokeActionOnValueChange = true;

        public Action OnValueChange;

        public bool GetValue()
        {
            return value;
        }

        public void SetValue(bool newValue)
        {
            value = newValue;
            if (invokeActionOnValueChange) OnValueChange?.Invoke();
        }
    }
}