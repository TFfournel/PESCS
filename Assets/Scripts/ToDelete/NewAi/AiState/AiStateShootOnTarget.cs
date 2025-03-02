using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AiStateShootOnTarget: AiStateBaseIncrease
{
    public Vector3 target;

    public override void OnStateChange()
    {
        base.OnStateChange();
        Debug.Log("couunt" + behaviourTree.trackedValues.GetXComponent<CalculatorVision>().spotedObjects.Count);
        target = behaviourTree.trackedValues.GetXComponent<CalculatorVision>().spotedObjects[RandomExtension.RandomInRange(behaviourTree.trackedValues.GetXComponent<CalculatorVision>().spotedObjects.Count)].transform.position;

        /*List<GameObject> lObjects =
        if(onChangeStateParam.active)
        {
            behaviourTree.trackedValues.GetXComponent<Weapon>().ShootOnPosRequest(target);
        }*/
        behaviourTree.trackedValues.weapon.ShootOnPosRequest(target);
    }
}