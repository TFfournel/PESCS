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

/*protected override void Init(AiStateParameter pParam)
 {
     base.Init(pParam);
 }

 public override void Factor()
 {
     base.Factor();
 }

 protected override void SetChangeState()
 {
     base.SetChangeState();
 }

 public override void Behaviour()
 {
     base.Behaviour();
 }*/

public class AiState: MonoBehaviour
{
    public float changeToStateFactor;
    private Action behaviourAction = null;
    private Action ChangeToStateFactorCompute = null;
    public AiStateManager aiStateManager;
    public bool stateActive = false;
    public EventHandler ChangeToState;
    public float targetValueToChangeToState = 1;
    public bool stopCalculatingFactorWhenActive = true;

    protected virtual void Init(AiStateParameter pParam)
    {
        ChangeToStateFactorCompute = Factor;
    }

    public virtual void Factor()
    {
        if(ChangeToStateFactorCompute != null)
            ChangeToStateFactorCompute();
        if(changeToStateFactor >= targetValueToChangeToState)
        {
            stateActive = true;
            ChangeToState?.Invoke(this,new EventArgs());
            SetChangeState();
        }
    }

    protected virtual void SetChangeState()
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