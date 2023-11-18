using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    enum Mode { Rocket, Wheels }

    [SerializeField] Steer _wheelcar;
    [SerializeField] RocketSteer _rocketcar;
    [SerializeField] List<CheckPoint> _checkpoints = new List<CheckPoint>();

    bool _accel, _steerL, _steerR;
    float _throttle;
    float _steering;
    UIScript _ui;
    int _currentCheckpoint = 0;
    float _raceTimer = - 10;

    public float RaceTimer { get => _raceTimer; }
    
    Mode _mode = Mode.Rocket;
    // Start is called before the first frame update
    void Start()
    {
        // get UI script to update onscreen throttle/steering
        _ui = GameObject.Find("UI").GetComponent<UIScript>();
        // setup checkpoints
        if (_checkpoints.Count > 0)
        {
            _checkpoints[0].OnCheckpointTrigger += NextCheckpoint;
            for (int i = 1; i < _checkpoints.Count; i++)
            {
                _checkpoints[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region Controls
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
        #endregion
        #region Race 
        _raceTimer += Time.deltaTime;

        #endregion

    }
    void NextCheckpoint()
    {
        if (_currentCheckpoint < _checkpoints.Count-1) 
        {
            _checkpoints[_currentCheckpoint].OnCheckpointTrigger -= NextCheckpoint;
            _checkpoints[_currentCheckpoint].gameObject.SetActive(false);
            _currentCheckpoint++;
            _checkpoints[_currentCheckpoint].gameObject.SetActive(true);
            _checkpoints[_currentCheckpoint].OnCheckpointTrigger += NextCheckpoint;
            UIText.DisplayText("Checkpoint!");
        }
        else
        {
            UIText.DisplayText("Finished");
            // race is finished
        }
    }
}