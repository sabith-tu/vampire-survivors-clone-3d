using UnityEngine;

public class ZombieAttackPlayer : MonoBehaviour
{
    private bool _isCloseToPlayer = false;
    [SerializeField] private float damage = 2;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) _isCloseToPlayer = true;
    }

    private void Update()
    {
        if (_isCloseToPlayer) PlayerSingletonReferences.Instance.playerHealth.TakeDamage(damage * Time.deltaTime);
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) _isCloseToPlayer = false;
    }
}