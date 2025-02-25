/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackEnemy: AiState
{
    private AiStateManager enemyToAttack;
    private StateSpotEnemies _StateSpotEnemies;
    private Weapon weapon;
    public float gainingSpeed = 0.15f;

    protected override void Init(AiStateParameter pParam)
    {
        base.Init(pParam);

        // Get the first StateSpotEnemies instance (if available)
        var foundStates = aiStateManager.FindDerivedStates(typeof(StateSpotEnemies));

        if(foundStates.Count > 0)
        {
            _StateSpotEnemies = (StateSpotEnemies)foundStates[0];  // Cast to StateSpotEnemies
        }
        else
        {
            _StateSpotEnemies = null;  // No matching state found
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
        if(_StateSpotEnemies.nearEnemies < 0)
            return;
        enemyToAttack = _StateSpotEnemies.allEnemies[RandomExtension.RandomInRange(_StateSpotEnemies.nearEnemies)];
    }

    public override void Behaviour()
    {
        base.Behaviour();
    }
}*/