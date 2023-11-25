using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    enum Type { BoostFuel, Shield }
    [SerializeField] Type _type;
    [SerializeField] float _returntime = 5;
    bool _enabled;
    GameControl _control;
    [SerializeField] GameObject _mesh;
    float _timer;
    // Start is called before the first frame updat
    private void Start()
    {
        _control = GameObject.Find("Controller").GetComponent<GameControl>();
        foreach (MeshRenderer r in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            if (r.transform != this.transform)
            {
                _mesh = r.gameObject;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PowerUp");
        if (_enabled && other.gameObject.tag == "Player" && other.gameObject.TryGetComponent<RocketSteer>(out RocketSteer player))
        {
            switch (_type)
            {
                case Type.BoostFuel:
                    _control.AddFuel(0.25f);
                    break;
                case Type.Shield:
                    break;
            }
            _enabled = false;
            _mesh.SetActive(false);
            _timer = Time.time;
        }
    }
    private void Update()
    {
        if (!_enabled) 
        {
            if (Time.time > _timer + _returntime)
            {
                _enabled = true;
                _mesh.SetActive(true);
            }
        }
    }
}
