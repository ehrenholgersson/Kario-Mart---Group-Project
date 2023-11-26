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
        if (GameControl.RemainingTime < 10 && GameControl.RaceState == GameControl.Mode.Racing)
        {
            _audio.pitch = 2 - (GameControl.RemainingTime / 10);
        }
        else
            _audio.pitch = 1;
    }
}
