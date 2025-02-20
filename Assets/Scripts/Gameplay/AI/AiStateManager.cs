using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateManager: MonoBehaviour
{
    public List<AiState> aiStates = new List<AiState>();
    private List<AiState> AiStateCurrent = new List<AiState>();
    public AiValues aiValues;

    // Start is called before the first frame update
    private void Start()
    {
        InitializeEachState(aiStates);
    }

    // Update is called once per frame
    private void Update()
    {
        ComputeEachAiStateFactor(aiStates);
        ComputeEachAiStateBehaviour(AiStateCurrent);
    }

    private void InitializeEachState(List<AiState> pAllStates)
    {
        int lLength = pAllStates.Count;
        AiState lState;
        for(int i = 0 ; (i) < lLength ; i++)
        {
            lState = pAllStates[i];
            lState.aiStateManager = this;
            lState.CustomStart();
        }
    }

    public List<AiState> FindDerivedStates(Type targetType)
    {
        // Ensure the targetType is a subclass of AiState
        if(!typeof(AiState).IsAssignableFrom(targetType))
        {
            throw new ArgumentException($"{targetType.Name} is not a subclass of AiState.");
        }

        var foundStates = new List<AiState>();

        foreach(var state in aiStates)
        {
            // Check if the object can be cast to the desired type
            if(state != null && targetType.IsInstanceOfType(state))
            {
                foundStates.Add(state);
            }
        }

        return foundStates;
    }

    private void ComputeEachAiStateFactor(List<AiState> pAllStates)
    {
        int lLength = pAllStates.Count;
        AiState lState;
        for(int i = 0 ; (i) < lLength ; i++)
        {
            lState = pAllStates[i];

            lState.Factor();
        }
    }

    private void ComputeEachAiStateBehaviour(List<AiState> pAllStates)
    {
        int lLength = pAllStates.Count;
        AiState lState;
        for(int i = 0 ; (i) < lLength ; i++)
        {
            lState = pAllStates[i];
            if(!lState.stateActive)
                return;
            lState.Behaviour();
        }
    }
}