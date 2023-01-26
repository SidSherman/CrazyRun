using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] protected bool _shouldMoveInReverseDirection;
    [SerializeField] protected bool _shouldStopInTheEnd;
    [SerializeField] protected bool _shoudMove;
    [SerializeField] protected bool _shoudLookAt;
    [SerializeField] protected bool _shoudRotate;
    [SerializeField] protected float _speed;
    [SerializeField] protected Vector3 _rotationSpeed;
    [SerializeField] protected Transform[] _points;
    [SerializeField] protected float _allowedDistanceToTarget = 0.1f;
    protected Transform Target;
    protected int CurrentPoint;
    protected bool Direction; //true = right, false = reversed
    
    
    protected virtual void  Start()
    {
        CurrentPoint = 0;
        if(_points.Length > 0)
            Target = _points[CurrentPoint];
        else Target = transform;

        transform.LookAt(Target.position);
    }

    protected virtual void  Update()
    {
        if (_shoudMove)
        {
            Movement();
            
        }

        if (_shoudRotate)
        {
            Rotation();
        }

    }

    protected virtual bool CheckDistanceToTarget()
    {
        if (_allowedDistanceToTarget > Vector3.Distance(transform.position, Target.position))
        {
            return true;
        }
        else return false;
    }
    protected virtual void SetTarget() { }
    protected virtual void Movement() { }

    protected virtual void Rotation ()
    {
       
        transform.Rotate(Time.deltaTime * _rotationSpeed.x,Time.deltaTime * _rotationSpeed.y,Time.deltaTime * _rotationSpeed.z);
    }

    protected virtual void  ActivateMovement ()
    {
        if(_shoudLookAt)
            transform.LookAt(Target.position);
        _shoudMove = true;
    }
    
    protected virtual void  DeativateMovement ()
    {
        _shoudMove = false;
    }

    
    
}

