using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    None, CallEveryXTime
}

public class TimeUse: MonoBehaviour
{
    public TimeManager TimeManager;
    public bool activated;
    public State state;
    public float delay;
    public float apparitionTime;
    public EventHandler OnEventFired;
    public float timeScale;
    public Action MethodCall;
    public bool destroyAfterUse = false;

    // Start is called before the first frame update
    private void Start()
    {
        TimeManager = TimeManager.GetInstance();
        TimeManager.OnTimeScaleChange += OnTimeScaleChange;
    }

    public void FireEvent()
    {
        if(MethodCall != null)
            MethodCall();
        OnEventFired?.Invoke(this,new EventArgs());
        if(destroyAfterUse)
        {
            Destroy(this);
            return;
        }

        activated = false;
        if(state == State.CallEveryXTime)
        {
            SetActive();
        }
    }

    private void OnDestroy()
    {
        TimeManager.allActiveTimeUse.Remove(this);
    }

    private void OnTimeScaleChange(object pSender,EventArgs pEventArgs)
    {
        timeScale = TimeManager.GetTimeSpeed();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void SetActive(bool pActive = true)
    {
        if(pActive)
        {
            if(!activated)
                TimeManager.allActiveTimeUse.Add(this);
            apparitionTime = TimeManager.GetElapsedTime();
        }
        else
        {
            TimeManager.allActiveTimeUse.Remove(this);
        }
        activated = pActive;
    }

    public bool CheckIfActive()
    {
        return activated;
    }

    public static TimeUse AddTimeUse(GameObject pContainer,Action pMethodToCall,State pState,float pDelay,bool pAutoStart = true,bool pDestroyAfterUse = false)
    {
        TimeUse lTimeUse = pContainer.AddComponent<TimeUse>();
        lTimeUse.SetTimeUse(pState,pMethodToCall,pDelay);
        TimeManager.GetInstance().allActiveTimeUse.Add(lTimeUse);
        return lTimeUse;
    }

    public void SetTimeUse(State pState,Action pMethodToCall,float pDelay,bool pDestroyAfterUse = false)
    {
        MethodCall = pMethodToCall;
        state = pState;
        destroyAfterUse = pDestroyAfterUse;
        delay = pDelay;
    }
}