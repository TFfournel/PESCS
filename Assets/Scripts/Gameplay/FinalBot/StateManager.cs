using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager: MonoBehaviour
{
    public List<GameObject> statesBase = new List<GameObject>(); // List of GameObjects with Statee components
    public List<Statee> states = new List<Statee>(); // List to store new instances
    private List<int> activeState = new List<int>();
    private List<Computor> computor = new List<Computor>();
    public BotStats botStats;

    private void Start()
    {
        botStats = GetComponent<BotStats>();
        Initialize();
    }

    // Update is called once per frame
    private void Update()
    {
        CustomUpdate();
    }

    public T FindInstanceOfClass<T>() where T : Statee
    {
        // Iterate through the list and check if the object is of type T
        foreach(var state in states)
        {
            if(state is T)
            {
                return (T)state; // Cast and return the found instance
            }
        }
        return null; // Return null if no instance of the target class is found
    }

    private void CloneGameObjectAndGetStateComponent()
    {
        foreach(GameObject baseState in statesBase)
        {
            GameObject newObj = Instantiate(baseState,transform); // Clone the GameObject
            Statee[] newStates = newObj.GetComponents<Statee>(); // Get all Statee components
            Computor[] newStatesComputor = newObj.GetComponents<Computor>(); // Get all Statee components

            foreach(Statee state in newStates)
            {
                states.Add(state); // Add each component to the list
            }
            foreach(Computor state in newStatesComputor)
            {
                computor.Add(state); // Add each component to the list
            }
        }
    }

    private void Initialize()
    {
        CloneGameObjectAndGetStateComponent();
        AllStateInitialize();
    }

    private void CustomUpdate()
    {
        ComputeAll();
        FactorCalculation();
        StateDoAction();
    }

    private void ComputeAll()
    {
        int lLength = computor.Count;

        Computor lComputor;
        for(int i = 0 ; i < lLength ; i++)
        {
            lComputor = computor[i];
            lComputor.compute();
        }
    }

    private void AllStateInitialize()
    {
        int lLength = states.Count;
        Statee lState;
        for(int i = 0 ; i < lLength ; i++)
        {
            lState = states[i];
            lState.Initialize(this);
            lState.stateManager = this;
        }
    }

    private void StateDoAction()

    {
        int lLength = activeState.Count;
        Statee lState;
        for(int i = lLength - 1 ; i >= 0 ; i--)
        {
            lState = states[activeState[i]];
            lState.DoAction();
        }
    }

    private void FactorCalculation()
    {
        int lLength = states.Count;
        Statee lState;

        for(int i = 0 ; i < lLength ; i++)
        {
            lState = states[i];
            float lFactor = lState.FactorCalculation();
            if(lFactor >= 1)
            {
                if(activeState.FindIndex(value => value == i) == -1)
                {
                    activeState.Add(i);
                }
                lState.SetMode();
                if(lState.resetFactorOnSetMode)
                {
                    lState.factor = 0.00001f;
                    return;
                }
            }
            else if(lFactor <= 0)
            {
                activeState.Remove(i);
            }

            //  Debug.Log(lState.name + "factor " + lState.factor);
        }
    }
}