using Sirenix.OdinInspector;

namespace SABI.SOA
{
    [System.Serializable]
    public class BoolReference
    {
        public bool UseConstant = false;
        [ShowIf("UseConstant")] public bool ConstantValue;
        [HideIf("UseConstant")] public BoolValueSO Variable;

        public bool Value
        {
            get { return UseConstant ? ConstantValue : Variable.GetValue(); }
            set
            {
                if (UseConstant) ConstantValue = value;
                else Variable.SetValue(value);
            }
        }
    }
}