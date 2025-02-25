using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

/*[Serializable]
public class BulletParameter()
{
}*/

public class Bullet: Moving
{
    public float damage;

    public Bullet(float damage)
    {
        this.damage = damage;
    }

    protected virtual void Init(Vector3 pDirection,float pSpeed,float pDamage)
    {
        base.Init(pDirection,pSpeed,true);
        damage = pDamage;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Init(transform.forward,speed,damage);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(GetVelocity() != Vector3.zero)
            Debug.Log("bullet velocity" + GetVelocity());
    }
}