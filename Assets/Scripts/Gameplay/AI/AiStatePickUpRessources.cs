using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AiStatePickUpRessources: AiState
{
    private int weaponRemainingAmo;
    public float missingAmoMultiplier;
    public float gainingSpeed;

    protected override void Init(AiStateParameter pParam)
    {
        base.Init(pParam);
    }

    public override void Factor()
    {
        base.Factor();
        if(aiStateManager.aiValues.weapon is null)
        {
            changeToStateFactor = 1;
            return;
        }
        int lAmo = aiStateManager.aiValues.weapon.remainingBullet;
        changeToStateFactor += gainingSpeed * Time.deltaTime;
        changeToStateFactor += 1 * lAmo * missingAmoMultiplier;
    }

    protected override void SetChangeState()
    {
        base.SetChangeState();
        List<Weapon> allFoundObject = ListExtension.LookForType<Weapon>(aiStateManager.aiValues.nearbyObjects);
        if(allFoundObject.Count == 0)
            return;
        Weapon firstWeapon = allFoundObject[RandomExtension.RandomInRange(allFoundObject.Count)];
        aiStateManager.aiValues.pathfinding.SetTarget(firstWeapon.transform.position);
    }

    public override void Behaviour()
    {
        base.Behaviour();
    }
}