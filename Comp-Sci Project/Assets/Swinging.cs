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
        swinging();
        
    }

    public void swinging()
    {
        if (vine == null) { return; }
       
        Player.singleton.enabled = false;
        lastFrame = transform.position;
        transform.position = vine.position;
        if (InputP.inputs.space)
        {
            vine = null;
            Player.singleton.enabled = true;
            Player.singleton.GetComponent<Rigidbody>().velocity = (transform.position - lastFrame) / (Time.deltaTime);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null)
        {
            if (collision.collider.CompareTag("vine"))
            {
                vine = collision.collider.transform;
            }
        }
    }
}
