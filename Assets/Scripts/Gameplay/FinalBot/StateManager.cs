using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager: MonoBehaviour
{
    private List<Statee> states = new List<Statee>();
    private List<int> activeState = new List<int>();
    private List<Computor> computor = new List<Computor>();
    public BotStats botStats;

    private void Start()
    {
        InitializeAllState();
        botStats = GetComponent<BotStats>();
    }

    // Update is called once per frame
    private void Update()
    {
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

    private void InitializeAllState()
    {
        int lLength = states.Count;
        Statee lState;
        for(int i = 0 ; i < lLength ; i++)
        {
            lState = states[i];
            lState.Initialize(this);
        }
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