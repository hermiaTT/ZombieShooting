using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapons
{


    private void Update()
    {
        Fire();
        WeaponRotation();
    }


    public override void Fire()
    {
        base.Fire();
    }


    public override void WeaponRotation()
    {
        base.WeaponRotation();
    }
}
