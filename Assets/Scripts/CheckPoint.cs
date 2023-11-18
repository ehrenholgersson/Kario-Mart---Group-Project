using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Action onCheckpointTrigger;

    private void OnTriggerEnter(Collider other)
    {
        onCheckpointTrigger?.Invoke();
    }
}
