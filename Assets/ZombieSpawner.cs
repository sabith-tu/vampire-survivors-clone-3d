using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    // [System.Serializable]
    // public class ZombiType
    // {
    //     public Transform zombiePrefab;
    //     public float chanceToSpawn = 0.1f
    // }

    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private Transform basicZombiePrefab;
    [SerializeField] private Transform fastZombiePrefab;
    [SerializeField] private Transform strongZombiePrefab;
    [SerializeField] private float delayBtwUpdateLoop = 1;
    [SerializeField] private PlayerLevelingDataSO playerLevelingDataSO;
    private WaitForSeconds _waitForSeconds;
    private Vector3 _whereToSpawn;
    private Transform _whichZombieToSpawn;

    private IEnumerator Start()
    {
        _waitForSeconds = new WaitForSeconds(delayBtwUpdateLoop);


        while (true)
        {
            if (EnemyManager.Instance.CanSpawnNewEnemy())
            {
                _whereToSpawn = spawnLocations[Random.Range(0, spawnLocations.Length)].position;
                FindWhichZombieToSpawn();
                Instantiate(basicZombiePrefab, _whereToSpawn, Quaternion.identity);
                EnemyManager.Instance.RegisterNewEnemy();
            }

            yield return _waitForSeconds;
        }
    }

    private void FindWhichZombieToSpawn()
    {
        if (playerLevelingDataSO.CurrentPlayerLevel < 2) _whichZombieToSpawn = basicZombiePrefab;
        else if (playerLevelingDataSO.CurrentPlayerLevel < 5)
        {
            _whichZombieToSpawn = Chance.GetRandomChance(0.2f) ? fastZombiePrefab : basicZombiePrefab;
        }
        else //if (playerLevelingDataSO.CurrentPlayerLevel < 10)
        {
            _whichZombieToSpawn = Chance.GetRandomChance(0.5f)
                ? basicZombiePrefab
                : (Chance.GetRandomChance(0.5f) ? fastZombiePrefab : strongZombiePrefab);
        }
    }
}