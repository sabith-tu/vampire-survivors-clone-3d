using System.Collections;
using DG.Tweening;
using SABI.SOA;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlackHole_Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnTransforms;
    private float _timeLeftForNextShoot;
    [SerializeField] private FloatValueSO blackholeFrequncy;
    [SerializeField] private FloatValueSO blackholeSize;

    [SerializeField] private float blackHoleSizeIncreasingDuration = 1;
    [SerializeField] private float blackholeLife = 5;
    private WaitForSeconds _waitForSeconds;
    [SerializeField] private Transform blackholePrefab;

    private int randomPositionToSpawn = 1;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(blackholeLife);
    }

    IEnumerator SpawnBlackhole()
    {
        Transform _spawnedBlackhole;
        randomPositionToSpawn = Random.Range(0, spawnTransforms.Length);
        _spawnedBlackhole = Instantiate(blackholePrefab, spawnTransforms[randomPositionToSpawn].position,
            quaternion.identity);

        _spawnedBlackhole.transform.parent = null;
        _spawnedBlackhole.localScale = Vector3.one * 0.01f;
        _spawnedBlackhole.DOScale(Vector3.one * blackholeSize.GetValue(), blackHoleSizeIncreasingDuration);

        yield return _waitForSeconds;

        _spawnedBlackhole.DOScale(Vector3.one * 0.01f, blackHoleSizeIncreasingDuration)
            .OnComplete(() => Destroy(_spawnedBlackhole.gameObject));
    }

    void Update()
    {
        if (_timeLeftForNextShoot <= 0)
        {
            StartCoroutine(nameof(SpawnBlackhole));
            _timeLeftForNextShoot = blackholeFrequncy.GetValue();
        }
        else
        {
            _timeLeftForNextShoot -= Time.deltaTime;
        }
    }
}