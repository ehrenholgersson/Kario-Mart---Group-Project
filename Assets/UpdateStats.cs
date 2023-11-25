using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStats : MonoBehaviour
{
    [SerializeField] Image _accel;
    [SerializeField] Image _speed;
    [SerializeField] Image _handling;
    [SerializeField] Image _grip;

    public void DrawStats()
    {
        _accel.fillAmount = PlayerSetup.Acceleration / 10f;
        _speed.fillAmount = PlayerSetup.TopSpeed / 10f;
        _handling.fillAmount = PlayerSetup.Handling / 10f;
        _grip.fillAmount = PlayerSetup.Grip / 10f;
    }

    private void OnEnable()
    {
        DrawStats();
    }
}
