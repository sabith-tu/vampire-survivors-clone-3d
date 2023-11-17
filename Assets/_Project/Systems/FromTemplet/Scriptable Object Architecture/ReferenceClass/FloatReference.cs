using Sirenix.OdinInspector;

namespace SABI.SOA
{
    [System.Serializable]
    public class FloatReference
    {
        public bool UseConstant = false;
        [ShowIf("UseConstant")] public float ConstantValue;
        [HideIf("UseConstant")] public FloatValueSO Variable;

        public float Value
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