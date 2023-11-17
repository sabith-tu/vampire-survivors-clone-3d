using System.Collections;
using UnityEngine;

public class CustomAbility_KillAllEnemy : MonoBehaviour
{
    public void ActivateAbility() => StartCoroutine(nameof(SpawnThousandSlash));

    [SerializeField] private float abilityLife = 5;
    private WaitForSeconds _waitForSeconds;
    [SerializeField] private GameObject abilityVFX;
    [SerializeField] private LayerMask whoToAttack;

    private void Start() => _waitForSeconds = new WaitForSeconds(abilityLife);

    IEnumerator SpawnThousandSlash()
    {
        Transform spawnedAbility;
        abilityVFX.SetActive(true);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20, whoToAttack);

        foreach (var VARIABLE in colliders)
        {
            VARIABLE.GetComponent<ZombieGotAttacked>().ZombieTakeDamage(100);
        }


        yield return _waitForSeconds;
        abilityVFX.SetActive(false);
    }
}