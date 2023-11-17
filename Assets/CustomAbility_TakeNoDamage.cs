using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CustomAbility_TakeNoDamage : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    private WaitForSeconds _waitForSeconds;

    [SerializeField] public float abilityLife = 3;
    [SerializeField] public GameObject abilityVFX;


    private void Start() => _waitForSeconds = new WaitForSeconds(abilityLife);

    IEnumerator SpawnThousandSlash()
    {
        abilityVFX.SetActive(true);
        playerHealth.SetIsOnDontTakeDamageAbility(true);
        yield return _waitForSeconds;
        playerHealth.SetIsOnDontTakeDamageAbility(false);
        abilityVFX.SetActive(false);
    }

    public void ActivateAbility()
    {
        StartCoroutine(nameof(SpawnThousandSlash));
    }
}