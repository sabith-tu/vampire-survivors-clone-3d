using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAbility_AddNewDrone : MonoBehaviour
{
    private int _numberOfExtraDrones = 0;
    private int _numberOfMaxDroens = 3;

    [SerializeField] private GameObject extraDrone1;
    [SerializeField] private GameObject extraDrone2;
    [SerializeField] private GameObject extraDrone3;

    public void ActivateAbility()
    {
        switch (_numberOfExtraDrones)
        {
            case 0:
                extraDrone1.SetActive(true);
                _numberOfExtraDrones = 1;
                break;
            case 1:
                extraDrone2.SetActive(true);
                _numberOfExtraDrones = 2;
                break;
            case 2:
                extraDrone3.SetActive(true);
                _numberOfExtraDrones = 3;
                break;
        }
    }
}