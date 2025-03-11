using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleSinglePhysics : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private CircleCollider2D _circleCollider;
    [SerializeField] private BoxCollider2D _boxCollider;
    [Header("Properties")]
    [SerializeField] private float _skinWidth = 0.03f;
    [SerializeField] private float _speed = 4;
    [SerializeField] private float _jumpHeight = 4;
    [SerializeField] private float _gravityScale = 1;
    [SerializeField] private float _maxFallHeight = 10;
    [SerializeField, Range(0, 180)] private float _maxSlopeAngle = 45;
    [SerializeField] private float _snapThreshold = 0.5f;
    [SerializeField] private LayerMask _collideWith = 1;
    [SerializeField] private bool _boxInsteadOfCircleCasting;
    private bool _isOnSteepSlope;
    private RaycastHit2D[] groundHit = new RaycastHit2D[1];
    // public bool checkGround => heightSpeed <= 0 && Physics2D.OverlapCircleNonAlloc(_body.position + (0.2f * Vector2.down), _mainCollider.radius, new Collider2D[1], _collideWith) > 0;
    // public bool checkGround => heightSpeed <= 0 && Physics2D.CircleCastNonAlloc(_body.position + (0.2f * Vector2.down), _mainCollider.radius, new Collider2D[1], _collideWith) > 0;
    public bool checkGround => heightSpeed <= 0 && 
    ( _boxInsteadOfCircleCasting ? Physics2D.BoxCastNonAlloc(_body.position + _boxCollider.offset, _boxCollider.size, 0, Vector2.down, groundHit, isGrounded ? _snapThreshold : Mathf.Max(0, Time.deltaTime * -heightSpeed) + _skinWidth, _collideWith) > 0
    : Physics2D.CircleCastNonAlloc(_body.position + _circleCollider.offset, _circleCollider.radius, Vector2.down, groundHit, isGrounded ? _snapThreshold : Mathf.Max(0, Time.deltaTime * -heightSpeed) + _skinWidth, _collideWith) > 0
    );
    
    public Vector2 currentHorizontalVelocity => Time.deltaTime * _body.linearVelocity.x * Vector2.right;

    public float heightSpeed { get; private set; }
    public bool isGrounded { get; private set; }

    void Update()
    {
        if (isGrounded)
        {
            if (heightSpeed < 0) heightSpeed = 0;
            if (Input.GetButtonDown("Jump"))
            {
                heightSpeed = _jumpHeight;
            }
        }
    }

    void FixedUpdate()
    {
        _isOnSteepSlope = false;
        // if (groundHit[0].collider != null)
        // {
        //     isGrounded = Physics2D.OverlapCircleNonAlloc(_body.position, (0.5f * _mainCollider.radius) - _skinWidth, new Collider2D[1], _collideWith) == 0;
        // }
        // else isGrounded = false;
        if (checkGround)
        {
            if (groundHit[0].normal.y > (1 - (_maxSlopeAngle * 0.0055555555555556f)))
            {
                isGrounded = groundHit[0].distance > 0;
            }
            else
            {
                _isOnSteepSlope = true;
                isGrounded = false;
            }
        }
        else isGrounded = false;

        Vector2 horizontalVelocity = CalculateHorizontalVelocity();
        Vector2 verticalVelocity = CalculateVerticalVelocity();


        _body.linearVelocity = horizontalVelocity + verticalVelocity;
    }

    private Vector2 CalculateVerticalVelocity()
    {
        if (!isGrounded)
        {
            heightSpeed += _gravityScale * Time.deltaTime * Physics2D.gravity.y;
            if (heightSpeed < -_maxFallHeight) heightSpeed = -_maxFallHeight;
        }

        return heightSpeed * Vector2.up;
    }

    Vector2 CalculateHorizontalVelocity()
    {
        Vector2 slopeTranslate = Vector3.zero;
        float moveDistance = Input.GetAxisRaw("Horizontal") * _speed;
        Vector2 moveDirection = Vector2.right;
        Vector2 previousDirection = moveDirection;

        RaycastHit2D slopeHit = _boxInsteadOfCircleCasting ?
        Physics2D.BoxCast(_body.position + _boxCollider.offset, _boxCollider.size, 0, Vector2.down, _snapThreshold, _collideWith)
        : Physics2D.CircleCast(_body.position + _circleCollider.offset, _circleCollider.radius, Vector2.down, _snapThreshold, _collideWith);

        // RaycastHit2D slopeHit = Physics2D.BoxCast(_body.position, (_radius - _skinWidth) * Vector2.one, 0, Vector2.down, 1, _collideWith);

        if (slopeHit && isGrounded)
        {
            heightSpeed = 0;
            var slopeDetectedSpot = new RaycastHit2D[1];
            Physics2D.RaycastNonAlloc(slopeHit.point + 0.1f * Vector2.up, Vector2.down, slopeDetectedSpot, 0.2f, _collideWith);
            if (slopeDetectedSpot[0])
            {
                slopeTranslate = (slopeHit.distance - _skinWidth) * Vector2.down;
                moveDirection = (Vector2.Perpendicular(-slopeDetectedSpot[0].normal).y * Vector2.up) + (moveDirection.x * Vector2.right);
                Debug.DrawRay(slopeHit.point, 0.5f * slopeDetectedSpot[0].normal, Color.blue);
            }
            // slopeTranslate = (slopeHit.distance - _skinWidth) * Vector2.down;
            // moveDirection = (Vector2.Perpendicular(-slopeHit.normal).y * Vector2.up) + (moveDirection.x * Vector2.right);
            // Debug.DrawRay(slopeHit.point, 0.5f * slopeHit.normal, Color.blue);
        }

        RaycastHit2D moveHit = Physics2D.CircleCast(_body.position + _circleCollider.offset, _circleCollider.radius - _skinWidth, Vector2.right, (moveDistance * Time.fixedDeltaTime) + _skinWidth , _collideWith);

        // if (moveHit.collider != null && Vector2.Dot(moveHit.normal, Vector2.up) > 0.5f)
        // {
        //     moveDistance = moveHit.distance - _skinWidth;
        //     isGrounded = false;
        //  return (slopeTranslate / Time.fixedDeltaTime) + ((moveHit.distance - _skinWidth) * moveDirection);
        // }
        if (_isOnSteepSlope)
        {
            moveDistance = 0;
        }
        else if (moveHit.collider != null && moveHit.normal.y <= (1 - (_maxSlopeAngle * 0.0055555555555556f)))
        {
            moveDistance = moveHit.distance - _skinWidth;
            moveDirection = previousDirection;
        }
        

        Vector2 horizontalMovement = moveDistance * moveDirection;
        return (slopeTranslate / Time.fixedDeltaTime) + horizontalMovement;
    }

    void OnGUI()
    {
        GUI.TextField(new Rect(8, 8, 320, 64), isGrounded.ToString(), new GUIStyle().fontSize = 48);
    }
}
