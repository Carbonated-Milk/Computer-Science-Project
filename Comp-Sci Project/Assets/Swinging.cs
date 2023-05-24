using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swinging : MonoBehaviour
{

    private Transform vine;
    private Vector3 lastFrame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputP.inputs.space)
        {
            Player.singleton.enabled = true;
            Player.singleton.GetComponent<Rigidbody>().velocity = (transform.position - lastFrame) / (Time.deltaTime);
        }
        else if (vine != null)
        {
            Player.singleton.enabled = false;
            swinging();
        }
        lastFrame = transform.position;
    }

    public void swinging()
    {
        transform.position = vine.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null)
        {
            if (collision.collider.CompareTag("vine"))
            {

            }
        }
    }
}
