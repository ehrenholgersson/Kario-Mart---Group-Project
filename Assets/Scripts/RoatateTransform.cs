using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoatateTransform : MonoBehaviour
{
    [SerializeField] float _speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * _speed, 0);
    }
}
