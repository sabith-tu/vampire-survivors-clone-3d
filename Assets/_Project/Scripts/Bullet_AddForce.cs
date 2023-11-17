using System.Collections;
using UnityEngine;

public class Bullet_AddForce : MonoBehaviour
{
    [SerializeField] float forceAmount;

    private IEnumerator Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * forceAmount);
        yield return new WaitForSeconds(2);
        if (this.gameObject.activeInHierarchy) Destroy(this.gameObject);
    }
}