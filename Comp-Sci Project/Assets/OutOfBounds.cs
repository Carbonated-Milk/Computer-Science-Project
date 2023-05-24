using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public float outOfBoundsDist = 200;
    void Update()
    {
        if (transform.position.sqrMagnitude > outOfBoundsDist * outOfBoundsDist)
        {
            PlayerHealth.singleton.Die();
        }
    }
}
