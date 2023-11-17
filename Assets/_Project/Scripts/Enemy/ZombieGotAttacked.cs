using System;
using System.Collections;
using SABI.SOA;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class ZombieGotAttacked : MonoBehaviour
{
    private Color _originalColur;
    private WaitForSeconds _waitForSeconds;
    private Material _zombieMeshMaterial;

    [SerializeField] private FloatValueSO gunBulletDamage;
    [SerializeField] private FloatValueSO droneBulletDamage;
    [SerializeField] private FloatValueSO blackholeDamage;
    [SerializeField] private FloatValueSO lightningDamage;
    [SerializeField] private float zombieHealth;
    [SerializeField] private GameObject lootToInstanciate;
    [SerializeField] private UnityEvent onZombieIsDead;
    [SerializeField] private SkinnedMeshRenderer zombieMesh;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GunBullet"))
        {
            ZombieTakeDamage(gunBulletDamage.GetValue());
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("DroneBullet"))
        {
            ZombieTakeDamage(droneBulletDamage.GetValue());
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("BlackHole"))
        {
            ZombieTakeDamage(blackholeDamage.GetValue());
        }
        else if (other.CompareTag("Lightning"))
        {
            ZombieTakeDamage(lightningDamage.GetValue());
        }
    }

    public void ZombieTakeDamage(float damage)
    {
        zombieHealth -= damage;
        StartCoroutine(nameof(ZombieFlashToIndicateDamage));

        if (zombieHealth <= 0)
        {
            Instantiate(lootToInstanciate, transform.position, quaternion.identity);
            EnemyManager.Instance.EnemyDestroyed();
            onZombieIsDead.Invoke();
            Destroy(this.gameObject, 0.3f);
        }
    }


    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(0.2f);
        _zombieMeshMaterial = zombieMesh.material;
        _originalColur = _zombieMeshMaterial.color;
    }

    private IEnumerator ZombieFlashToIndicateDamage()
    {
        _zombieMeshMaterial.color = Color.red;
        yield return _waitForSeconds;
        _zombieMeshMaterial.color = _originalColur;
    }
}