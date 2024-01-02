using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour
{
    GameObject _character;

    private void Start()
    {
        UpdateCharacter();
    }
    private void UpdateCharacter()
    {
        if (_character != null)
        {
            Destroy(_character);
        }
        if (PlayerSetup.PlayerPrefab != null)
        {
            _character = Instantiate(PlayerSetup.PlayerPrefab);
            _character.transform.SetParent(transform);
            _character.transform.localPosition = Vector3.zero;
            _character.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.Log("Failed to load character");
            NextCharacter();
        }
    }
    public void NextCharacter()
    {
        if (_character != null)
        {
            Destroy(_character);
        }
        if (PlayerSetup.PlayerPrefab != null)
        {
            _character = Instantiate(PlayerSetup.NextCharacter.characterPrefab);
            _character.transform.SetParent(transform);
            _character.transform.localPosition = Vector3.zero;
            _character.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.Log("Failed to load character");
            NextCharacter();
        }
        }
    public void PreviousCharacter()
    {
        if (_character != null)
        {
            Destroy(_character);
        }
        if (PlayerSetup.PlayerPrefab != null)
        {
            _character = Instantiate(PlayerSetup.PreviousCharacter.characterPrefab);
            _character.transform.SetParent(transform);
            _character.transform.localPosition = Vector3.zero;
            _character.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.Log("Failed to load character");
            NextCharacter();
        }
    }
}
