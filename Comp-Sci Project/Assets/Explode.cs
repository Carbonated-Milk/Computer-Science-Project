using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject particals;
    public float explodeRadius = 1f;
    public float explodePower;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject);
        BlowUp();
    }

    public void BlowUp()
    {
        Collider[] stuff = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach (Collider item in stuff)
        {
            Rigidbody rb = item.attachedRigidbody;
            if (rb != null)
            {
                var relDist = rb.transform.position - transform.position;
                rb.AddExplosionForce(explodePower / relDist.magnitude, transform.position, 300);
            }
        }
        var part = Instantiate(particals);
        part.transform.position = transform.position;
        Destroy(part, 5f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, explodeRadius);
    }
}
