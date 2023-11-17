using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAbility_FillHealth : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;


    public void ActivateAbility() => playerHealth.SetPlayerHealthToMax();
}