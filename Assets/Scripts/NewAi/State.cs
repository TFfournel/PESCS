using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateChangeParam: EventArgs
{
    public State state;
    public bool active;
}

public class State: MonoBehaviour
{
    //[Serializable] public class
    public int index;

    public List<string> tag;
    public List<string> basePossibleStates;
    public List<string> currentlyPossibleStates;
    public Dictionary<string,float> tagsFactorMultiplier;
    public float factor;
    public EventHandler onChangeState;
    public OnStateChangeParam onChangeStateParam = new OnStateChangeParam();
    protected BehaviourTree behaviourTree;

    public void SetBehaviourTree(BehaviourTree pTree)
    {
        behaviourTree = pTree;
    }

    public void SetOtherValues(List<string> tags,List<float> values)
    {
    }

    public void ToggleOffStates(List<string> tags)
    {
        foreach(string VARIABLE in tags)
        {
        }
    }

    public void SetStateOff()
    {
    }

    public void ToggleOffAllForbidenState()
    {
    }

    public void SetupOtherRequiredState()
    {
    }

    public void SetupRequiredCalculator()
    {
    }

    public void SetOtherTagValues()
    {
    }

    public void MultiplyOtherStateIncrease()
    {
    }

    public virtual void OnStateChange()
    {
        onChangeStateParam.state = this;
        onChangeState?.Invoke(this,onChangeStateParam);
        Debug.Log("change state " + onChangeStateParam.active);
    }

    /*public State GetStateFromName(List<State> pList)
    {
        State LState;
        foreach(State VARIABLE in pList)
        {
        }
    }*/

    public virtual void Behaviour()
    {
        Debug.Log("behaviour tree" + behaviourTree == null);
    }

    public virtual void ComputeTurnOnState()
    {
        factor = Math.Clamp(factor,0,1);
        Debug.Log(GetType().ToString() + "factor" + factor);

        if(factor >= 1)
        {
            onChangeStateParam.active = true;
            OnStateChange();
        }
    }

    public virtual void ComputeTurnOffState()
    {
        factor = Math.Clamp(factor,0,1);
        Debug.Log(GetType().ToString() + "factor" + factor);

        if(factor <= 0)
        {
            onChangeStateParam.active = false;
            OnStateChange();
        }
    }
}