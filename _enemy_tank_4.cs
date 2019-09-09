using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _enemy_tank_4 : _enemy_tank_3
{

    protected float ThirdReloadTime;
    protected float ThirdReloadTimeCounter;


    protected override void Awake()
    {
        base.Awake();

        ThirdReloadTime = SecondReloadTime + 0.25f;
        ThirdReloadTimeCounter = 0f;
    }

    protected new void Shoot()
    {
        if (reloadTimeCounter <= 0f)
        {
            GameObject newbul = Instantiate(bull, transform.position, transform.rotation) as GameObject;
            newbul.transform.SetParent(ctrl.transform);
            reloadTimeCounter = reloadTime;
        }

        if (SecondReloadTimeCounter <= 0f)
        {
            GameObject newbul = Instantiate(bull, transform.position, transform.rotation) as GameObject;
            newbul.transform.SetParent(ctrl.transform);
            SecondReloadTimeCounter = SecondReloadTime;
        }

        if (ThirdReloadTimeCounter <= 0f)
        {
            GameObject newbul = Instantiate(bull, transform.position, transform.rotation) as GameObject;
            newbul.transform.SetParent(ctrl.transform);
            ThirdReloadTimeCounter = ThirdReloadTime;
        }
    }

    protected new void Update()
    {
        if (isShooting)
        {
            Shoot();
        }
        reloadTimeCounter -= Time.fixedDeltaTime;
        SecondReloadTimeCounter -= Time.fixedDeltaTime;
        ThirdReloadTimeCounter -= Time.fixedDeltaTime;
        Move();

    }
}



