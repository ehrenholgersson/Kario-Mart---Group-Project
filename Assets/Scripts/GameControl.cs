using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public enum Mode { PreRace, Racing, Finished }

    [SerializeField] Steer _wheelcar;
    [SerializeField] HoverController _rocketcar;
    [SerializeField] protected List<CheckPoint> _checkpoints = new List<CheckPoint>();
    [SerializeField] GameObject _menu;

    static GameControl _instance;   
    public static GameObject RespawnPoint { get => _instance.PrevCheckpoint(); }
    public static Action OnRaceStart;

    bool _accel, _steerL, _steerR;
    float _throttle;
    float _steering;

    UIScript _ui;

    protected int _currentCheckpoint = 0;
    float _raceTimer = - 7;
    float _remainingTime = 20;
    int countdown = 4;

    bool _boost;
    float _boostfuel = 1;

    public static float RaceTimer { get => _instance._raceTimer; }
    public static float RemainingTime { get => _instance._remainingTime; }
    
    Mode _mode = Mode.PreRace;
    public static Mode RaceState { get => _instance._mode; }
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;
        // get UI script to update onscreen throttle/steering
        _ui = GameObject.Find("UI").GetComponent<UIScript>();
        // setup checkpoints
        //if (_checkpoints.Count > 1)
        //{
        //    //_checkpoints[0].OnCheckpointTrigger += NextCheckpoint;
        //    for (int i = 1; i < _checkpoints.Count; i++)
        //    {
        //        _checkpoints[i].gameObject.SetActive(false);
        //    }
        //}
        _rocketcar.transform.position = _checkpoints[0].transform.position;
        _rocketcar.transform.rotation = _checkpoints[0].transform.rotation;
        Cursor.visible = false;
    }
    public void AddFuel(float amount)
    {
        _boostfuel += amount;
    }    

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    GameObject PrevCheckpoint() 
    {
        if (_currentCheckpoint > 0)
        {
            return _checkpoints[_currentCheckpoint-1].gameObject;
        }
        return _checkpoints[0].gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (_mode == Mode.PreRace)
        {
            if (Mathf.Abs(RaceTimer)< countdown-1)
            {
                countdown--;
                if (countdown > 0)
                    UIText.DisplayText(countdown.ToString());
            }
            if (RaceTimer >= 0)
            {
                UIText.DisplayText("GO!");
                _mode = Mode.Racing;
                OnRaceStart?.Invoke();
                if (_currentCheckpoint == 0) 
                    NextCheckpoint();
            }
        }
        #region Controls
        if (_mode == Mode.Racing)
        {
            _steering = Input.GetAxis("Horizontal");
            _throttle = Input.GetAxis("Vertical");
            _boost = Input.GetButton("Jump");
            if (_wheelcar != null)
            {
                _wheelcar._motorTorque = _throttle * 100;
                _wheelcar._steeringAngle = _steering * 100;
            }
            if (_rocketcar != null)
            {
                _rocketcar.steer = _steering;
                _rocketcar.forward = _throttle;
                _rocketcar.boost = (_boost && _boostfuel > 0);
            }
            _ui.steering = _steering;
            _ui.throttle = _throttle;
            _ui.boost = _boostfuel;
            if (_boost)
            {
                _boostfuel = Mathf.Clamp(_boostfuel - Time.deltaTime / 4, 0, 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
        #endregion

        #region Race Timers
        if (_mode != Mode.Finished)
        {
            _raceTimer += Time.deltaTime;
        }
        else
        {
            if (_remainingTime < 1 && !(_menu.activeSelf))
                ToggleMenu();
        }
        if (_mode != Mode.PreRace) 
            _remainingTime -= Time.deltaTime; 
        if (_remainingTime < 0)
        {
            _mode = Mode.Finished;
            _remainingTime = 6;
            UIText.DisplayText("Times Up");
            if (_rocketcar.Alive)
                _rocketcar.PlayerDeath(20);
        }
        #endregion
    }
    void NextCheckpoint()
    {
        if (_currentCheckpoint < _checkpoints.Count-1) 
        {
            _checkpoints[_currentCheckpoint].OnCheckpointTrigger -= NextCheckpoint;
            //_checkpoints[_currentCheckpoint].gameObject.SetActive(false);
            _remainingTime += _checkpoints[_currentCheckpoint].ExtraTime;
            _currentCheckpoint++;
            //_checkpoints[_currentCheckpoint].gameObject.SetActive(true);
            _checkpoints[_currentCheckpoint].OnCheckpointTrigger += NextCheckpoint;
            if (_currentCheckpoint!=1)
                UIText.DisplayText("Checkpoint!",1);
            
        }
        else
        {
            UIText.DisplayText("Finished");
            _mode = Mode.Finished;
            _remainingTime = 6;
            // race is finished
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }

    public void ToggleMenu()
    {
        if (_menu.activeSelf)
        {
            Cursor.visible = false;
            _menu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Cursor.visible = true;
            _menu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}