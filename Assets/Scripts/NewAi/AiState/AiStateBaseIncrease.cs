using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateBaseIncrease: State
{
    public float gainingSpeed = 0.2f;
    public float decreaseSpeed = 0.2f;

    public override void ComputeTurnOnState()
    {
        base.ComputeTurnOnState();
        factor += gainingSpeed * Time.deltaTime;
    }

    public override void ComputeTurnOffState()
    {
        base.ComputeTurnOffState();
        factor -= decreaseSpeed * Time.deltaTime;
    }
}