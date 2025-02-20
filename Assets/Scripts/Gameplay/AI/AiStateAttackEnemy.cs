using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateAttackEnemy: AiState
{
    private AiStateManager enemyToAttack;
    private AiStateSpotEnemies AiStateSpotEnemies;
    private Weapon weapon;
    public float gainingSpeed = 0.15f;

    protected override void Init(AiStateParameter pParam)
    {
        base.Init(pParam);

        // Get the first AiStateSpotEnemies instance (if available)
        var foundStates = aiStateManager.FindDerivedStates(typeof(AiStateSpotEnemies));

        if(foundStates.Count > 0)
        {
            AiStateSpotEnemies = (AiStateSpotEnemies)foundStates[0];  // Cast to AiStateSpotEnemies
        }
        else
        {
            AiStateSpotEnemies = null;  // No matching state found
        }
    }

    public override void Factor()
    {
        base.Factor();
        changeToStateFactor += gainingSpeed * Time.deltaTime;
    }

    protected override void SetChangeState()
    {
        base.SetChangeState();
        weapon = aiStateManager.aiValues.weapon;
        if(AiStateSpotEnemies.nearEnemies < 0)
            return;
        enemyToAttack = AiStateSpotEnemies.allEnemies[RandomExtension.RandomInRange(AiStateSpotEnemies.nearEnemies)];
    }

    public override void Behaviour()
    {
        base.Behaviour();
    }
}