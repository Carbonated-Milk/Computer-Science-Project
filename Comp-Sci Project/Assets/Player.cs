using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Objects")]
    public Transform camObj;

    private Rigidbody rb;
    private CapsuleCollider col;

    public static Player singleton;

    private void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        MoveCamera();

        HandleGrounding();

        HandleJumping();

        Walking();
    }

    [Header("Camera")]
    public float sensitivity = 1f;

    private Vector2 mousePos;
    public void MoveCamera()
    {
        mousePos += InputP.inputs.mouseDelta * sensitivity;
        float camClamp = Mathf.Clamp(mousePos.y, -85, 80);
        mousePos = new Vector2(mousePos.x, camClamp);
        camObj.localRotation = Quaternion.Euler(-mousePos.y * Vector3.right);
        transform.rotation = Quaternion.Euler(mousePos.x * sensitivity * Vector3.up);
    }

    [Header("Walking")]

    public float maxSpeed = 5;
    public float timeToMaxSpeed = 1f;
    public float deceleration = 30;
    public void Walking()
    {
        float controltiplier = 1;
        if (!grounded) controltiplier/= 4;

        float _maxSpeed = InputP.inputs.run ? maxSpeed * 2 : maxSpeed;

        Vector3 targetRB = transform.rotation * (InputP.inputs.wasd.x * Vector3.right + InputP.inputs.wasd.y * Vector3.forward).normalized;
        targetRB *= _maxSpeed;

        if (InputP.inputs.wasd == Vector2.zero || Vector3.Dot(targetRB, rb.velocity - rb.velocity.y * Vector3.up) < 0)
        {
            rb.velocity = Vector3.MoveTowards(rb.velocity, targetRB + rb.velocity.y * Vector3.up, deceleration * Time.deltaTime);
            return;
        }

        Vector3 relVel = targetRB - (rb.velocity - rb.velocity.y * Vector3.up);

        rb.AddForce(relVel * controltiplier);
    }

    [Header("Ground Handling")]
    public float height = 1;
    public float radius = 1;
    public LayerMask mask;

    private bool grounded = true;

    public void HandleGrounding()
    {
        grounded = Physics.CheckSphere(transform.position - Vector3.up * (col.height / 2 + height), radius, mask);
    }

    [Header("Jumping")]
    public float jumpPower = 5;
    public float groundAccelerator = 1f;
    public void HandleJumping()
    {
        if (grounded && InputP.inputs.spaceDown)
        {
            Jump();
        }
        else if(InputP.inputs.space)
        {
            rb.AddForce(Vector2.down * groundAccelerator);
        }

        void Jump()
        {
            rb.velocity += Vector3.up * jumpPower;
        }
    }

    private void OnDrawGizmos()
    {
        if (col == null) col = GetComponent<CapsuleCollider>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - Vector3.up * (col.height / 2 + height), radius);
    }

    #region Bonus Functions

    public bool GetGrounded()
    {
        return grounded;
    }

    #endregion
}


