using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField, Range(0, 150)] float _power;
    [SerializeField] bool _pointBased;
    [SerializeField] float _baseSpeed = 100;
    public bool boost;
    float _topSpeed;
    Rigidbody _rb;
    ParticleSystem _trail;
    
    // Start is called before the first frame update
    void Start()
    {
        _topSpeed = _baseSpeed / 3 + (_baseSpeed * (PlayerSetup.TopSpeed / 10));
        _rb = transform.parent.GetComponent<Rigidbody>();
        _trail = GetComponentInChildren<ParticleSystem>();
    }

    public void SetPower(float power)
    {
        _power = power;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_trail != null)
        {
            if (boost)
            {
                if (!_trail.isPlaying)
                    _trail.Play();
            }
            else if (_trail.isPlaying)
                _trail.Stop();
        }

        if (boost || !(_topSpeed > 0) || Vector3.Project(_rb.velocity, transform.up).magnitude < _topSpeed)
        {
            if (_pointBased)
                _rb.AddForceAtPosition(transform.up * _power, transform.position);
            else
                _rb.AddForce(transform.up * _power);
        }
    }
}
