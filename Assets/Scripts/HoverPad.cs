using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPad : MonoBehaviour
{
    [SerializeField] float _maxRange;
    [SerializeField] float _minRange;
    [SerializeField] float _maxForce;
    [SerializeField] float _minForce;
    [SerializeField] float _dampenAmount;

    public bool grounded { get; private set; }

    float _dampenValue;
    int _layermask;
    Rigidbody _rb;
    List<Vector3> _rays = new List<Vector3>();
    List<bool> _hits = new List<bool>();

    public enum Raytype {Single,Multi,Box};
    [SerializeField] Raytype _rayType;

    private void Start()
    {
        _layermask = LayerMask.GetMask("Terrain");
        _rb = transform.parent.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        grounded = false;
        if (_rb != null)
        {
            RaycastHit hit;
            switch (_rayType)
            {
                case Raytype.Box:
                    if (Physics.BoxCast(transform.position, Vector3.one * 0.4f, -transform.up, out hit, transform.rotation, _maxRange, _layermask)) //Physics.Raycast(transform.position, -transform.up, out hit, _maxRange, _layermask))
                    {
                        float force = Mathf.Lerp(_maxForce, _minForce, (hit.distance - _minRange) / _maxRange);
                        force -= Mathf.Clamp(Vector3.Project(_rb.velocity,transform.up).magnitude * _dampenAmount, 0,force);
                        _rb.AddForceAtPosition(force * transform.up, transform.position);
                        grounded = true;
                        //Debug.Log("Hoverpad - Distance: " + hit.distance + " Force: " + force);
                    }
                    break;

                case Raytype.Single:
                    // get rays for gizmo lines
                    _rays.Clear();
                    _hits.Clear();
                    _rays.Add(-transform.up * _maxRange);
                    _hits.Add(false);
                    if (Physics.Raycast(transform.position, -transform.up, out hit, _maxRange, _layermask))
                    {
                        float force = Mathf.Lerp(_maxForce, _minForce, (hit.distance - _minRange) / _maxRange);
                        _rb.AddForceAtPosition(force * transform.up, transform.position);
                        //Debug.Log("Hoverpad - Distance: " + hit.distance + " Force: " + force);
                        _hits[0] = true;
                        grounded = true;
                    }
                    break;

                case Raytype.Multi:
                    Vector3[] rayDirection = new Vector3[]
                    {
                        - transform.up + (transform.forward / 1.8f),
                        - transform.up + (-transform.forward / 1.8f),
                        - transform.up + (transform.right / 1.8f),
                        - transform.up + (-transform.right / 1.8f)
                    };
                    // get rays for gizmo lines
                    _rays.Clear();
                    _hits.Clear();
                    foreach(Vector3 r in rayDirection)
                    {
                        _rays.Add(r.normalized*_maxRange);
                        _hits.Add(false);
                    }
                    for (int i = 0; i < rayDirection.Length; i++)
                    {
                        if (Physics.Raycast(transform.position, rayDirection[i], out hit, _maxRange, _layermask))
                        {
                            float force = Mathf.Lerp(_maxForce, _minForce, (hit.distance - _minRange) / _maxRange);
                            _dampenValue = Mathf.Clamp(Vector3.Dot(_rb.velocity, -rayDirection[i]) * _dampenAmount, -force, force);
                            force -= _dampenValue;
                            _rb.AddForceAtPosition((force/3) * -rayDirection[i],transform.position);
                            //Debug.Log("Hoverpad - Distance: " + hit.distance + " Force: " + force);
                            _hits[i] = true;
                            grounded = true;
                        }
                    }
                    break;
            }

        }
    }
    private void OnDrawGizmos()
    {
        int i = 0;
        foreach(Vector3 r in _rays)
        {
            if (_hits[i])
                Gizmos.color = Color.green;
            else 
                Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, transform.position + r);
        }
    }
}
