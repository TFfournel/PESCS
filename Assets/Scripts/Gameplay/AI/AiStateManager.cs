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