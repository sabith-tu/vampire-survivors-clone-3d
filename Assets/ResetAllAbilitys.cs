using System.Collections;
using System.Collections.Generic;
using SABI.SOA;
using UnityEngine;

public class ResetAllAbilitys : MonoBehaviour
{
    public List<TyesOfAbilityDefaultValues> abilityTypes;

    public void ResetAbilitys()
    {
        foreach (var abilityCollection in abilityTypes)
        {
            foreach (var ability in abilityCollection.floatAbilitys)
            {
                ability.ResetToDefult();
            }
        }
    }


    [System.Serializable]
    public class AbilityDefaultValues
    {
        public FloatValueSO abilityValue;
        public float defultValue;
        public void ResetToDefult() => abilityValue.SetValue(defultValue);
    }

    [System.Serializable]
    public class TyesOfAbilityDefaultValues
    {
        public string abilityName;
        public List<AbilityDefaultValues> floatAbilitys;
    }
}