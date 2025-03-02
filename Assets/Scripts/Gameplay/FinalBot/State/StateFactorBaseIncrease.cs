using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFactorBaseIncrease: Statee
{
    public float baseIncrease = 100;

    public override void Initialize(StateManager lManager)
    {
        base.Initialize(lManager);
    }

    public override void SetMode()
    {
        base.SetMode();
        factor = 1;
    }

    public override void DoAction()
    {
        base.DoAction();
    }

    public override float FactorCalculation()
    {
        base.FactorCalculation();
        factor += baseIncrease * Time.deltaTime;
        Debug.Log(name + "factor" + factor);

        return factor;
    }
}