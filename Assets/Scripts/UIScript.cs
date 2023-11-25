using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] Transform _wheel;
    [SerializeField] Image _throttle;
    [SerializeField] Image _boost;
    [SerializeField] Color _tStartColor;
    [SerializeField] Color _tEndColor;
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
    }
}
