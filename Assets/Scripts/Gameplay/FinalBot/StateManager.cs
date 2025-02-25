using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager: MonoBehaviour
{
    private List<Statee> states = new List<Statee>();
    private List<int> activeState = new List<int>();

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        FactorCalculation();
        StateDoAction();
    }

    private void StateDoAction()
    {
        int lLength = activeState.Count;
        Statee lState;
        for(int i = lLength - 1 ; i >= 0 ; i--)
        {
            lState = states[activeState[i]];
            float lFactor = lState.FactorCalculation();
            lState.DoAction();
            if(lFactor <= 0)
            {
                activeState.Remove(i);
            }
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
                activeState.Add(i);
                lState.SetMode();
            }
        }
    }
}