using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleRunnerPhysics : MonoBehaviour
{
    public float moveSpeed { get; set; }
    private Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    [Header("Jumping")]
    [SerializeField] float jumpForce = 12f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] float _gravityScale = 3;
    [SerializeField] float _maxFallHeight = 10f;
    // [SerializeField] float _snapThreshold = 0.25f;

    bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    // bool IsGrounded() => rb.linearVelocityY <= 0 && Physics2D.CircleCast(groundCheck.position, groundCheckRadius, Vector2.down, isGrounded ? _snapThreshold : Mathf.Max(0, Time.deltaTime * -rb.linearVelocityY), groundLayer);

    public int maxAirJumps { get; set; }
    private int airJumpsRemaining;

    [Header("Wall Jump")]
    [SerializeField] float wallSlideSpeed = 1f;
    [SerializeField] float wallJumpForce = 15f;
    [SerializeField] Transform leftWallCheck, rightWallCheck;
    [SerializeField] float wallCheckDistance = 0.5f;
    [SerializeField] LayerMask platformLayer;
    public bool isGrounded { get; private set; }

    bool IsOnWall()
    {
        return Physics2D.Raycast(leftWallCheck.position, Vector2.left, wallCheckDistance, groundLayer) ||
               Physics2D.Raycast(rightWallCheck.position, Vector2.right, wallCheckDistance, groundLayer);
    }

    void HandleWallSlide()
    {
        if (IsOnWall() && !IsGrounded() && rb.linearVelocity.y < 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }
    }

    void HandleWallJump()
    {
        if (IsOnWall() && Input.GetButtonDown("Jump"))
        {
            // float direction = IsOnLeftWall() ? 1 : -1;
            rb.linearVelocity = new Vector2(wallJumpForce * 1, jumpForce);
        }
    }

    [SerializeField] float jumpInputBuffer = 0.1f;
    float jumpInputBufferTimer = 0;

    void Update()
    {
        if (isGrounded)
        {
            if (rb.linearVelocityY < -groundCheckRadius) rb.linearVelocityY = -groundCheckRadius;
            airJumpsRemaining = maxAirJumps;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpInputBufferTimer = jumpInputBuffer;
        }

        if (jumpInputBufferTimer > 0)
        {
            jumpInputBufferTimer -= Time.deltaTime;
            if (isGrounded || airJumpsRemaining > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                if (!isGrounded) airJumpsRemaining--;
                jumpInputBufferTimer = 0;
            }
        }

        // DropdownPlatform(Input.GetKey(KeyCode.DownArrow));
        DropdownPlatform(Input.GetAxisRaw("Vertical") < 0);

        // HandleWallSlide();
        // HandleWallJump();

        if (rb.linearVelocityY > -_maxFallHeight)
        {
            rb.linearVelocityY += _gravityScale * Time.deltaTime * Physics2D.gravity.y;
        }
    }

    // [Header("Slope Handling")]
    // [SerializeField] float slopeCheckDistance = 0.5f;
    // [SerializeField] float maxSlopeAngle = 45f;

    void FixedUpdate()
    {
        isGrounded = IsGrounded();
        Vector2 moveDirection = Vector2.right;
        // if (IsGrounded())
        // {
        //     RaycastHit2D slopeHit = Physics2D.BoxCast(groundCheck.position, new Vector2(groundCheckRadius, 0.1f), 0, Vector2.down, 1, groundLayer);
        //     if (slopeHit.collider != null)
        //     {
        //         moveDirection = Vector2.Perpendicular(-slopeHit.normal);
        //     }
        // }
        rb.linearVelocity = (moveDirection * moveSpeed) + (rb.linearVelocity.y * Vector2.up);
    }

    void DropdownPlatform(bool ignorePlatforms)
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("OneWayPlatform"), ignorePlatforms);
    }

    // void OnDrawGizmos()
    // {
    //     if (jumpInputBufferTimer == 0 && rb.linearVelocityY > 0)
    //     {
    //         Gizmos.color = Color.green;
    //         Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    //     }
    //     else if (isGrounded)
    //     {
    //         Gizmos.color = Color.yellow;
    //         Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    //     }
    //     else
    //     {
    //         Gizmos.color = Color.gray;
    //         Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    //     }
    // }
}
