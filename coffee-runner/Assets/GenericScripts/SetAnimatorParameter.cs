using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorParameter : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _parameter;
    
    public void _SetBool(bool value) => _animator.SetBool(_parameter, value);
    public void _SetBoolTrue(string parameter) => _animator.SetBool(parameter, true);
    public void _SetBoolFalse(string parameter) => _animator.SetBool(parameter, false);
    public void _SetFloat(float value) => _animator.SetFloat(_parameter, value);
    public void _SetInt(int value) => _animator.SetInteger(_parameter, value);
    public void _SetTrigger() => _animator.SetTrigger(_parameter);
    public void _SetTrigger(string trigger) => _animator.SetTrigger(trigger);
}
