using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _enemy_tank_3 : _enemy_tank_2
{

    public GameObject SupBull;


    private void SuperShoot()
    {
        if (reloadTimeCounter <= 0f)
        {
            GameObject newbul = Instantiate(SupBull, transform.position, transform.rotation) as GameObject;
            newbul.transform.SetParent(ctrl.transform);
            reloadTimeCounter = reloadTime;
        }
    }

    protected override void Update()
    {
        if (isShooting)
        {
            SuperShoot();
        }
        reloadTimeCounter -= Time.fixedDeltaTime;
        Move();
    }
}

