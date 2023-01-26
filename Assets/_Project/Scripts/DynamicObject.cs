using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObject : MonoBehaviour
{
    [SerializeField] private Transform _root;

    public Transform Root => _root;
}
