using System;
using UnityEngine;

namespace SABI.SOA
{
    [CreateAssetMenu(menuName = "SO/Base/StringValueSO")]
    public class StringValueSO : ScriptableObject
    {
        [SerializeField] private string value;
        [SerializeField] private bool invokeActionOnValueChange = true;

        public Action OnValueChange;

        public string GetValue()
        {
            return value;
        }

        public void SetValue(string newValue)
        {
            value = newValue;
            if (invokeActionOnValueChange) OnValueChange?.Invoke();
        }
    }
}