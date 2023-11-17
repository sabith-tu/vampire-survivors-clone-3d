using Sirenix.OdinInspector;


namespace SABI.SOA
{
    [System.Serializable]
    public class StringReference
    {
        public bool UseConstant = false;
        [ShowIf("UseConstant")] public string ConstantValue;
        [HideIf("UseConstant")] public StringValueSO Variable;

        public string Value
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