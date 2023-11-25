using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class volume : MonoBehaviour
{

    private float tempVolume;
    [SerializeField] private AudioMixer AudioMixer;

    public void SetVolume(float volume)
    {
        AudioMixer.SetFloat("Volume", volume);
        tempVolume = volume;
    }

}
