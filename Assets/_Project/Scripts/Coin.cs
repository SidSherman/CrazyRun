using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : InteractiveObject
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _particleSystem;

    private void Start()
    {
        if (!_gameManager)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
      
    }
    
    public override void Activate()
    {
        base.Activate();
        _gameManager.UpdateScore(1);
        Instantiate(_particleSystem, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_objectType == InteractiveObjectType.Overlapping)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Activate();
            }
        }
    }
    

}