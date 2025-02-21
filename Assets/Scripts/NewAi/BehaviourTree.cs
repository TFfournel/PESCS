using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourTree: MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> statesFactory = new List<GameObject>();

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
        InitializeCalculator();
        if(GetComponent<TrackedValues>() == null)
            trackedValues = gameObject.AddComponent<TrackedValues>();
    }

    // Update is called once per frame
    private void Update()
    {
        ComputeStates();
        ComputeCalculator();
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

    public void AddMissingStates(GameObject go,List<State> stateList)
    {
        // Get all State components attached to the GameObject.
        State[] components = go.GetComponents<State>();

        foreach(State state in components)
        {
            // Check if the list already contains a state of this type.
            if(!stateList.Any(s => s.GetType() == state.GetType()))
            {
                // Add a new component of the same type to the GameObject.
                State newState = (State)go.AddComponent(state.GetType());
                // Copy all fields from the found state to the new one.
                CopyAllFields(state,newState);
                // Add the new state to the list.
                stateList.Add(newState);
            }
        }
    }

    private void CopyAllFields(object source,object destination)
    {
        if(source == null || destination == null)
            return;
        if(source.GetType() != destination.GetType())
            return;

        Type type = source.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach(FieldInfo field in fields)
        {
            object value = field.GetValue(source);
            field.SetValue(destination,value);
        }
    }

    /// <summary>
    /// add subscriber to on change state
    /// add state to active list
    /// set state behaviour tree value to this
    /// </summary>
    private void InitiliazeState()
    {
        /*int lLength = statesFactory.Count;
        for(int i = 0 ; i < lLength ; i++)
        {
            states.Add(AddMissingStates(statesFactory[i],states));
        }*/

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

    /*public State Clone()
    {
        State newState = gameObject.AddComponent<State>(); // Create a new instance in Unity
        newState.value = this.value;
        newState.name = this.name;
        return newState;
    }*/
}