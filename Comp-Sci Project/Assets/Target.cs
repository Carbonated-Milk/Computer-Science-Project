using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    public Transform target;

    [Header("Settings")]
    public float speed = 1f;
    void Start()
    {
        target = Player.singleton.transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddForce((target.position - transform.position).normalized, ForceMode.VelocityChange);
    }
}
