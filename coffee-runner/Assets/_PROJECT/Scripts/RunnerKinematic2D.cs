using UnityEngine;

// public class RunnerKinematic2D : KinematicSupport2D
// {
//     [SerializeField] float _speed = 4f;
//     [SerializeField] float _gravityScale = 1f;
//     [SerializeField] float _groundCheckDistance = 0.1f;
//     [SerializeField] float _footDistanceFromCenter = 1f;
//     [SerializeField] float _snapToGroundDistance = 0.2f;
//     public bool isGrounded = false;
//     private Vector2 _lastMovementProjected;
//     private Vector2 _currentVelocity;

//     protected override Vector2 ComputeVelocity()
//     {
//         ApplyGravity();

//         return (Time.deltaTime * _currentVelocity);
//     }

//     void ApplyGravity()
//     {
//         if (!isGrounded)
//         {
//             _currentVelocity.y += Physics2D.gravity.y * _gravityScale * Time.deltaTime;
//         }
//         else
//         {
//             _currentVelocity.y = 0;
//         }

//         _currentVelocity.x = _speed;
//     }

//     protected override Vector2 ComputeProjection(Vector2 vector, Vector2 planeNormal)
//     {
//         Vector2 projection = vector - Vector2.Dot(vector, planeNormal) * planeNormal;
//         if (planeNormal.y > 0.1f) // Consideramos suelo si la normal es mayor a ~70°
//         {
//             isGrounded = true;
//         }
//         _lastMovementProjected = (projection.magnitude + (vector - projection).magnitude) * projection.normalized;
//         return _lastMovementProjected;
//     }

//     private new void FixedUpdate()
//     {
//         base.FixedUpdate();
//         CheckGroundStatus();
//     }

//     void CheckGroundStatus()
//     {
//         RaycastHit2D[] hits = new RaycastHit2D[1];
//         float distance = _skinWidth + _groundCheckDistance;
//         int hitCount = _body.Cast(
//             Vector2.down, 
//             _filter, 
//             hits, 
//             distance
//         );
//         bool expectAirborne = isGrounded;
//         isGrounded = hitCount > 0 && Mathf.Abs(hits[0].normal.y) > 0.1f;

//         // if (expectAirborne && !isGrounded)
//         // {
//         //     _currentVelocity.y += _lastMovementProjected.y / Time.deltaTime;
//         // }
//     }
// }

public class RunnerKinematic2D : KinematicSupport2D
{
    [Header("Properties")]
    [SerializeField] float _moveSpeed = 4f;
    [SerializeField] float _jumpSpeed = 6f;
    [SerializeField] float _gravityScale = 1f;
    [SerializeField] float _groundCheckDistance = 0.25f;
    // [SerializeField] float _footDistanceFromCenter = 1f;
    // [SerializeField] float _snapToGroundDistance = 0.2f;
    const float _slopeSpeedMultiplier = 1.666667f; // Ajuste para compensar pendientes
    public bool isGrounded = false;
    private Vector2 _currentVelocity;
    [Header("Ground Anchoring")]
    [SerializeField] float _maxSlopeAngle = 45f; // Ángulo máximo de pendiente pisable
    // [SerializeField] float _groundSnapForce = 5f; // Fuerza de ajuste al suelo
    private Vector2 _groundNormal = Vector2.up;

    protected override Vector2 ComputeVelocity()
    {
        ApplyGravity();
        ApplyGroundSnapping();

        return Time.deltaTime * _currentVelocity;
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            _currentVelocity.y += Physics2D.gravity.y * _gravityScale * Time.deltaTime;
        }
        else
        {
            _currentVelocity.y = 0;
            if (Input.GetButton("Jump"))
            {
                _currentVelocity.y = _jumpSpeed;
                isGrounded = false;
            }
        }

        _currentVelocity.x = Input.GetAxisRaw("Horizontal") * _moveSpeed;
    }

    void ApplyGroundSnapping()
    {
        if (isGrounded)
        {
            // Calcular dirección de la pendiente y ajustar velocidad
            Vector2 slopeDirection = new Vector2(_groundNormal.y, -_groundNormal.x).normalized;
            // float slopeAngleFactor = 1f / Mathf.Abs(slopeDirection.x);

            _currentVelocity.x = Input.GetAxisRaw("Horizontal") * _moveSpeed * slopeDirection.x * _slopeSpeedMultiplier;
            _currentVelocity.y = Input.GetAxisRaw("Horizontal") * _moveSpeed * slopeDirection.y * _slopeSpeedMultiplier;

            // Ajuste fino para mantener velocidad horizontal constante
            float horizontalCompensation = Mathf.Clamp(1 + (1 - Mathf.Abs(slopeDirection.x)), 1f, 1.5f);
            _currentVelocity.x *= horizontalCompensation;
        }
    }

    protected override Vector2 ComputeProjection(Vector2 vector, Vector2 planeNormal)
    {
        float slopeAngle = Vector2.Angle(planeNormal, Vector2.up);
        Vector2 projection = vector - Vector2.Dot(vector, planeNormal) * planeNormal;
        return (projection.magnitude + (vector - projection).magnitude) * projection.normalized;

        // return base.ComputeProjection(vector, planeNormal);
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        CheckGroundStatus();
        SnapToGround();
    }

    void SnapToGround()
    {
        if (!isGrounded) return;

        RaycastHit2D[] hits = new RaycastHit2D[1];
        float checkDistance = _skinWidth + 0.5f; // Rango de ajuste

        if (_body.Cast(Vector2.down, _filter, hits, checkDistance) > 0)
        {
            // Ajuste fino de posición
            float snapDistance = hits[0].distance - _skinWidth;
            Vector2 correction = Vector2.down * snapDistance;
            _body.position += correction;
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit2D[] hits = new RaycastHit2D[1];
        float distance = _skinWidth + _groundCheckDistance - (Time.deltaTime * _currentVelocity.y);
        // float distance = (_skinWidth + _groundCheckDistance) * Mathf.Clamp01(1 - Mathf.Sign(_currentVelocity.y));

        int hitCount = _body.Cast(
            Vector2.down,
            _filter,
            hits,
            distance
        );

        isGrounded = hitCount > 0 &&
                    Vector2.Angle(hits[0].normal, Vector2.up) <= _maxSlopeAngle;
    }
}