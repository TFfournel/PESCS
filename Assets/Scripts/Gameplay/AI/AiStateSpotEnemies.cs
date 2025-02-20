using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateSpotEnemies: AiState
{
    public List<AiStateManager> allEnemies = new List<AiStateManager>();
    public float gainingSpeed = .3f;
    public int nearEnemies = 0;

    protected override void Init(AiStateParameter pParam)
    {
        base.Init(pParam);
    }

    public override void Factor()
    {
        base.Factor();
        changeToStateFactor += gainingSpeed * Time.deltaTime;
    }

    protected override void SetChangeState()
    {
        base.SetChangeState();
        allEnemies = ListExtension.LookForType<AiStateManager>(aiStateManager.aiValues.nearbyObjects);
        nearEnemies = allEnemies.Count;
    }

    public override void Behaviour()
    {
        base.Behaviour();
    }
}