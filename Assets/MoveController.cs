using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float xInput;


    [Header("Collision Checks")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    private bool groundDetected;
    private bool facingRight = true; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        AnimationController();
        CollisionChecks();

        xInput = Input.GetAxisRaw("Horizontal");

        Movement();
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
        FlipController();
    }
    private void AnimationController() {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("IsMoving", isMoving);

        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGorunded", groundDetected);
    }
    private void CollisionChecks() {
        groundDetected = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    private void Jump() {
        if (groundDetected){
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpForce);
        }
    }
    private void Movement() {
        rb.velocity = new UnityEngine.Vector2(xInput *moveSpeed, rb.velocity.y);
    }

    private void FlipController() {
        UnityEngine.Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < transform.position.x && facingRight || mousePos.x > transform.position.x && !facingRight) {
            Flip();
        }
    }
    private void Flip() {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
