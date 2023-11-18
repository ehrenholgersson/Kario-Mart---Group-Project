using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSteer : MonoBehaviour
{

    [SerializeField, Range(-1, 1)] public float steer;
    [SerializeField, Range(-1, 1)] public float forward;

    [SerializeField] Rocket _mainEngine;
    [SerializeField] Rocket _steerleft;
    [SerializeField] Rocket _steerright;
    [SerializeField] float _maxforwardPower;
    [SerializeField] float _maxSteerPower;
    [SerializeField] float _slideFrictionAmount;
    [SerializeField] float Rotation;
    //Vector3 _desiredRotation;
    Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<Rigidbody>(out _rb))
            Debug.Log("Why!!");
        if (_rb==null)
        {
            _rb = GetComponentInChildren<Rigidbody>();
        }
    }

    void SlideFriction() // stop vehicle moving sideways
    {
        Vector3 slideAmount = Vector3.Project(_rb.velocity, transform.right);
        _rb.velocity -= slideAmount * (_slideFrictionAmount/10); // this need improving
    }

    // Update is called once per frame //test
    void Update()
    {
        //Vector3 desiredrotation = ;
        Rotation = _rb.angularVelocity.y;
        _mainEngine.SetPower(forward *_maxforwardPower);
        if (steer < 0)
        {
            _steerright.SetPower(0);
            _steerleft.SetPower(steer * _maxSteerPower);
        }
        else
        {
            _steerleft.SetPower(0);
            _steerright.SetPower(-steer * _maxSteerPower);
        }
        
    }
    private void FixedUpdate()
    {
        SlideFriction();
    }
}
