using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerialiAiStateGoToTargetParam: AiStateParameter
{
    public Vector3 target;
}

public class AiStateGoToTarget: AiState
{
    private Vector3 target;
    public bool activated = true;

    protected override void Init(AiStateParameter pTarget)
    {
        SerialiAiStateGoToTargetParam pParam = (SerialiAiStateGoToTargetParam)pTarget;
        target = pParam.target;
    }

    public override void Behaviour()
    {
        base.Behaviour();
    }

    protected virtual void SetChangeState()
    {
        //  aiStateManager.aiValues.bot.SetTarget(new Transform(target));
    }
}