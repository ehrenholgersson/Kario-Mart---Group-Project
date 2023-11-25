using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HoverController : MonoBehaviour
{

    [SerializeField, Range(-1, 1)] public float steer;
    [SerializeField, Range(-1, 1)] public float forward;
    public bool boost;

    [SerializeField] Rocket _mainEngine;
    [SerializeField] Rocket _steerleft;
    [SerializeField] Rocket _steerright;
    [SerializeField] float _baseForwardPower;
    [SerializeField] float _boostPower;
    [SerializeField] float _baseSteerPower;
    [SerializeField] float _baseSlideFrictionAmount;
    
    [SerializeField] float _BrakeForce;
    ParticleSystem _explosion;
    GameObject _playerModel;
    GameObject _virtualCam;
    GameObject _shield;
    //[SerializeField] float Rotation;
    //Vector3 _desiredRotation;
    Rigidbody _rb;
    List<HoverPad> hoverPads = new List<HoverPad>();
    bool _grounded;
    float _slideFrictionAmount;
    float _forwardPower;
    float _steerPower;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("running HoverController.Start ");

        _virtualCam = GameObject.Find("VCamera").gameObject;
        _playerModel = transform.Find("PlayerModel").gameObject;
        _explosion = transform.Find("Explosion").GetComponent<ParticleSystem>();
        _shield = transform.Find("Shield").gameObject;
        _shield.SetActive(false);
        _slideFrictionAmount = _baseSlideFrictionAmount / 1.3f + (_baseSlideFrictionAmount * ((float)PlayerSetup.Grip / 25));
        _forwardPower = _baseForwardPower / 1.3f + (_baseForwardPower * ((float)PlayerSetup.Acceleration / 25));
        _steerPower = (_baseSteerPower / 1.3f) + (_baseSteerPower * ((float)PlayerSetup.Handling / 25));
        
        foreach (HoverPad pad in GetComponentsInChildren<HoverPad>())
        {
            hoverPads.Add(pad);
        }

        if (!TryGetComponent<Rigidbody>(out _rb))
            Debug.Log("Why!!");
        if (_rb == null)
        {
            _rb = GetComponentInChildren<Rigidbody>();
        }
        
        _rb.centerOfMass = Vector3.zero;
        _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ; // only move on y, so we dont have to be perfect on height when placing the car
        GameControl.OnRaceStart += StartRace; // unfreeze when we go
    }

    private void OnDestroy()
    {
        GameControl.OnRaceStart -= StartRace;
    }

    public void Shield()
    { 
        _shield.SetActive(true); 
    }

    void StartRace()
    {
        _rb.constraints = RigidbodyConstraints.None; 
        //_rb.velocity = Vector3.zero;
    }

    public async void PlayerDeath(float respawnTime)
    {
        _shield.SetActive(false);
        _explosion.Play();
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _playerModel.SetActive(false);
        await Task.Delay((int)(1000 * respawnTime));
        if (this!=null)
        {
            transform.position = GameControl.RespawnPoint.transform.position;
            transform.rotation = GameControl.RespawnPoint.transform.rotation;
            _explosion.Stop();
            _rb.constraints = RigidbodyConstraints.None; 
            _playerModel.SetActive(true);
            _virtualCam.transform.position = transform.position;
        }
    }
    public async void PlayerDeath()
    {
        _shield.SetActive(false);
        _explosion.Play();
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _playerModel.SetActive(false);
        await Task.Delay(3000);
        if (gameObject != null)
        {
            transform.position = GameControl.RespawnPoint.transform.position;
            transform.rotation = GameControl.RespawnPoint.transform.rotation;
            _explosion.Stop();
            _rb.constraints = RigidbodyConstraints.None;
            _playerModel.SetActive(true);
            _virtualCam.transform.position = transform.position;
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
        _rb.velocity -= slideAmount * (_BrakeForce / 10); // this need improving
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
                _mainEngine.SetPower(forward * _forwardPower);
            else
                Brake();
        }
        else
        {
            _mainEngine.SetPower(forward * _forwardPower/4);
        }
        if (steer < 0)
        {
            _steerright.SetPower(0);
            _steerleft.SetPower(steer * _steerPower);
        }
        else
        {
            _steerleft.SetPower(0);
            _steerright.SetPower(-steer * _steerPower);
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
