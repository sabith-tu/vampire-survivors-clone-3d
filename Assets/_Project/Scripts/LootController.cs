using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SABI.SOA;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class LootController : MonoBehaviour
{
    [SerializeField] private LayerMask layersToCheck;
    [SerializeField] private float rateOfFire;
    [SerializeField] private FloatValueSO radius;
    [DisplayAsString] [SerializeField] private int XP;
    [SerializeField] private PlayerLevelingDataSO playerLevelingDataSo;
    private float _timeLeftForNextShoot;

    [SerializeField] private UnityEvent ShowUpgradeMenue;


    void Update()
    {
        if (_timeLeftForNextShoot <= 0)
        {
            _timeLeftForNextShoot = rateOfFire;

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius.GetValue(), layersToCheck);

            Vector3 thisPossition = transform.position;
            if (colliders.Length > 0)
            {
                foreach (var VARIABLE in colliders)
                {
                    VARIABLE.transform.DOMove(thisPossition, 0.3f).OnComplete(() =>
                    {
                        Destroy(VARIABLE.gameObject);
                        playerLevelingDataSo.AddXP();
                    });
                }
            }
        }
        else
        {
            _timeLeftForNextShoot -= Time.deltaTime;
        }
    }
}