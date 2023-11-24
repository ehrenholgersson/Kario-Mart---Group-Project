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
    public static GameObject PlayerPrefab { get => SelectedCharacter.ReturnCharacter()??null; }
    
    public static int TopSpeed { get => SelectedCharacter.ReturnValue("TopSpeed"); }
    public static int Acceleration { get => SelectedCharacter.ReturnValue("Acceleration"); }
    public static int Handling { get => SelectedCharacter.ReturnValue("Handling"); }
    public static int Grip { get => SelectedCharacter.ReturnValue("Grip"); }
    public static PlayerCharacter NextCharacter { get => SelectedCharacter.Next(); }
    public static PlayerCharacter PreviousCharacter { get => SelectedCharacter.Previous(); }

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

    protected PlayerCharacter Next()
    {
        _selected = ((_selected + 1) % _characters.Count);
        return _characters[_selected];
    }

    protected PlayerCharacter Previous()
    {
        _selected -= 1;
        if (_selected < 0)
        {
            _selected += _characters.Count;
        }
        return _characters[_selected];
    }

    protected GameObject ReturnCharacter()
    {
        if ( _characters.Count > 0)
        {
            return _characters[_selected].characterPrefab;
        }
        return null;
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
