using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour
{
    GameObject _character;

    private void Start()
    {
        _character = Instantiate(PlayerSetup.PlayerPrefab);
        _character.transform.SetParent(transform);
        _character.transform.localPosition = Vector3.zero;
    }
}
