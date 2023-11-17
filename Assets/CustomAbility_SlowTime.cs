using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

public class CustomAbility_SlowTime : MonoBehaviour
{
    private WaitForSeconds _waitForSeconds;

    [SerializeField] private float abilityLife = 3;
    [SerializeField] private GameObject vfx;
    [SerializeField] private MMTimeManager timeManager;
    private void Start() => _waitForSeconds = new WaitForSeconds(abilityLife);
    public void ActivateAbility() => StartCoroutine(nameof(SpawnThousandSlash));

    IEnumerator SpawnThousandSlash()
    {
        vfx.SetActive(true);
        timeManager.SetTimeScaleTo(0.2f);
        Time.fixedDeltaTime = 0.02F * 0.2f;
        
        yield return _waitForSeconds;
        
        vfx.SetActive(false);
        timeManager.SetTimeScaleTo(1f);
        Time.fixedDeltaTime = 0.02F;
    }
}