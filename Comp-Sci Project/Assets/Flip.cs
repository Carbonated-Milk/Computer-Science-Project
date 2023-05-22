using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    public float waitTime = 1f;
    public float amount = 90;
    public float timeToRot = 2f;

    public float timeOffset = 0;
    void Start()
    {
        StartCoroutine(FlipTransform());
    }

    private IEnumerator FlipTransform()
    {
        yield return new WaitForSeconds(timeOffset);
        while (true)
        {
            float time = Time.time;
            while (Time.time - time < timeToRot)
            {
                transform.rotation *= Quaternion.Euler(amount / timeToRot * transform.forward * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
