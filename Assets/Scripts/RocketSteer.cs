using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSteer : MonoBehaviour
{

    [SerializeField, Range(-1, 1)] public float steer;
    [SerializeField, Range(-1, 1)] public float forward;
    public bool boost;

    [SerializeField] Rocket _mainEngine;
    [SerializeField] Rocket _steerleft;
    [SerializeField] Rocket _steerright;
    [SerializeField] float _maxforwardPower;
    [SerializeField] float _boostPower;
    [SerializeField] float _maxSteerPower;
    [SerializeField] float _slideFrictionAmount;
    [SerializeField] float _BrakeForce;
    //[SerializeField] float Rotation;
    //Vector3 _desiredRotation;
    Rigidbody _rb;
    List<HoverPad> hoverPads = new List<HoverPad>();
    bool _grounded;
    // Start is called before the first frame update
    void Start()
    {
        foreach(HoverPad pad in GetComponentsInChildren<HoverPad>())
        {
            hoverPads.Add(pad);
        }
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

    void Brake()
    {
        Vector3 slideAmount = Vector3.Project(_rb.velocity, transform.forward);
        _rb.velocity -= slideAmount * (_BrakeForce / 100); // this need improving
    }

    // Update is called once per frame //test
    void Update()
    {
        _mainEngine.boost = boost;
        if (boost)
        {
            forward = 1 + _boostPower;
        }
        //Rotation = _rb.angularVelocity.y;
        if (_grounded)
        {
            if (forward>=0)
                _mainEngine.SetPower(forward * _maxforwardPower);
            else
                Brake();
        }
        else
        {
            _mainEngine.SetPower(forward * _maxforwardPower/4);
        }
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
        if (boost)
        {
            forward -= _boostPower;
            boost = false;
        }
    }
    private void FixedUpdate()
    {
        _grounded = false;
        foreach (HoverPad pad in hoverPads)
        {
            if (pad.grounded)
            {
                _grounded = true;
                break;
            }
        }
        if(_grounded)
            SlideFriction();
    }
}
