using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    //[SerializeField] private int enemyLimit = 250;
    [SerializeField] private PlayerLevelingDataSO playerLevelingDataSO;
    public static EnemyManager Instance { get; private set; }

    private int totalEnemys;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterNewEnemy()
    {
        totalEnemys++;
        UpdateUi();
    }

    public void EnemyDestroyed()
    {
        totalEnemys--;
        UpdateUi();
    }

    public bool CanSpawnNewEnemy() => totalEnemys < playerLevelingDataSO.GetMaximumBasicEnemysForCurrentLevel();

    private void UpdateUi() => text.text = "totalEnemys : " + totalEnemys.ToString();
}