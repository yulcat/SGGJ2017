using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

public class SGTimer {

    public int TimeLimit;
    int currentTime;
    public int CurrentTime { get { return currentTime; } }
    System.IDisposable subscribe;

    public Action<int> OnStart;
    public Action<int> OnUpdate;
    public Action OnOver;


    public void SetTime(int timeLimit)
    {
        TimeLimit = timeLimit;
    }

    public void TimerReset()
    {
        currentTime = TimeLimit;
    }

    public void TimerStart()
    {
        TimerStop();
        TimerReset();

        if(OnStart != null) OnStart.Invoke(currentTime);
        subscribe = Observable.Interval(System.TimeSpan.FromSeconds(1)).Select(_ => currentTime).Where(_ => _ > 0).Subscribe(_ => {
            currentTime--;
            if (OnUpdate != null) OnUpdate.Invoke(currentTime);

            if (currentTime == 0)
            {
                subscribe.Dispose();
                if (OnOver != null) OnOver.Invoke();
            }
        });
    }

    public void TimerStop()
    {
        if (subscribe != null)
            subscribe.Dispose();
    }

}
