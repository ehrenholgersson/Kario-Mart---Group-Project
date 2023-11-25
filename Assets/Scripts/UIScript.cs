using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] Transform _wheel;
    [SerializeField] Image _throttle;
    [SerializeField] Image _boost;
    [SerializeField] Color _tStartColor;
    [SerializeField] Color _tEndColor;
    [SerializeField] TextMeshProUGUI _totalTime;
    [SerializeField] TextMeshProUGUI _remainingTime;
    public float steering;
    public float throttle;
    public float boost;
    // Update is called once per frame
    void Update()
    {
        _throttle.fillAmount = throttle;
        _boost.fillAmount = boost;
        _wheel.rotation = Quaternion.Euler(0,0,steering*-120);
        Vector3 tCol = Vector3.Lerp(new Vector3(_tStartColor.r, _tStartColor.g, _tStartColor.b), new Vector3(_tEndColor.r, _tEndColor.g, _tEndColor.b), throttle);
        _throttle.color = new Color(tCol.x,tCol.y,tCol.z,1);
        if (GameControl.RaceTimer > 0)
            _totalTime.text = Math.Round((double)GameControl.RaceTimer,2).ToString();
        if (GameControl.RaceState != GameControl.Mode.Finished)
            _remainingTime.text = Math.Round((double)GameControl.RemainingTime, 2).ToString();
    }

    //public void Countdown(int count)
    //{
    //    switch (count)
    //    {
    //        case 2:
    //            break;
    //        case 1:
    //            break;
    //        case 0:
    //            break;

    //    }
    //}
}
