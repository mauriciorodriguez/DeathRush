using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ClassSuperstar : Classes
{
    public ClassSuperstar()
    {
        Init(Type.Superstar, TypeTierOne.LastHope, TypeTierOne.Martir,
            TypeTierTwo.Lucky, TypeTierTwo.Gifted,
            TypeTierThree.PeaceMaker, TypeTierThree.TooGood);
    }

    public override void Execute(Vehicle v, TypeTierOne one, TypeTierTwo two, TypeTierThree three)
    {
        base.Execute(v, one, two, three);
    }

    protected override void ExecuteTierOneOne(Vehicle v)
    {
    }

    protected override void ExecuteTierOneTwo(Vehicle v)
    {
    }

    protected override void ExecuteTierThreeOne(Vehicle v)
    {
    }

    protected override void ExecuteTierThreeTwo(Vehicle v)
    {
    }

    protected override void ExecuteTierTwoOne(Vehicle v)
    {
    }

    protected override void ExecuteTierTwoTwo(Vehicle v)
    {
        if (UnityEngine.Random.value <= .25f)
        {
            v.vehicleVars.topSpeed += v.vehicleVars.topSpeed * .25f;
            v.vehicleVars.nitroTimer += v.vehicleVars.nitroTimer * .25f;
            foreach (var weapon in v.weaponManager.GetComponentsInChildren<Weapon>())
            {
                weapon.damage += weapon.damage * .25f;
            }
        }
    }
}
