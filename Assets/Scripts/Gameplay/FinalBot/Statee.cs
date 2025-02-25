using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statee: MonoBehaviour
{
    public float factor = 0;

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