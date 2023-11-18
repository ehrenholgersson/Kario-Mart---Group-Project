using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Action OnCheckpointTrigger;

    private void OnTriggerEnter(Collider other)
    {
        OnCheckpointTrigger?.Invoke();
    }
}
