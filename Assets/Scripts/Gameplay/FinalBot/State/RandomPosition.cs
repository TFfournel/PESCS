using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition: Statee
{
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

    private Vector3 GetRandomPointOnBoxCollider(BoxCollider boxCollider)
    {
        Bounds bounds = boxCollider.bounds;

        float randomX = Random.Range(bounds.min.x,bounds.max.x);
        float randomY = Random.Range(bounds.min.y,bounds.max.y);
        float randomZ = Random.Range(bounds.min.z,bounds.max.z);

        return new Vector3(randomX,randomY,randomZ);
    }
}