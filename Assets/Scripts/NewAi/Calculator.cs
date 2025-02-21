using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator: MonoBehaviour
{
    public float updateFrequency = 1;
    public bool activated = true;

    private IEnumerator UpdateCoroutine()
    {
        while(activated)
        {
            Compute();
            yield return new WaitForSeconds(updateFrequency);
        }
    }

    public void ChangeCalculatorState(bool pActivated)
    {
        activated = pActivated;
        if(activated)
        {
            Initialized();
        }
    }

    public void Initialized()
    {
        StartCoroutine(UpdateCoroutine());
    }

    public void Compute()
    {
    }

    protected virtual void UpdateInfo()
    {
    }
}