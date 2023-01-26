using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision))]
public class InteractiveObject : MonoBehaviour
{
    [SerializeField] protected bool _isActive;
    [SerializeField] protected InteractiveObjectType _objectType;

    public InteractiveObjectType ObjectType => _objectType;

    public enum InteractiveObjectType
    {
        Overlapping,
        Usable
    }
    
    public bool IsActive => _isActive;
    
    void Start()
    {
        if (_isActive)
        {
            Activate();
        }
    }
    public virtual void Use()
    {
        if (_isActive)
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }
    
    public virtual void Activate()
    {
        _isActive = true;
        //Debug.Log("Activate");
    }
    
    public virtual void Deactivate()
    {
        _isActive = false;
       // Debug.Log("Deactivate");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_objectType == InteractiveObjectType.Overlapping)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Use();
            }
        }
    }
    
}

