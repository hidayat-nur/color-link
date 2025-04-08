﻿/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/**

*/
public class TextTimer: MonoBehaviour
{

    Text _label = null;

    long _target = 0;

    long _delta = 0;

    UnityAction _callback = null;

    UnityAction _updataCallback = null;

    bool _timing = false;

    bool _real = false;

    int _timeScale = 1;

    bool showText = true;

    string format = "{0}:{1}";

    void Awake() {
        _label = transform.GetComponent<Text>();
    }

    public void setCallback(UnityAction callback) {
        _callback = callback;
    }

    public void setUpdataCallback(UnityAction callback)
    {
        _updataCallback = callback;
    }

    public void setTimeScale(int timeScale) {
        _timeScale = timeScale;
    }

    public void setReal(bool real)
    {
        _real = real;
    }

 
    public void startTiming(long targetTimeTicks)
    {
        _target = (long)(targetTimeTicks * Math.Pow(10, 7));
        _timing = true;
        refresh();
    }
    
    public void startTimingBySeconds(int IntervalSeconds)
    {
        _target = (long)(DateTime.Now.Ticks + IntervalSeconds * Math.Pow(10, 7));
        _timing = true;
        refresh();
    }

  
    public void stopTiming() {
        _timing = false;
    }


    public void startTiming()
    {
        _timing = true;
    }

    public void setTime(long targetTimeTicks)
    {
        _target = (long)(targetTimeTicks * Math.Pow(10, 7));
        refresh();
    }

    public void setTimeBySeconds(int IntervalSeconds)
    {
        _target = (long)(DateTime.Now.Ticks + IntervalSeconds * Math.Pow(10, 7));
        refresh();
    }

    public float getTime() {
        return _delta / 1000;
    }

    public void setTextShow(bool isShow) {
        showText = isShow;
    }

    public void setFormat(string myFormat) {
        format = myFormat;
    }
    void Update() {
        int dt = (int)(Time.deltaTime * Math.Pow(10,7));
        dt *= _timeScale;
        if (_timing && _delta > 0)
        {
            _updateLabel();
            if (_updataCallback!= null)
            {
                _updataCallback();
            }
            if (_real)
            {
                _delta = _target - DateTime.Now.Ticks;
            }
            else
            {
                _delta -= dt;

            }
            if (_delta <= 0 && _callback != null)
            {
                _timing = false;
                _callback();
            }
        }
    }

    void _updateLabel()
    {
        TimeSpan ts = new TimeSpan(_delta +1000);
        if (_label != null)
        {
            if (showText)
            {
                _label.text = string.Format(format,ts.Minutes.ToString("00"),ts.Seconds.ToString("00"));
            }
            else {
                _label.text = "";
            }
        }
    }

    void refresh() {
        _delta = _target - DateTime.Now.Ticks;
        _updateLabel();
    }

}
