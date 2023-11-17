using System;
using System.Linq;
using DG.Tweening;
using SABI.SOA;
using UnityEngine;
using Random = UnityEngine.Random;

public class DroneController : MonoBehaviour
{
    [SerializeField] private Vector3 offsetFromPlayer;
    private float _timeLeftForNextShoot;
    [SerializeField] private FloatValueSO rateOfFire;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform closestEnemy;
    [SerializeField] private LayerMask layerToCheck;
    [SerializeField] private FloatValueSO droneSensorRadius;

    private void Update()
    {
        DroneFollow();
        DroneShoot();
    }

    private float _droneMoveTime;

    private void Start()
    {
        _droneMoveTime = Random.Range(0.2f, 2);
    }

    void DroneFollow()
    {
        transform.DOMove(
            PlayerSingletonReferences.Instance.transform.position + offsetFromPlayer, _droneMoveTime);
    }

    void DroneShoot()
    {
        DroneLookAtClosestEnemy();
        if (closestEnemy == null) return;
        if (_timeLeftForNextShoot <= 0)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            _timeLeftForNextShoot = rateOfFire.GetValue();
        }
        else
        {
            _timeLeftForNextShoot -= Time.deltaTime;
        }
    }

    bool DroneLookAtClosestEnemy()
    {
        Vector3 thisPossition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(thisPossition, droneSensorRadius.GetValue(), layerToCheck);
        if (colliders.Length > 0)
        {
            closestEnemy = colliders.OrderBy(o => (o.transform.position - thisPossition).sqrMagnitude)
                .FirstOrDefault().transform;
            transform.DOLookAt(closestEnemy.position, 0.1f);
            return true;
        }

        closestEnemy = null;
        return false;
    }
}