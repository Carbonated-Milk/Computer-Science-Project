using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMove : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.position -= speed * transform.right * Time.deltaTime;
    }
}
