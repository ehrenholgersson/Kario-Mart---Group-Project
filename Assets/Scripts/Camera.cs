using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject _target;
    [SerializeField] float _zOffset;
    [SerializeField] float _yOffset;
    [SerializeField] float _maxCameraSpeed;
    Rigidbody _targetRigidbody;
    Vector3 _desiredPosition;

    private void Start()
    {
        _targetRigidbody = _target.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_targetRigidbody.velocity.magnitude > 2)
            _desiredPosition = _targetRigidbody.position + (-(_targetRigidbody.velocity - Vector3.Project(_targetRigidbody.velocity, Vector3.up)).normalized * _zOffset);
        _desiredPosition = new Vector3(_desiredPosition.x, _target.transform.position.y + _yOffset, _desiredPosition.z);
        transform.position = Vector3.Lerp(transform.position, _desiredPosition,Mathf.Clamp(_maxCameraSpeed / (transform.position - _desiredPosition).magnitude * Time.deltaTime,0f,1f));
    }
}


//Ryan Was Here