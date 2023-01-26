
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    [SerializeField] private float _goundAcceleration;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _airAcceleration;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _dragCoef;
    
    [SerializeField] private GameObject _deathVFX;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _root;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private FollowCamera _followArmature;
    [SerializeField] private Timer _timer;
    
    private List<GameObject> _overlappedObjects;
    
    private Rigidbody _rigidbody;
    private PlayerControlls _input;

    private bool _canMove = true;
    private bool _isDead = false;
    private bool _isFalling = false;
    
    private Vector2 _inputDirection;
    private float _mouseYDelta;
    
    public bool IsDead => _isDead;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _input = new PlayerControlls();
        _input.Ball.Move.performed += MoveInput;
        _input.Ball.Move.canceled += StopMove;
        _input.Ball.Jump.started += Jump;
        _input.Ball.Mouse.performed += RotateInput;
        _input.Ball.Use.started += UseObjects;
        
        _timer.onTimerStop += Death;
    }

    private void Start()
    {
        if (!_gameManager)
            _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Move();
        }
        if(_followArmature)
            _followArmature.MoveCamera(transform.position);
    }
    
    public void Move()
    {
        var accelerate = _isFalling ? _airAcceleration : _goundAcceleration;

        var forward = Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up);
        var right = Vector3.ProjectOnPlane(_camera.transform.right, Vector3.up);

        var direction = forward.normalized * _inputDirection.y + right.normalized * _inputDirection.x;

        var force = direction * accelerate;
        
         //Stop movement if Input is null
        if (direction.Equals(Vector3.zero))
        {
            var newX = _rigidbody.velocity.x > 0 ? - _dragCoef* Time.fixedDeltaTime : _dragCoef* Time.fixedDeltaTime;
            var newZ = _rigidbody.velocity.z > 0 ? - _dragCoef*  Time.fixedDeltaTime : _dragCoef*  Time.fixedDeltaTime;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x + newX, _rigidbody.velocity.y, _rigidbody.velocity.z +newZ);
        }
    
        _rigidbody.AddForce(force, ForceMode.Acceleration);
    }
    
    public void RotateInput(InputAction.CallbackContext context)
    {
        _mouseYDelta = context.ReadValue<float>();
    }
    
    public void MoveInput(InputAction.CallbackContext context)
    { 
        _inputDirection = context.ReadValue<Vector2>();
    }
    
    public void StopMove(InputAction.CallbackContext context)
    {
        _inputDirection = Vector2.zero;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!_isFalling)
        {
            _rigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Acceleration);
        }
    }
    
    public void Death()
    {
        _isDead = true;

        if(TryGetComponent(out MeshRenderer renderer))
        {
            renderer.enabled = false;
        }

        DeactivateMovement();
        
        if(TryGetComponent(out Collider collider))
        {
            collider.enabled = false;
        }

        
        Instantiate(_deathVFX, transform.position, transform.rotation);
        
        if (_gameManager)
        {
            _gameManager.Lose();
           
        }
    }
    
    public void DeactivateMovement()
    {
        _input.Disable();
        _canMove = false;
        
        if(_rigidbody)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.isKinematic = true;
        }
    }
    
    public void ActivateMovement()
    {
        _canMove = true;
        
        if(_rigidbody)
        {
            _rigidbody.isKinematic = false;
        }
    }
    
    private void UseObjects(InputAction.CallbackContext context)
    {
        var objects = Physics.OverlapSphere(transform.position, 0.5f);
        
        foreach (var activeObject in objects)
        {
            if (activeObject.TryGetComponent(out InteractiveObject obj))
            {
                if (obj.ObjectType == InteractiveObject.InteractiveObjectType.Usable)
                {
                    obj.Use();
                }
            }
                
        }
    }

  
    private void OnCollisionStay(Collision collision)
    {
        _isFalling = false;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        if (collision.gameObject.CompareTag("DynamicObject"))
        {
            Debug.Log("DynamicObject");
            var obj = collision.gameObject.GetComponent<DynamicObject>();
                transform.SetParent(obj.Root);

        }
        _isFalling = false;
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("DynamicObject"))
        {
            transform.SetParent(_root.transform);
        }
        
        _isFalling = true;
    }
    
}
