using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    void Update()
    {
        if (transform.position.sqrMagnitude > 200 * 200)
        {
            PlayerHealth.singleton.Die();
        }
    }
}
