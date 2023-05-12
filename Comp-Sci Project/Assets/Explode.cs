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
        BlowUp();
    }

    public void BlowUp()
    {
        Collider[] stuff = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach (Collider item in stuff)
        {
            Rigidbody rb = item.attachedRigidbody;
            if (item.gameObject == gameObject) continue;
            if (rb != null)
            {
                Vector3 relDist = rb.transform.position - transform.position;
                rb.AddForce(relDist.normalized * explodePower * Functions.SmoothStep(1 - relDist.magnitude/explodeRadius), ForceMode.Impulse);
            }
        }
        ParticleSystem particalInstance = Instantiate(particals).GetComponent<ParticleSystem>();
        particalInstance.transform.position = transform.position;

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}
