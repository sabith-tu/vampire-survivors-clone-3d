using System;
using UnityEngine;

public class PlayerSingletonReferences : MonoBehaviour
{
    public static PlayerSingletonReferences Instance;

    public PlayerHealth playerHealth;

    private void Awake()
    {
        Instance = this;
    }
}