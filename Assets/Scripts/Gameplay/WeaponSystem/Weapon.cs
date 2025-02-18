using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon: MonoBehaviour
{
    /// <summary>
    /// parameters
    /// </summary>
    public GameObject bulletPrefab;

    public int totalAmo = 100;
    public int burstBulletAmount = 1;

    public float burstDelay = 1f;
    public float intraBurstBulletDelay;

    public float reloadTime;
    public int magSize;
    public bool autoReload = true;

    /// <summary>
    /// values
    /// </summary>
    private TimeUse timer;

    private bool reloading = false;
    private int remainingBullet;
    private float burstModulo; //set to 0 everytime the delay needs to be burstDelay
    private int countSinceLastBurst;

    private float intraBurstBulletDelayValue
    {
        get => burstBulletAmount == 1 ? burstDelay : intraBurstBulletDelay;
    }    // Start is called before the first frame update

    private void Relaod()
    {
        reloading = true;
        //timer.SetTimeUse(State.CallOnce,EndingReload,ComputeDelay(),true);
    }

    private void EndingReload()
    {
        reloading = false;
        remainingBullet = magSize > totalAmo ? totalAmo : magSize;
    }

    public void ShootRequest()
    {
        if(timer.CheckIfActive())
            return;
        Shoot();
    }

    private void Shoot()
    {
        SpawnBullet(bulletPrefab,transform.position,transform.forward);
        CountingRemainingBullet();
    }

    private void CountingRemainingBullet()
    {
        if(remainingBullet <= 1)
        {
            AutoReloadCheck();
            return;
        }
        countSinceLastBurst++;

        remainingBullet--;
    }

    private bool AutoReloadCheck()
    {
        if(autoReload)
        {
            Relaod();
            return true;
        }

        return false;
    }

    private float ComputeDelay()
    {
        if(countSinceLastBurst % burstBulletAmount == 0)
        {
            countSinceLastBurst = 0;
            return burstDelay;
        }

        return intraBurstBulletDelayValue;
    }

    private void Start()
    {
        //TimeUse timer = TimeUse.AddTimeUse(gameObject,Shoot,State.CallOnce,ComputeDelay(),false);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private GameObject SpawnBullet(GameObject pBulletPrefab,Vector3 pPos,Vector3 pOrientation,Transform pParent = null)
    {
        Quaternion lOrientation = Quaternion.LookRotation(pOrientation);
        GameObject lBullet;
        if(pParent != null)
            lBullet = Instantiate(pBulletPrefab,pParent);
        lBullet = Instantiate(pBulletPrefab);

        lBullet.transform.position = pPos;
        lBullet.transform.rotation = lOrientation;
        return lBullet;
    }
}