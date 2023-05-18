using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    [Header("Assign Stuff")]
    public Player player;
    public Rigidbody rb;
    public Transform rightArm, leftArm;

    [Header("Variables")]
    public float rangeOfMotion = 20;
    public float cycleSpeed = 20;
    public float cycleSpeedMax = 20;

    private float cycle;
    void Update()
    {
        rightArm.localRotation = Quaternion.Euler(new Vector3(20 * Mathf.Sin(cycle), rightArm.localRotation.eulerAngles.y, 0));
        leftArm.localRotation = Quaternion.Euler(new Vector3(20 * -Mathf.Sin(cycle), leftArm.localRotation.eulerAngles.y, 0));
        cycle += Time.deltaTime * Mathf.Min(rb.velocity.sqrMagnitude / 10, cycleSpeedMax);
    }
}
