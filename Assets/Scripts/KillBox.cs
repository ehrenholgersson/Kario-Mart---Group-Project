using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && other.gameObject.TryGetComponent<HoverController>(out HoverController player))
        {
            player.PlayerDeath();

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.TryGetComponent<HoverController>(out HoverController player))
        {
            player.PlayerDeath();
        }
    }
}
