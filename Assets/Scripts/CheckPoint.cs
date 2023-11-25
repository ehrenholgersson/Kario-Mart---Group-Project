using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Action OnCheckpointTrigger;
    [SerializeField] int _extratime = 10;
    public int ExtraTime { get => _extratime; }

    private void OnTriggerEnter(Collider other)
    {
        OnCheckpointTrigger?.Invoke();
    }
}
