using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Inputs inputs;

    [Header("Objects")]
    public Transform camObj;

    private Rigidbody rb;
    private CapsuleCollider col;

    void Start()
    {
        inputs = new Inputs();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        GatherInputs();

        MoveCamera();

        HandleGrounding();

        HandleJumping();

        Walking();
    }

    [Header("Camera")]
    public float sensitivity = 1f;
    public void MoveCamera()
    {
        float camClamp = Mathf.Clamp(inputs.mousePos.x * sensitivity, -85, 80);
        camObj.localRotation = Quaternion.Euler(-camClamp * Vector3.right);
        transform.rotation = Quaternion.Euler(inputs.mousePos.y * sensitivity * Vector3.up);
    }

    [Header("Walking")]

    public float maxSpeed = 5;
    public float timeToMaxSpeed = 1f;
    private float _acceleration = 0;
    public float deceleration = 30;
    public void Walking()
    {
        if (!grounded) return;

        Vector3 targetRB = transform.rotation * (inputs.wasd.x * Vector3.right + inputs.wasd.y * Vector3.forward);

        if (inputs.wasd == Vector2.zero || Vector3.Dot(targetRB, rb.velocity - rb.velocity.y * Vector3.up) < 0)
        {
            _acceleration = 0;
            rb.velocity = Vector3.MoveTowards(rb.velocity, targetRB + rb.velocity.y * Vector3.up, deceleration * Time.deltaTime);
            return;
        }

        _acceleration += maxSpeed / timeToMaxSpeed * Time.deltaTime;
        _acceleration = Mathf.Clamp(_acceleration, rb.velocity.magnitude, maxSpeed);

        targetRB = targetRB.normalized * _acceleration + rb.velocity.y * Vector3.up;

        rb.velocity = targetRB;
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
    public void HandleJumping()
    {
        if (grounded && inputs.spaceDown)
        {
            Jump();
        }

        void Jump()
        {
            rb.velocity += Vector3.up * jumpPower;
        }
    }

    public void GatherInputs()
    {
        inputs.space = Input.GetKey(KeyCode.Space);
        inputs.spaceDown = Input.GetKeyDown(KeyCode.Space);

        inputs.mousePos = Vector2.right * Input.mousePosition.y + Vector2.up * Input.mousePosition.x;
        inputs.wasd = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        inputs.run = Input.GetKey(KeyCode.LeftShift);
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

public struct Inputs
{
    public Vector2 mousePos;
    public Vector2 wasd;
    public bool space;
    public bool spaceDown;
    public bool run;
}
