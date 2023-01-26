using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject _followingObgect;
    [SerializeField] private GameObject _followCamera;

    public GameObject FollowCamera1 => _followCamera;

    private void LateUpdate()
    {
        if (_followingObgect && _followCamera)
        {
            _followCamera.transform.LookAt(_followingObgect.transform);
        }
    }

    public void RotateCamera(float rotationSpeed, float rotationDirection )
    {
        transform.Rotate( rotationDirection * Vector3.up * rotationSpeed);
    }

    public void MoveCamera(Vector3 position)
    {
        transform.position = position;
    }
}
