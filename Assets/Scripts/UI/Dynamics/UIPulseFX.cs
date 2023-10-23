using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPulseFX : MonoBehaviour
{
    public AnimationCurve animCurve;
    public bool doScale;
    public bool doColor;
    public Gradient colorOverCurve;
    public Vector2 uniformScaleOverCurve;
    float _lastT = 0f;
    float _duration = 1f;
    float _nextScale = 1f;
    private void OnEnable()
    {
        _lastT = 0f;
        _duration = 5f; //animCurve.keys[animCurve.keys.Length].time; <-- this key check didn't work? Why?
        if (!doColor && !doScale) { this.enabled = false; }
        //Debug.Log("Duration = "  + _duration);
    }
    private void LateUpdate()
    {
        _lastT += Time.deltaTime;
        _lastT %= _duration;
        float eval = animCurve.Evaluate(_lastT);
        if (doScale)
        {
            _nextScale = Mathf.Lerp(uniformScaleOverCurve.x, uniformScaleOverCurve.y, eval);
            transform.localScale = Vector3.one * _nextScale;
            //Debug.Log("Last T = " + _lastT);
        }



    }
}
