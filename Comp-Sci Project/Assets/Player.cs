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

    private FakeChild fakeChild;
    private void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        fakeChild = GetComponent<FakeChild>();
    }

    void Update()
    {
        MoveCamera();

        HandleGrounding();

        HandleJumping();

        Walking();
    }

    [Header("Camera")]
    public static float sensitivity = 1f;

    public void MoveCamera()
    {
        Vector2 mDelta = InputP.inputs.mouseDelta * sensitivity;
        camObj.localRotation *= Quaternion.Euler(-mDelta.y * Vector3.right);
        float xClamp = camObj.localRotation.eulerAngles.x;

        Vector2 bounds = new Vector2(85, 280);
        if (xClamp > bounds.x && xClamp < 180) xClamp = bounds.x; //tedious garbage I have to do
        else if (xClamp < bounds.y && xClamp > 180) xClamp = bounds.y;

        camObj.localRotation = Quaternion.Euler(xClamp * Vector3.right);
        transform.localRotation *= Quaternion.Euler(mDelta.x * Vector3.up);
    }

    [Header("Walking")]

    public float maxSpeed = 5;
    public float deceleration = 30;
    public float speedCorrector = 5; //should be removeable later
    public void Walking()
    {
        rb.angularVelocity = Vector3.zero;

        float controltiplier = 1;
        if (!grounded) controltiplier /= 4;

        float _maxSpeed = InputP.inputs.run ? maxSpeed * 2 : maxSpeed;

        Vector3 targetRB = transform.rotation * (InputP.inputs.wasd.x * Vector3.right + InputP.inputs.wasd.y * Vector3.forward).normalized;
        targetRB *= _maxSpeed;

        Vector3 relVel = targetRB - (rb.velocity - rb.velocity.y * Vector3.up);

        rb.AddForce(relVel * controltiplier * speedCorrector, ForceMode.Acceleration);
    }

    [Header("Ground Handling")]
    public float height = 1;
    public float radius = 1;
    public LayerMask mask;

    private bool grounded = true;
    private float lastTimeGrounded;

    public void HandleGrounding()
    {
        Collider[] groundParent = Physics.OverlapSphere(transform.position - Vector3.up * (col.height / 2 + height), radius, mask);


        if (groundParent.Length != 0)
        {
            grounded = true;
            lastTimeGrounded = Time.time;
            if(fakeChild.enabled == false)
            {
                fakeChild.enabled = true;
                fakeChild.SetFakeParent(groundParent[0].transform);
            }
        }
        else
        {
            grounded = false;
            fakeChild.enabled = false;
        }
    }

    [Header("Jumping")]
    public float jumpPower = 5;
    public float groundAccelerator = 1f;
    public float cayoteTime;

    private float lastTimeJumped;
    public void HandleJumping()
    {
        if (InputP.inputs.spaceDown)
        {
            if(grounded || Time.time - lastTimeGrounded < cayoteTime && Time.time - lastTimeJumped > cayoteTime) Jump();
        }
        else if(InputP.inputs.space)
        {
            rb.AddForce(Vector2.down * groundAccelerator);
        }

        void Jump()
        {
            rb.velocity += Vector3.up * jumpPower;
            lastTimeJumped = Time.time;
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


