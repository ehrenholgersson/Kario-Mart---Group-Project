using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    bool _accel, _steerL, _steerR;
    float _throttle;
    float _steering;
    UIScript _ui;
    [SerializeField] Steer _wheelcar;
    [SerializeField] RocketSteer _rocketcar;
    enum Mode { Rocket, Wheels }
    Mode _mode = Mode.Rocket;
    // Start is called before the first frame update
    void Start()
    {
        _ui = GameObject.Find("UI").GetComponent<UIScript>();
    }

    // Update is called once per frame
    void Update()
    {

        _steering = Input.GetAxis("Horizontal");
        _throttle = Input.GetAxis("Vertical");
        if (_wheelcar != null)
        {
            _wheelcar._motorTorque = _throttle * 100;
            _wheelcar._steeringAngle = _steering * 100;
        }
        if (_rocketcar != null)
        {
            _rocketcar.steer = _steering;
            _rocketcar.forward = _throttle;
        }
        _ui.steering = _steering;
        _ui.throttle = _throttle;
        
    }
}