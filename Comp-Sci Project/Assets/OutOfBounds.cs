using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private float outOfBoundsDist;

    private void Start()
    {
       outOfBoundsDist = Vector3.Distance(FindObjectOfType<WinZone>().transform.position, transform.position) * 2;
    }
    void Update()
    {
        if (transform.position.sqrMagnitude > outOfBoundsDist * outOfBoundsDist)
        {
            PlayerHealth.singleton.Die();
        }
    }
}
