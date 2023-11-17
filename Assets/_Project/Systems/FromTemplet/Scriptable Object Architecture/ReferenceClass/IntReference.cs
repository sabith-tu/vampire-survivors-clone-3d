using Sirenix.OdinInspector;

namespace SABI.SOA
{
    [System.Serializable]
    public class IntReference
    {
        public bool UseConstant = false;
        [ShowIf("UseConstant")] public int ConstantValue;
        [HideIf("UseConstant")] public IntValueSO Variable;

        public int Value
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