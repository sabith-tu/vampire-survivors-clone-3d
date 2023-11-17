using UnityEngine;

public static class Chance
{
    public static bool GetRandomChance(float probability)
    {
        return (Random.Range(0f, 1f) <= probability);
    }
}