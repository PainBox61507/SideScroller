using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBaseController : MonoBehaviour
{

    protected Animator animator;

    public AnimationBaseController(Animator _animator)
    {
        animator = _animator;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    protected void SetBool(string _boolName, bool _value)
    {
        animator.SetBool(_boolName, _value);
    }

    protected void SetFloat(string _floatName, float _value)
    {
        animator.SetFloat(_floatName, _value);
    }
}
