using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swinging : MonoBehaviour
{

    private Transform vine;
    private Vector3 lastFrame;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        swinging();
    }

    public void swinging()
    {
        if (vine == null) { return; }
       
        Player.singleton.enabled = false;
        lastFrame = vine.position;
        transform.position = vine.position;
        Player.singleton.MoveCamera();
        if (InputP.inputs.space)
        {
            rb.velocity = Vector3.zero;
            rb.velocity = (vine.position - lastFrame) / Time.deltaTime + transform.up * Player.singleton.jumpPower;
            vine = null;
            Player.singleton.enabled = true;
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject != null)
        {
            if (collider.CompareTag("vine"))
            {
                vine = collider.transform;
                vine.GetComponent<Rigidbody>().AddForce(rb.velocity * 1.5f, ForceMode.VelocityChange);
            }
        }
    }
}
