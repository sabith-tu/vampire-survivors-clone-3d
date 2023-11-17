using System;
using System.Collections;
using System.Collections.Generic;
using SABI.SOA;
using UnityEngine;

public class CloseMelay : MonoBehaviour
{
    private float _timeLeftForNextShoot;
    [SerializeField] private FloatValueSO melayDelayInLoop;
    [SerializeField] private FloatValueSO damage;
    [SerializeField] private FloatValueSO range;
    [SerializeField] private GameObject vfx;
    [SerializeField] private LayerMask layersToCheck;
    [SerializeField] private float disableafter = 0.25f;

    private void Start()
    {
        vfx.SetActive(false);
    }

    void Update()
    {
        if (_timeLeftForNextShoot <= 0)
        {
            vfx.SetActive(true);
            Collider[] colliders = Physics.OverlapSphere(transform.position, range.GetValue(), layersToCheck);
            if (colliders.Length > 0)
            {
                foreach (var VARIABLE in colliders)
                {
                    VARIABLE.GetComponent<ZombieGotAttacked>().ZombieTakeDamage(damage.GetValue());
                }
            }

            Invoke(nameof(DisableVFX), disableafter);
            _timeLeftForNextShoot = melayDelayInLoop.GetValue();
        }
        else
        {
            _timeLeftForNextShoot -= Time.deltaTime;
        }
    }

    void DisableVFX() => vfx.SetActive(false);
}