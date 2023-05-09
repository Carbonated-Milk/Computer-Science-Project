using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Inputs inputs;

    [Header("Objects")]
    public Transform camObj;

    private Rigidbody rb;

    void Start()
    {
        inputs = new Inputs();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GatherInputs();

        MoveCamera();

        HandleGrounding();

        HandleJumping();

        MovePlayer();
    }

    [Header("Camera")]
    public float sensitivity = 1f;
    public void MoveCamera()
    {
        float camClamp = Mathf.Clamp(inputs.mousePos.x * sensitivity, -85, 80);
        camObj.localRotation = Quaternion.Euler(-camClamp * Vector3.right);
        transform.rotation = Quaternion.Euler(inputs.mousePos.y * sensitivity * Vector3.up);
    }

    [Header("Movement")]

    public float maxSpeed = 5;
    public float _acceleration = 3;
    public void MovePlayer()
    {
        float acceleration = _acceleration;

        Vector3 targetRB = transform.rotation * (inputs.wasd.x * Vector3.right + inputs.wasd.y * Vector3.forward);

        if (inputs.wasd == Vector2.zero || Vector3.Dot(targetRB, rb.velocity - rb.velocity.y * Vector3.up) < 0)
        {
            acceleration *= 2;
        }

        
        targetRB = targetRB.normalized * maxSpeed + rb.velocity.y * Vector3.up;

        rb.velocity = Vector3.MoveTowards(rb.velocity, targetRB, acceleration * Time.deltaTime);
    }

    [Header("Ground Handling")]
    private bool grounded = true;
    public void HandleGrounding()
    {

    }


    public float jumpPower = 5;
    public void HandleJumping()
    {
        if(grounded && inputs.spaceDown)
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
    }
}

public struct Inputs
{
    public Vector2 mousePos;
    public Vector2 wasd;
    public bool space;
    public bool spaceDown;
}
