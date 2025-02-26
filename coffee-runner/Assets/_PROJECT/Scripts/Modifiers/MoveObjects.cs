using System.Collections;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    [SerializeField] Transform[] _target;
    [SerializeField, Min(0)] float _duration = 0.5f;
    [SerializeField] AnimationCurve _easing = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] Vector3 _translation = Vector3.zero;
    

    public void Execute()
    {
        StartCoroutine(ExecuteInternal());
    }

    IEnumerator ExecuteInternal()
    {
        Vector3[] initialValues = GetInitialValues();
        float t = 0;
        if (_duration > 0)
        {
            float deltaDuration = 1 / Mathf.Max(_duration, Mathf.Epsilon);
            while (t < 1)
            {
                ApplyChange(initialValues, t);
                t += deltaDuration * Time.deltaTime;
                yield return null;
            }
        }
        ApplyChange(initialValues, 1);
    }

    Vector3[] GetInitialValues()
    {
        int count = _target.Length;
        Vector3[] initialPositions = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            initialPositions[i] = _target[i].position;
        }
        return initialPositions;
    }

    void ApplyChange(Vector3[] initialValues, float progress)
    {
        for (int i = 0; i < _target.Length; i++)
        {
            _target[i].position = Vector3.Lerp(initialValues[i], initialValues[i] + _translation, _easing.Evaluate(progress));
        }
    }
}
