
using UnityEngine;

public class ConnectLevelDataToUpgradeSystem : MonoBehaviour
{
    [SerializeField] private ShowUpdatePanel showUpdatePanel;
    [SerializeField] private PlayerLevelingDataSO playerLevelingDataSO;

    private void OnEnable()
    {
        playerLevelingDataSO.OnLevelCompleted += showUpdatePanel.Show;
    }
    
    private void OnDisable()
    {
        playerLevelingDataSO.OnLevelCompleted -= showUpdatePanel.Show;
    }
}