using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ShootOnPos: Statee
{
    public Vector3 targetPos;

    public override void Initialize(StateManager lManager)
    {
        base.Initialize(lManager);
    }

    public override void SetMode()
    {
        base.SetMode();
        stateManager.botStats.currentWeapon.ShootOnPosRequest(targetPos);
    }

    public override void DoAction()
    {
        base.DoAction();
    }

    public override float FactorCalculation()
    {
        base.FactorCalculation();
        return factor;
    }
}