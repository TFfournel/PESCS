using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computor: MonoBehaviour
{
    public float UpdateFrequency = .5f;
    public float elapsedTime = 0;

    public virtual void compute()
    {
        if(elapsedTime <= UpdateFrequency)
        {
            return;
        }
    }
}