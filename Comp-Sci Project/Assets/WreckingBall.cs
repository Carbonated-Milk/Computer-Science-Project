using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    public float length;
    public GameObject joint;
    public Transform endPoint;
    public float joints;
    void Start()
    {
        var lastObj = gameObject;
        for (int i = 1; i < joints + 1; i++)
        {
            var obj = Instantiate(joint);
            obj.transform.position = Vector3.Lerp(transform.position, endPoint.position, i/(joints + 1));
            obj.transform.localScale = Vector3.one;
            lastObj.GetComponent<ConfigurableJoint>().connectedBody = obj.GetComponent<Rigidbody>();
            lastObj = obj;
        }
        Destroy(lastObj.GetComponent<ConfigurableJoint>());
        lastObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
