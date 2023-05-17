using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swivel : MonoBehaviour
{
    public float degreesOfFreedom;
    public Vector2 angles;
    public float waitTime = 1f;
    public float speed;
    void Start()
    {
        StartCoroutine(Rotate());
        transform.parent.GetChild(3).parent = transform;
    }

    private float ogY;
    private Quaternion ogRot;
    public IEnumerator Rotate()
    {
        if (waitTime == 0) waitTime = 1;

        ogY = transform.rotation.eulerAngles.y;
        int i = 1;

       /* while (true)
        {
            Quaternion targetRot = ogRot * Quaternion.Euler(Vector3.up*)
        }*/

        while(true)
        {
            float targetY = transform.rotation.eulerAngles.y + i * degreesOfFreedom;
            float time = Time.time;
            while (Time.time - time < waitTime)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Vector3.up * targetY), speed * Time.deltaTime);
                yield return null;
            }
            i *= -1;
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos. draw angle thing
    }
}
