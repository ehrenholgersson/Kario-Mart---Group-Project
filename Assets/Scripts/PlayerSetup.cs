using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSetup
{
    List<PlayerCharacter> _characters = new List<PlayerCharacter>();
    int _selected;
    static PlayerSetup _instance;
    public static PlayerSetup SelectedCharacter { get => (_instance ?? new PlayerSetup()); } //tryna be clever
    public static GameObject PlayerPrefab { get => _instance.ReturnCharacter(); }
    
    public static int TopSpeed { get => _instance.ReturnValue("TopSpeed"); }
    public static int Acceleration { get => _instance.ReturnValue("Acceleration"); }
    public static int Handling { get => _instance.ReturnValue("Handling"); }
    public static int Grip { get => _instance.ReturnValue("Grip"); }

    public PlayerSetup ()
    {
        //Application.DontDestroyOnLoad(this);
        if (_instance == null)
            _instance = this;
        foreach(PlayerCharacter p in Resources.LoadAll("Characters",typeof(PlayerCharacter)))
        {
            _characters.Add(p);
        }
    }

    public void Next()
    {
        _selected = ((_selected + 1) % _characters.Count);
    }

    public void Previous()
    {
        _selected -= 1;
        if (_selected < 0)
        {
            _selected += _characters.Count;
        }
    }

    protected GameObject ReturnCharacter()
    {
        return _characters[_selected].characterPrefab;
    }
    protected int ReturnValue(string Value)
    {
        switch (Value)
        {
            case "TopSpeed":
                return _characters[_selected].topSpeed;
                break;
            case "Acceleration":
                return _characters[_selected].acceleration;
                break;
            case "Handling":
                return _characters[_selected].handling;
                break;
            case "Grip":
                return _characters[_selected].grip;
            default: 
                return 0;
        }
    }
}
