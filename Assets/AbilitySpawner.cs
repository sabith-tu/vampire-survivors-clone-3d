using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SABI.SOA;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AbilitySpawner : MonoBehaviour
{
    private float _timeLeftForNextShoot;
    [SerializeField] private FloatValueSO abilityDelayInLoop;

    [SerializeField] private float abilityLife = 5;
    private WaitForSeconds _waitForSeconds;
    [SerializeField] private Transform abilityPrefab;

    private int randomPositionToSpawn = 1;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(abilityLife);
    }

    IEnumerator SpawnBlackhole()
    {
        Transform spawnedAbility;

        spawnedAbility = Instantiate(abilityPrefab, this.transform.position,
            quaternion.identity);

        spawnedAbility.transform.parent = null;

        yield return _waitForSeconds;

        Destroy(spawnedAbility.gameObject);
    }

    void Update()
    {
        if (_timeLeftForNextShoot <= 0)
        {
            StartCoroutine(nameof(SpawnBlackhole));
            _timeLeftForNextShoot = abilityDelayInLoop.GetValue();
        }
        else
        {
            _timeLeftForNextShoot -= Time.deltaTime;
        }
    }
}