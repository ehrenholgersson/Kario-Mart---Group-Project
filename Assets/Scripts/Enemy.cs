using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    MeshRenderer _mr;
    BoxCollider _boxCollider;
    MoveObject _moveScript;
    [SerializeField] float _reactivateTime = 10;
    float _timer;

    private void Start()
    {
        _mr = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _moveScript = GetComponent<MoveObject>();
    }
    public void DeactivateEnemy()
    {
        _mr.enabled = false;
        _boxCollider.enabled = false;
        _timer = Time.time;
    }
    private void FixedUpdate()
    {
        if (_mr.enabled == false) 
        {
            if (Time.time > _timer + _reactivateTime)
            {
                _mr.enabled = true;
                _boxCollider.enabled = true;
                _moveScript.ResetPosition();
            }
        
        }
    }
}
