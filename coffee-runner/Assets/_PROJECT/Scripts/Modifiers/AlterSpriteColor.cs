using System.Collections;
using UnityEngine;

public class AlterSpriteColor : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] _target;
    [SerializeField, Min(0)] float _duration = 0.5f;
    [SerializeField] AnimationCurve _easing = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] Color _color = new Color(1,1,1,1);
    

    public void Execute()
    {
        StartCoroutine(ExecuteInternal());
    }

    IEnumerator ExecuteInternal()
    {
        Color[] initialValues = GetInitialValues();
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

    Color[] GetInitialValues()
    {
        int count = _target.Length;
        Color[] initialColors = new Color[count];
        for (int i = 0; i < count; i++)
        {
            initialColors[i] = _target[i].color;
        }
        return initialColors;
    }

    void ApplyChange(Color[] initialValues, float progress)
    {
        for (int i = 0; i < _target.Length; i++)
        {
            _target[i].color = Color.Lerp(initialValues[i], _color, _easing.Evaluate(progress));
        }
    }
}
