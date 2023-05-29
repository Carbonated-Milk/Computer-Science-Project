using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonkGravity : MonoBehaviour
{
    public static WonkGravity singleton;
    private Rigidbody rb;
    public float adjustSpeed = 15;

    private void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private Quaternion targetRot;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up, out hit, 5f);

        //changes gravity
        if (hit.collider != null && hit.collider.CompareTag("GravityChanger"))
        {
            StopAllCoroutines();
            var rotAmount = Quaternion.FromToRotation(transform.up, hit.normal);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotAmount * transform.rotation, adjustSpeed * Time.deltaTime);


            var gravChange = Quaternion.FromToRotation(Physics.gravity.normalized, -transform.up);
            rb.velocity = gravChange * rb.velocity;
            Physics.gravity = -transform.up * Physics.gravity.magnitude;
        }
    }

    public void ChangeGravity(Vector3 newDown)
    {
        Physics.gravity = newDown.normalized * Physics.gravity.magnitude;
        StartCoroutine(ChangeRot());
    }

    public IEnumerator ChangeRot()
    {
        float turnTime = .2f;
        float startTime = Time.time;
        var startRot = transform.rotation;
        var endRot = Quaternion.FromToRotation(transform.up, -Physics.gravity) * startRot;
        while (startTime + turnTime > Time.time)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, Functions.SmoothStep((Time.time - startTime)/turnTime));
            yield return null;
        }
        transform.rotation = endRot;
    }
}
