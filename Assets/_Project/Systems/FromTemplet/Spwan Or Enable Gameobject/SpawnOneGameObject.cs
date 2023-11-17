using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[AddComponentMenu("_SABI/Gameobject/SpawnOneGameObject")]
public class SpawnOneGameObject : MonoBehaviour
{
    [TabGroup("Settings")] [HideIf("spawnOnStartRandom")] [SerializeField]
    private bool spawnOnStartWithIndex = false;

    [TabGroup("Settings")] [ShowIf("spawnOnStartWithIndex")] [SerializeField]
    private int spawnOnStartIndex = 0;

    [TabGroup("Settings")] [HideIf("spawnOnStartWithIndex")] [SerializeField]
    private bool spawnOnStartRandom = false;


    [TabGroup("References")] [SerializeField]
    private GameObject[] allItems;


    private void Start()
    {
        if (spawnOnStartWithIndex)
        {
            SpawnOneWithIndex(spawnOnStartIndex);
        }
        else if (spawnOnStartRandom)
        {
            SpawnOneRandomly();
        }
    }


    public void SpawnOneWithIndex(int index)
    {
        if ((index < allItems.Length) && (index >= 0))
        {
            Instantiate(allItems[index], transform.position, transform.rotation);
        }
        else
        {
            Debug.LogWarning("EnableOneWithIndex Out of range index = " + index +
                             " Possible values = 0 - " + allItems.Length);
        }
    }


    public void SpawnOneRandomly()
    {
        SpawnOneWithIndex(Random.Range(0, allItems.Length));
    }
}