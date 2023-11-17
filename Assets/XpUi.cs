using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XpUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI XpText;
    [SerializeField] private Slider XpSlider;
    [SerializeField] private PlayerLevelingDataSO playerLevelingDataSo;

    private void Start()
    {
        UpdateXPUi();
    }

    private void OnEnable()
    {
        playerLevelingDataSo.OnXpCollected += UpdateXPUi;
        playerLevelingDataSo.OnLevelCompleted += UpdateXPUi;
        playerLevelingDataSo.OnLevelCompleted += UpdateLevelUi;
    }

    private void OnDisable()
    {
        playerLevelingDataSo.OnXpCollected -= UpdateXPUi;
        playerLevelingDataSo.OnLevelCompleted -= UpdateXPUi;
        playerLevelingDataSo.OnLevelCompleted -= UpdateLevelUi;
    }

    void UpdateXPUi()
    {
        XpSlider.value = playerLevelingDataSo.HowMuchPercentageXpIsCollectedForNextLevel();
    }

    void UpdateLevelUi()
    {
        XpText.text = "LVL : " + playerLevelingDataSo.CurrentPlayerLevel.ToString();
    }
}