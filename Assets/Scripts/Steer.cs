using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steer : MonoBehaviour
{
    [SerializeField] List<WheelCollider> _steeringWheels = new List<WheelCollider>();
    [SerializeField] List<WheelCollider> _powerWheels = new List<WheelCollider>();
    [SerializeField, Range (-10, 100)] public float _motorTorque;
    [SerializeField, Range(-45, 45)] public float _steeringAngle;


    // Update is called once per frame
    void Update()
    {
        foreach (WheelCollider wheel in _steeringWheels)
        {
            wheel.steerAngle = _steeringAngle;
        }
        foreach (WheelCollider wheel in _powerWheels)
        {
            wheel.motorTorque = _motorTorque;
        }

    }
}
