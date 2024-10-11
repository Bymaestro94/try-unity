using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float xInput;

    [Header("Collision Checks")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    private bool groundDetected;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CollisionChecks();

        xInput = Input.GetAxisRaw("Horizontal");

        Movement();
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }
    private void CollisionChecks() {
        groundDetected = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    private void Jump() {
        if (groundDetected){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    private void Movement() {
        rb.velocity = new Vector2(xInput *moveSpeed, rb.velocity.y);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
