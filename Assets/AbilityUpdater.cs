using System.Collections.Generic;
using SABI.SOA;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class AbilityUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    [Space(15)] [SerializeField] private List<AbilityUpdateData> abilityUpdateDatas;

    private bool isAbilityUnlocked = false;
    [Space(15)] [SerializeField] private bool shouldUnloackeAbilityAsFirstUpdate = false;

    [Space(5)] [ShowIf("shouldUnloackeAbilityAsFirstUpdate")] [SerializeField]
    private GameObject gameObjectToEnable;

    [Space(5)] [SerializeField] private string UpdateUiToShow = "No string ";
    [Space(5)] [SerializeField] private string UnlockUiToShow = "No string ";


    private void Start()
    {
        if (shouldUnloackeAbilityAsFirstUpdate)
        {
            if (isAbilityUnlocked)
            {
            }
            else
            {
                if (gameObjectToEnable.activeInHierarchy)
                {
                    isAbilityUnlocked = true;
                }
                else
                {
                }
            }
        }

        if (shouldUnloackeAbilityAsFirstUpdate && !isAbilityUnlocked)
        {
            text.text = UnlockUiToShow;
        }
        else
        {
            text.text = UpdateUiToShow;
        }
    }


    public void UpdateAbility()
    {
        if (shouldUnloackeAbilityAsFirstUpdate && !isAbilityUnlocked)
        {
            gameObjectToEnable.SetActive(true);
            text.text = UpdateUiToShow;
        }
        else
        {
            foreach (var VARIABLE in abilityUpdateDatas)
            {
                VARIABLE.abilityValueSO.SetValue(VARIABLE.abilityValueSO.GetValue() + VARIABLE.GetValueToIncrement());
            }
        }
    }

    [System.Serializable]
    public class AbilityUpdateData
    {
        [SerializeField] public FloatValueSO abilityValueSO;

        [SerializeField] bool usePercentageValue = true;

        [SerializeField] public float valueToIncrement;

        public float GetValueToIncrement()
        {
            return (usePercentageValue ? abilityValueSO.GetValue() * (valueToIncrement / 100) : valueToIncrement);
        }
    }
}