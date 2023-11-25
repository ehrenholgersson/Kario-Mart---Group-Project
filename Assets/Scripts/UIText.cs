using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    float _alpha;
    float _fadeSpeed;
    float _defaultSpeed = 5;
    TextMeshProUGUI text;
    static UIText main;

    private void Start()
    {
        
        if (main == null)
        {
            main = this;
            text = GetComponent<TextMeshProUGUI>();
        }
        else Destroy(this);
        _alpha = 0;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (_alpha > 0)
        {
            _alpha -= Time.deltaTime/_fadeSpeed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, _alpha);
        }
    }

    void UpdateText(string newText)
    {
        _fadeSpeed = _defaultSpeed;
        text.text = newText;
        _alpha = 1;
        text.color = new Color(text.color.r, text.color.g, text.color.b, _alpha);
    }

    void UpdateText(string newText, float speed)
    {
        _fadeSpeed = speed;
        text.text = newText;
        _alpha = 1;
        text.color = new Color(text.color.r, text.color.g, text.color.b, _alpha);
    }

    public static void DisplayText(string newText)
    {
        main.UpdateText(newText);
    }
    public static void DisplayText(string newText, float speed)
    {
        main.UpdateText(newText, speed);
    }
}
