using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

/*
 public override void Initialize(StateManager lManager)
{
base.Initialize(lManager);
}
  public override void SetMode()
   {
       base.SetMode();
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
*/

public class Statee: MonoBehaviour
{
    public StateManager stateManager;
    public float factor = 0;
    public bool resetFactorOnSetMode = true;

    public virtual void Initialize(StateManager lManager)
    {
        stateManager = lManager;
    }

    public virtual void SetMode()
    {
    }

    public virtual void DoAction()
    {
    }

    public virtual float FactorCalculation()
    {
        return factor;
    }
}