using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gyro : MonoBehaviour
{
    struct Line
    {
        public Vector3[] endPoints;
        public Color color;

        public Line(Vector3 start, Vector3 end)
        {
            endPoints = new Vector3[2];
            endPoints[0] = start;
            endPoints[1] = end;
            color = Color.white;
        }
        public Line(Vector3 start, Vector3 end, Color col)
        {
            endPoints = new Vector3[2];
            endPoints[0] = start;
            endPoints[1] = end;
            color = col;
        }
    }


    [SerializeField] float _oldStrength;
    [SerializeField] float _strength;
    [SerializeField] float _dampen;
    Rigidbody _rb;
    int _layermask;
    [SerializeField] float _maxRange = 3;
    Vector3 _desiredRotation;
    Vector3 _directionOfRotation;
    List<GyroSensor> _sensors = new List<GyroSensor>();
    [SerializeField] bool _oldCode;
    [SerializeField] bool _downwardSensor;
    List<Line> _debugLines = new List<Line>();


    private void Start()
    {
        _rb = transform.parent.GetComponent<Rigidbody>();
        _layermask = LayerMask.GetMask("Terrain");
        foreach (GyroSensor g in GetComponentsInChildren<GyroSensor>())
        {
                _sensors.Add(g);
        }
    }
    private void FixedUpdate()
    {
        _debugLines.Clear();
        if (_oldCode)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, -Vector3.up, out hit, _maxRange, _layermask))
            {
                _desiredRotation = hit.normal;

                Quaternion torqueDirection = Quaternion.FromToRotation(transform.up, _desiredRotation);
                //Vector3 torqueToApply = new Vector3(torqueDirection.x, torqueDirection.y, torqueDirection.z);

                _directionOfRotation = transform.InverseTransformDirection(_desiredRotation);//((Vector3.RotateTowards(transform.up, _desiredRotation, 20, 20) - transform.up).normalized);// * _strength * (transform.up - _desiredRotation).magnitude;//new Vector3(torqueDirection.x, torqueDirection.y, torqueDirection.z) * _strength;
                                                                                             // maths stuppid
                Vector3 desiredRotation = new Vector3(Mathf.Acos(1 / (new Vector2(_directionOfRotation.z, _directionOfRotation.y).normalized / _directionOfRotation.y).magnitude), 0, Mathf.Acos(1 / (new Vector2(_directionOfRotation.x, _directionOfRotation.y).normalized / _directionOfRotation.y).magnitude));
                if (_directionOfRotation.y < 0)
                {
                    desiredRotation.x = Mathf.PI - desiredRotation.x;
                    desiredRotation.y = Mathf.PI - desiredRotation.y;
                }
                if (_directionOfRotation.x < 0)
                {
                    desiredRotation.z *= -1;
                }
                if (_directionOfRotation.x < 0)
                {
                    desiredRotation.x *= -1;
                }
                //desiredRotation = transform.InverseTransformVector(desiredRotation);


                if (Quaternion.Dot(torqueDirection, _rb.rotation) < 0.5f || Vector3.Project(_rb.angularVelocity, _directionOfRotation).magnitude < 0.6f)
                {
                    if (desiredRotation.magnitude > 0)
                        _rb.AddTorque((-desiredRotation - _rb.angularVelocity) * _oldStrength);//_directionOfRotation));
                    Debug.Log("applied rotation");
                    Debug.Log("rotating X:" + desiredRotation.x + " Z:" + desiredRotation.z);
                }
                else
                {
                    if (Quaternion.Dot(torqueDirection, _rb.rotation) > 0.5f)
                        Debug.Log("moving towards center");
                    if (_rb.angularVelocity.magnitude > 0.2f)
                        Debug.Log("moving quickly");

                }
            }
            else

            {
                _desiredRotation = Vector3.up;

            }
        }
        else
        {
            _desiredRotation = Vector3.zero;

            if (_downwardSensor)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -Vector3.up, out hit, _maxRange, _layermask))
                {
                    //Debug.Log("gyro is working");
                    _desiredRotation += hit.normal; //always has a weight of 1
                    _debugLines.Add(new Line(transform.position, transform.position - Vector3.up * _maxRange,Color.blue));
                }
            }

            foreach (GyroSensor g in _sensors)
            {
                RaycastHit hit;

                if (Physics.Raycast(g.transform.position, -g.transform.up, out hit, g.range, _layermask)) 
                {
                    //Debug.Log("gyro is working");
                    _desiredRotation += hit.normal * g.weight;
                    _debugLines.Add(new Line(g.transform.position, g.transform.position - (g.transform.up * g.range), Color.blue));
                }
                else
                    _debugLines.Add(new Line(g.transform.position, g.transform.position - (g.transform.up * g.range), Color.red));
            }
            if (_desiredRotation != Vector3.zero)
            {
                _desiredRotation.Normalize();
            }
            else
            {
                //Debug.Log("no hits on gyro sensors");
                _desiredRotation = Vector3.up;
            }

            var springTorque = _strength * Vector3.Cross(_rb.transform.up, Vector3.up);
            var dampTorque = _dampen * - _rb.angularVelocity;
            _rb.AddTorque(springTorque + dampTorque, ForceMode.Acceleration);
        }

        //Debug.Log("Angular Velocity = " + _rb.angularVelocity.magnitude + "rotation magnitiude = " + new Vector3(torqueDirection.x, torqueDirection.y, torqueDirection.z).magnitude);

        //Debug.Log("Gyro Correction Amount: " + correctionAmount);
    }
    private void OnDrawGizmos()
    {
        
        foreach (Line l in _debugLines) 
            {
            Gizmos.color = l.color;
            Gizmos.DrawLine(l.endPoints[0], l.endPoints[1]);
            }
    //    Gizmos.DrawLine(transform.position, transform.position + _desiredRotation * 5);
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(transform.position + transform.up * 5, transform.position + transform.up * 5 + _directionOfRotation);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, transform.position + transform.up * 5);
    }
}
