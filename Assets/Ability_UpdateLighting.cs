using System.Collections;
using System.Collections.Generic;
using SABI.SOA;
using TMPro;
using UnityEngine;

public class Ability_UpdateLighting : MonoBehaviour
{
    private int updateLevel = 1;

    //[SerializeField] private FloatValueSO Range;
    [SerializeField] private FloatValueSO Frequncy;
    [SerializeField] private IntValueSO Damage;
    [SerializeField] private GameObject lightingAbility;

    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        UiUpdate();
    }

    void UiUpdate()
    {
        switch (updateLevel)
        {
            case 1:
                SetUiEnableLighting();

                break;
            case 2:
                SetUiUpgradeFrequwncy();
                break;

            case 3:
                SetUiUpgradeFrequwncy();

                break;
            case 4:
                SetUiUpgradeDamage();
                break;
            case 5:
                SetUiUpgradeFrequwncy();
                break;
            case 6:
                SetUiUpgradeDamage();
                break;
            case 7:
                SetUiUpgradeFrequwncy();
                break;
            case 8:
                SetUiUpgradeDamage();
                break;
            case 9:
                SetUiUpgradeFrequwncy();
                break;
            case 10:
                SetUiUpgradeDamage();
                break;
        }
    }


    public void AbilitySelected()
    {
        UiUpdate();
        switch (updateLevel)
        {
            case 1:
                EnableLighting();
                break;
            case 2:
                UpgradeFrequwncy();
                break;

            case 3:
                UpgradeDamage();

                break;
            case 4:
                UpgradeFrequwncy();
                break;
            case 5:
                UpgradeDamage();
                break;
            case 6:
                UpgradeFrequwncy();
                break;
            case 7:
                UpgradeDamage();
                break;
            case 8:
                UpgradeFrequwncy();
                break;
            case 9:
                UpgradeFrequwncy();
                break;
            case 10:
                UpgradeDamage();
                break;
        }

        UiUpdate();
        updateLevel++;
        UiUpdate();
    }

    private void EnableLighting()
    {
        lightingAbility.SetActive(true);
    }


    private void UpgradeFrequwncy()
    {
        Frequncy.SetValue(Frequncy.GetValue() - 1);
    }

    private void UpgradeDamage()
    {
        Damage.SetValue(Damage.GetValue() + 1);
    }
    //_____________________________________________

    private void SetUiEnableLighting()
    {
        text.text = "Unloack lighting";
    }

    private void SetUiUpgradeRange()
    {
        text.text = "lighting Range + 0.1";
    }

    private void SetUiUpgradeFrequwncy()
    {
        text.text = "lighting Frequncy - 1";
    }

    private void SetUiUpgradeDamage()
    {
        text.text = "lighting Damage + 0.1";
    }
}