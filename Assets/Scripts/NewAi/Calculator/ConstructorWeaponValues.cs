using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructorWeaponValues: MonoBehaviour
{
    public Weapon weapon;

    public int GetRemainingMagAmo()
    {
        return weapon.remainingBullet;
    }

    public int GetRemainingTotalAmo()
    {
        return weapon.totalAmo;
    }
}