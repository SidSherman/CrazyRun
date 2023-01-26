using UnityEngine;

public class Mover : Actor
{
    override protected void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.position, Time.deltaTime * _speed);
        if (CheckDistanceToTarget())
        {
            SetTarget();
            
            Target = _points[CurrentPoint];
            if(_shoudLookAt)
                transform.LookAt(Target.position);
        }
    }

    override protected void SetTarget()
    {
        if(_shouldStopInTheEnd)
        {
             if(CurrentPoint + 1 == _points.Length)
                {
                    DeativateMovement ();
                }
        }

        if(_shouldMoveInReverseDirection)
        {
            if(Direction)
            {
                if(CurrentPoint + 1 == _points.Length)
                {
                    Direction = false;
                    CurrentPoint--;
                }
                else
                {
                    CurrentPoint++;
                }   
            }
            else
            {   
                if(CurrentPoint == 0)
                {
                    Direction = true;
                    CurrentPoint++;
                }
                else
                {
                    CurrentPoint--;
                }
            }
        }
        else{
            if(CurrentPoint + 1 < _points.Length)
            {
                CurrentPoint++;
            }
            else {
                CurrentPoint = 0;
            }
        }
    }
    
}


