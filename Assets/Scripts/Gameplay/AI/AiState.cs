using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AiStateParameter
{
}

public class AiState
{
    public float changeToStateFactor;
    public Action behaviourAction = null;
    public Action ChangeToStateFactorCompute = null;
    public AiStateManager aiStateManager;

    protected virtual void Init(AiStateParameter pParam)
    {
    }

    protected virtual void Factor()
    {
        if(ChangeToStateFactorCompute != null)
            ChangeToStateFactorCompute();
    }

    protected virtual void SetChangeState()
    {
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