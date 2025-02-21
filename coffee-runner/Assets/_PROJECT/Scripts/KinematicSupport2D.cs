using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KinematicSupport2D : MonoBehaviour
{
    [Header("Collision settings")]
    [SerializeField] protected float _skinWidth = 0.08f;
    [SerializeField] protected LayerMask _collideWith = 1;
    [SerializeField] protected int _maxCollisionIterations = 3;
    
    protected Rigidbody2D _body;
    protected ContactFilter2D _filter;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _filter = new ContactFilter2D();
        _filter.layerMask = _collideWith;
        _filter.useLayerMask = true;
    }

    protected void FixedUpdate()
    {
        Vector2 movement = ComputeVelocity();
        PerformMovement(ref movement);
        _body.MovePosition(_body.position + movement);
    }

    void PerformMovement(ref Vector2 movement)
    {
        int iterations = 0;
        while (iterations < _maxCollisionIterations)
        {
            RaycastHit2D[] hit = new RaycastHit2D[1];
            float distance = movement.magnitude + _skinWidth;
            
            if (_body.Cast(movement.normalized, _filter, hit, distance) > 0)
            {
                Vector2 snapMovement = movement.normalized * (hit[0].distance - _skinWidth);
                movement -= snapMovement;
                _body.position += snapMovement;
                movement = ComputeProjection(movement, hit[0].normal);
                iterations++;
            }
            else
            {
                _body.position += movement;
                break;
            }
        }
    }

    protected virtual Vector2 ComputeVelocity() { return Vector2.zero; }

    protected virtual Vector2 ComputeProjection(Vector2 vector, Vector2 planeNormal)
    {
        return vector - Vector2.Dot(vector, planeNormal) * planeNormal;
    }
}
