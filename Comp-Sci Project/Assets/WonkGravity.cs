using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonkGravity : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up, out hit, 5f);

        //changes gravity
        if (hit.collider != null && hit.collider.CompareTag("GravityChanger"))
        {
            var rotAmount = Quaternion.FromToRotation(transform.up, hit.normal);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotAmount * transform.rotation, .1f);


            var gravChange = Quaternion.FromToRotation(Physics.gravity.normalized, -transform.up);
            rb.velocity = gravChange * rb.velocity;
            Physics.gravity = -transform.up * Physics.gravity.magnitude;
        }
    }
}
