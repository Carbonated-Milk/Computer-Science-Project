using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    private float waitTime = 1f;
    private float amount = 90;
    private float timeToRot = 2f;
    void Start()
    {
        StartCoroutine(FlipTransform());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator FlipTransform()
    {
        while (true)
        {
            float time = Time.time;
            while (Time.time - time < timeToRot)
            {
                transform.rotation *= Quaternion.Euler(amount / timeToRot * transform.right * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
