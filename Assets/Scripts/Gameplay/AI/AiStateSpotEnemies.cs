using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateSpotEnemies: AiState
{
    private Dictionary<int,GameObject> enemyDictionnary = new Dictionary<int,GameObject>();

    protected override void Init(AiStateParameter pParam)
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
        List<AiStateManager> allFoundObject = ListExtension.LookForType<AiStateManager>(aiStateManager.aiValues.nearbyObjects);
        if(allFoundObject.Count == 0)
            return;
        DictionnaryExtensions.ListToDictionary<int,AiStateManager>();
        AiStateManager firstWeapon = allFoundObject[RandomExtension.RandomInRange(allFoundObject.Count)];
        aiStateManager.aiValues.pathfinding.SetTarget(firstWeapon.transform.position);
    }

    public override void Behaviour()
    {
        base.Behaviour();
    }
}