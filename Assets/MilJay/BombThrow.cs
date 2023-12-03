using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class BombThrow : MonoBehaviour

{
    [SerializeField] float speed = 0.1f;
    [SerializeField] GameObject _bombPrefab;
    float _timer; Vector3 targetpos;
    Vector3 direction = Vector3.zero;
    [SerializeField] float _bombspeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {

        _timer += Time.deltaTime;
        if (_timer > 4&&GameControl.RaceState==GameControl.Mode.Racing)
        {
            _timer = 0;
            direction = (GameControl.PlayerPosition - transform.position).normalized;
            direction -= new Vector3(0, direction.y, 0);
            GameObject bomb = Instantiate(_bombPrefab);
            bomb.transform.position = transform.position;
            bomb.GetComponentInChildren<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f)).normalized * _bombspeed; _timer = 0;

        }
        transform.position = transform.position + direction * speed;
    }
}