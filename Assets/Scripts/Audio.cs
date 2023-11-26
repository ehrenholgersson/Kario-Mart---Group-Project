using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.RemainingTime < 20)
        {
            _audio.pitch = 5 - (GameControl.RemainingTime / 5);
        }
    }
}
