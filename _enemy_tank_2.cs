using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class _enemy_tank_2 : _enemy_tank_1
{

    protected float SecondReloadTime;
    protected float SecondReloadTimeCounter;


    protected override void Awake()
    {
        base.Awake();
        SecondReloadTime = reloadTime + 0.2f;
        SecondReloadTimeCounter = 0f;
    }

    protected override void Shoot()
    {
        base.Shoot();

        if (SecondReloadTimeCounter <= 0f)
        {
            GameObject newbul = Instantiate(bull, transform.position, transform.rotation) as GameObject;
            newbul.transform.SetParent(ctrl.transform);
            SecondReloadTimeCounter = SecondReloadTime;
        }
    }

    protected override void Update()
    {
        base.Update();

        SecondReloadTimeCounter -= Time.fixedDeltaTime;
    }
}

