using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField, Range(0, 150)] float _power;
    [SerializeField] bool _pointBased;
    Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = transform.parent.GetComponent<Rigidbody>();
    }

    public void SetPower(float power)
    {
        _power = power;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_pointBased)
            _rb.AddForceAtPosition(transform.up * _power, transform.position);
        else
            _rb.AddForce(transform.up * _power);
    }
}
