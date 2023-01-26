using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimationObject : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private int _animatorState;

    public int AnimatorState => _animatorState;

    /// <summary>
    /// 0 = Rotating 180 degrees
    /// 1 = Rotating 180 degrees Slowly
    /// 2 = Rotating 360 degrees
    /// 4 = Move
    /// </summary>
    
    void Start()
    {
        SetAnimState(_animatorState);
    }
    
    void SetAnimState(int state)
    {
        _animatorState = state;
        _anim.SetInteger("CurrentAnimState",state );
    }
    
    void SetTrigger(string trigger)
    {
        _anim.SetTrigger(trigger);
    }

    void SetRandomAnimState()
    {
        bool randb = Random.Range(0, 100) > 80 ? true : false;
        
        if (randb)
        {
            SetTrigger("Disappear");
        }
        else
        {
            int rand = Random.Range(0,4);
            SetAnimState(rand);
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
    
}
