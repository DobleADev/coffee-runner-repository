using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TranslateThroughWaypoints : MonoBehaviour
{
    public enum MovementType { MoveTowards, Lerp }
    [SerializeField] private MovementType movementType = MovementType.MoveTowards;
    [SerializeField] private float speed = 5f;
    [SerializeField] private AnimationCurve easing = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private float smoothTime = 0.5f;
    [SerializeField] private float waitTime = 0.5f;
    [SerializeField] private float startDelay = 0f;
    [SerializeField] private Vector3 positionOffset = Vector3.zero;
    [SerializeField] private bool playOnAwake = true;
    [SerializeField] private bool loop = false;
    [SerializeField] private List<Transform> points = new List<Transform>();
    public UnityEvent OnCycleStart;
    public UnityEvent OnPointArrived;
    public UnityEvent OnCycleEnd;

    private Coroutine movementCoroutine;

    void Start()
    {
        if (playOnAwake)
        {
            Play();
        }
    }

    public void Play()
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }
        movementCoroutine = StartCoroutine(MovementCycle());
    }

    public void Stop()
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }
    }

    private IEnumerator MovementCycle()
    {
        yield return new WaitForSeconds(startDelay);

        if (points == null || points.Count == 0)
        {
            Debug.LogError("No points assigned to TranslateThroughWaypoints!", this);
            yield break;
        }

        OnCycleStart.Invoke();

        int currentIndex = 0;

        // teleport to first point
        transform.position = points[currentIndex].position + positionOffset;

        while (true)
        {
            Transform currentPoint = points[currentIndex];
            if (currentPoint == null)
            {
                Debug.LogError("Missing transform reference in points list!", this);
                yield break;
            }

            Vector3 targetPosition = currentPoint.position + positionOffset;

            if (movementType == MovementType.MoveTowards)
            {
                yield return StartCoroutine(MoveTowardsTarget(currentPoint));
            }
            else
            {
                yield return StartCoroutine(LerpToTarget(targetPosition));
            }

            OnPointArrived.Invoke();
            yield return new WaitForSeconds(waitTime);

            currentIndex++;
            if (currentIndex >= points.Count)
            {
                if (loop)
                {
                    currentIndex = 0;
                }
                else
                {
                    OnCycleEnd.Invoke();
                    yield break;
                }
            }
        }
    }

    private IEnumerator MoveTowardsTarget(Transform target)
    {
        Vector3 currentVelocity = Vector3.zero;
        Vector3 targetPosition = target.position + positionOffset;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref currentVelocity,
                smoothTime,
                Mathf.Abs(speed),
                Mathf.Min(Time.deltaTime, Time.maximumDeltaTime)
            );

            yield return null;
        }

        transform.position = targetPosition;
    }

    private IEnumerator LerpToTarget(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;

        if (speed < 0f)
        {
            Vector3 temp = startPosition;
            startPosition = targetPosition;
            targetPosition = temp;
        }

        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / Mathf.Abs(speed);

        float elapsed = 0f;

        while (elapsed < duration)
        {
            duration = distance / Mathf.Abs(speed);
            elapsed += Mathf.Min(Time.deltaTime, Time.maximumDeltaTime);
            float t = Mathf.Clamp01(elapsed / duration);
            float easedT = easing.Evaluate(t);
            transform.position = Vector3.Lerp(startPosition, targetPosition, easedT);
            yield return null;
        }

        transform.position = targetPosition;
    }
}

