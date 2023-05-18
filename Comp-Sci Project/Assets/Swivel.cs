using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Swivel : MonoBehaviour
{
    public Vector2 angles;
    public float waitTime = 1f;
    public float timeToRot = 2f;
    void Start()
    {
        transform.GetChild(0).SetParent(transform, true);
        StartCoroutine(Rotate());
    }
    public IEnumerator Rotate()
    {
        if (timeToRot == 0) timeToRot = 1;

        int i = 1;

        transform.rotation = Quaternion.Euler(Vector3.up * angles.x);
        float totalAngles = angles.y - angles.x;

        while (true)
        {
            float time = Time.time;
            while (Time.time - time < timeToRot)
            {
                transform.rotation *= Quaternion.Euler(totalAngles * i / timeToRot * Vector3.up * Time.deltaTime);
                yield return null;
            }

            i *= -1;
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void OnDrawGizmos()
    {
        //maybe later
        //Handles.CircleHandleCap(1, transform.position + 2 * Vector3.up, Quaternion.identity, 3, EventType.DragUpdated);
    }
}
