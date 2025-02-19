using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AiStateParameter
{
}

public class ChangeStateEventParam: EventArgs
{
}

public class AiState
{
    public float changeToStateFactor;
    public Action behaviourAction = null;
    public Action ChangeToStateFactorCompute = null;
    public AiStateManager aiStateManager;
    public bool stateActive = false;
    public EventHandler ChangeToState;

    protected virtual void Init(AiStateParameter pParam)
    {
        ChangeToState += SetChangeState;
    }

    protected virtual void Factor()
    {
        if(ChangeToStateFactorCompute != null)
            ChangeToStateFactorCompute();
        if(changeToStateFactor == 1)
        {
            stateActive = true;
            ChangeToState?.Invoke(this,new EventArgs());
        }
    }

    protected virtual void SetChangeState(object pSender,EventArgs pArgs)
    {
        stateActive = true;
    }

    protected virtual float StateChangeCompute()
    {
        return changeToStateFactor;
    }

    public virtual void CustomStart()
    {
    }

    public virtual void Behaviour()
    {
        if(behaviourAction != null)

            behaviourAction();
    }
}