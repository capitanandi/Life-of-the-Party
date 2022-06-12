using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float speedMultiplier = 10.0f;
    [SerializeField] private float airMultiplier = 0.4f;
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float groundDrag = 6.0f;
    [SerializeField] private float airDrag = 2.0f;
    public bool isWalking;
    public bool canMove;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundMask;
    private float groundDistance = 0.4f;
    public bool playerGrounded;
    [SerializeField] private float playerHeight = 2.0f;

    private float horizontalInput;
    private float verticalInput;

    [SerializeField] Transform orientation;

    Vector3 direction;
    Vector3 slopeDirection;

    Rigidbody rb;

    RaycastHit slopeHit;

    [SerializeField] PhysicMaterial physicMaterial;

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canMove = true;
    }

    private void Update()
    {
        playerGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundDistance, groundMask);

        if(canMove)
        {
            MovementInput();
            ControlDrag();
        }

        if(playerGrounded && canMove && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        slopeDirection = Vector3.ProjectOnPlane(direction, slopeHit.normal);
    }

    void MovementInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        direction = orientation.forward * verticalInput + orientation.right * horizontalInput;
    }

    void ControlDrag()
    {
        if (playerGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (playerGrounded && !OnSlope())
        {
            rb.AddForce(direction.normalized * speed * speedMultiplier, ForceMode.Acceleration);
            isWalking = true;
        }
        else if(playerGrounded && OnSlope())
        {
            rb.AddForce(slopeDirection.normalized * speed * speedMultiplier, ForceMode.Acceleration);
            physicMaterial.staticFriction = 0.5f;
            isWalking = true;
        }
        else
        {
            rb.AddForce(direction.normalized * speed * speedMultiplier * airMultiplier, ForceMode.Acceleration);
            isWalking = false;
        }

        if(horizontalInput == 0 && verticalInput == 0)
        {
            isWalking = false;
        }
    }
}
