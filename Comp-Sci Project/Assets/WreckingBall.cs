using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    
    public GameObject joint;
    public Transform endPoint;
    public float jointsPerDist = 1.5f;
    void Start()
    {
        float length = (endPoint.position - transform.position).magnitude;
        float joints = Mathf.CeilToInt(length / jointsPerDist);

        var lastObj = gameObject;
        for (int i = 1; i < joints; i++)
        {
            var obj = Instantiate(joint);
            obj.transform.position = Vector3.Lerp(transform.position, endPoint.position, i / joints);
            obj.transform.localScale = Vector3.one;
            lastObj.GetComponent<ConfigurableJoint>().connectedBody = obj.GetComponent<Rigidbody>();
            lastObj = obj;
        }
        lastObj.GetComponent<ConfigurableJoint>().connectedBody = endPoint.GetComponent<Rigidbody>();

        Destroy(joint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    public GameObject joint;
    public Transform endPoint;

    public float jointsPerDist = 1.5f;
    void Start()
    {
        float length = (endPoint.position - transform.position).magnitude;
        int joints = Mathf.CeilToInt(length / jointsPerDist);
        var lastObj = gameObject;

        for (int i = 1; i < joints; i++)
        {
            var obj = Instantiate(joint);
            obj.transform.position = Vector3.Lerp(transform.position, endPoint.position, i / joints);
            obj.transform.localScale = Vector3.one;
            lastObj.GetComponent<ConfigurableJoint>().connectedBody = obj.GetComponent<Rigidbody>();
            lastObj = obj;
        }
        lastObj.GetComponent<ConfigurableJoint>().connectedBody = endPoint.GetComponent<Rigidbody>();

        Destroy(joint);
    }

    void Update()
    {

    }
}
*/