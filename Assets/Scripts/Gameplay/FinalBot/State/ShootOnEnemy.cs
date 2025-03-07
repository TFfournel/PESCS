using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootOnEnemy: Statee
{
    private AISensor aiSensor;

    private GameObject closestEnemy;

    // Start is called before the first frame update
    private void Start()
    {
        aiSensor = GetComponent<AISensor>();
    }

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
        closestEnemy = GetClosestEnemy();
        if(closestEnemy is null)
            return;
        stateManager.botStats.currentWeapon.ShootOnPosRequest(closestEnemy.transform.position);
        stateManager.botStats.agent.isStopped = true; // Stop the agent
        stateManager.botStats.agent.ResetPath(); // Clear the current path
    }

    public override float FactorCalculation()
    {
        base.FactorCalculation();
        factor = 1;
        return factor;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private GameObject GetClosestEnemy()
    {
        float lClosestDistance = -1;
        GameObject lClosestEnemy = null;
        float lDist;
        foreach(GameObject lEnemy in aiSensor._Objects)
        {
            lDist = VectorExtensions.Direction(transform.position,lEnemy.transform.position).magnitude;

            if(lClosestDistance < 0 || lDist < lClosestDistance)
            {
                lClosestDistance = lDist;
                lClosestEnemy = lEnemy;
            }
        }

        return lClosestEnemy;
    }
}