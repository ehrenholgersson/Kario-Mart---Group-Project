using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.DeactivateEnemy();
            gameObject.SetActive(false);
        }
    }
}
