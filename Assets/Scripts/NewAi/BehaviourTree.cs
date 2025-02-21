using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourTree: MonoBehaviour
{
    // Start is called before the first frame update
    public List<State> states = new List<State>();

    public List<int> activeStates = new List<int>();
    public List<int> currentState = new List<int>();

    // Start is called before the first frame update
    public List<Calculator> calculator = new List<Calculator>();

    public List<int> activeCalculator = new List<int>();
    public TrackedValues trackedValues;

    private void Start()
    {
        InitiliazeState();
        if(GetComponent<TrackedValues>() == null)
            trackedValues = gameObject.AddComponent<TrackedValues>();
    }

    // Update is called once per frame
    private void Update()
    {
        ComputeStates();
        Debug.Log("transform" + transform.position);
    }

    private void ComputeCalculator()
    {
        int lLength = activeCalculator.Count;
        Calculator lCalculator;
        for(int i = 0 ; i < lLength ; i++)
        {
            lCalculator = calculator[activeCalculator[i]];
            lCalculator.Compute();
        }
    }

    private void InitializeCalculator()
    {
        int lLength = calculator.Count;
        Calculator lCalculator;
        for(int i = 0 ; i < lLength ; i++)
        {
            lCalculator = calculator[i];
            activeCalculator.Add(i);
        }
    }

    private void ComputeStates()
    {
        int lLength = activeStates.Count;
        State lState;
        int activeStatesIndex;
        int currentStateIndex;
        int activeState;
        //loop over all active states = the one to compute
        for(int i = 0 ; i < lLength ; i++)
        {
            activeStatesIndex = activeStates[i]; //Get the State class for each activatedStateIndex
            lState = states[activeStatesIndex]; //get the state from state list
            currentStateIndex = currentState.Find(value => value == activeStatesIndex);
            Debug.Log("currentStateIndex" + currentStateIndex);
            Debug.Log("currentStateIndex1" + currentState.Count);
            if(currentStateIndex < 0 || currentState.Count == 0)
            {
                lState.ComputeTurnOnState();
            }
            else if(currentStateIndex >= 0 && currentState.Count != 0)
            {
                lState.ComputeTurnOffState();
            }
            if(currentStateIndex < 0 || currentState.Count == 0)
                return;
            lState.Behaviour();
        }
    }

    private void ONStateChange(object pSender,EventArgs pEvent)
    {
        OnStateChangeParam lParam = (OnStateChangeParam)pEvent;
        int lActiveIndex = activeStates.Find(value => value == lParam.state.index);
        if(lActiveIndex < 0)
            return;
        if(lParam.active)
        {
            int pt = states.FindIndex(value => value == lParam.state);
            if(currentState.Find(value => value == pt) < 0 || currentState.Count == 0)
                currentState.Add(lParam.state.index);
        }
        else
        {
            currentState.Remove(lParam.state.index);
        }
    }

    /// <summary>
    /// add subscriber to on change state
    /// add state to active list
    /// set state behaviour tree value to this
    /// </summary>
    private void InitiliazeState()
    {
        int lLength = states.Count;
        State lState;
        for(int i = 0 ; i < lLength ; i++)
        {
            lState = states[i];
            lState.onChangeState += ONStateChange;
            lState.index = i;
            activeStates.Add(i);
            lState.SetBehaviourTree((BehaviourTree)this);
            ;
        }
    }
}