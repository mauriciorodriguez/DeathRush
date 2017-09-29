using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ClassTechnician : Classes
{
    public ClassTechnician()
    {
        Init(Type.Technician, TypeTierOne.BargainDriver, TypeTierOne.Miner,
            TypeTierTwo.Overclock, TypeTierTwo.Hacker,
            TypeTierThree.DamageControl, TypeTierThree.HyperCharge);
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
        v.canHyperCharge = true;
    }

    protected override void ExecuteTierTwoOne(Vehicle v)
    {
        v.vehicleVars.nitroTimer += (v.vehicleVars.nitroTimer * .15f);
    }

    protected override void ExecuteTierTwoTwo(Vehicle v)
    {
    }
}
