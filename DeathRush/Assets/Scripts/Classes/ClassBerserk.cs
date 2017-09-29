using UnityEngine;
using System.Collections;
using System;

public class ClassBerserk : Classes
{
    public ClassBerserk()
    {
        Init(Type.Berserk, TypeTierOne.BountyHunter, TypeTierOne.Though,
            TypeTierTwo.Bloodthirsty, TypeTierTwo.Crasher,
            TypeTierThree.AgentOfChaos, TypeTierThree.OnTheEdge);
    }

    public override void Execute(Vehicle v, TypeTierOne one, TypeTierTwo two, TypeTierThree three)
    {
        foreach (var weapon in v.weaponManager.GetComponentsInChildren<Weapon>())
        {
            weapon.damage += weapon.damage * CSVManager.instance.getData(K.CSV_PASSIVE_BERSERK, K.CSV_STAT_DAMAGE);
        }
        v.GetComponent<VehicleData>().maxLife += v.GetComponent<VehicleData>().maxLife * CSVManager.instance.getData(K.CSV_PASSIVE_BERSERK, K.CSV_STAT_RESISTANCE);
        v.GetComponent<VehicleData>().currentLife += v.GetComponent<VehicleData>().currentLife * CSVManager.instance.getData(K.CSV_PASSIVE_BERSERK, K.CSV_STAT_RESISTANCE);
        base.Execute(v, one, two, three);
    }

    protected override void ExecuteTierOneOne(Vehicle v)
    {
    }

    protected override void ExecuteTierOneTwo(Vehicle v)
    {
        v.GetComponent<VehicleData>().maxLife += ((v.GetComponent<VehicleData>().maxLife * CSVManager.instance.getData(K.CSV_PASSIVE_BERSERK, K.CSV_STAT_RESISTANCE)) * -1);
        v.GetComponent<VehicleData>().currentLife += ((v.GetComponent<VehicleData>().currentLife * CSVManager.instance.getData(K.CSV_PASSIVE_BERSERK, K.CSV_STAT_RESISTANCE)) * -1);
    }

    protected override void ExecuteTierThreeOne(Vehicle v)
    {
    }

    protected override void ExecuteTierThreeTwo(Vehicle v)
    {
        v.GetComponent<VehicleData>().canUseOnTheEdge = true;
    }

    protected override void ExecuteTierTwoOne(Vehicle v)
    {
    }

    protected override void ExecuteTierTwoTwo(Vehicle v)
    {
        v.isCrasher = true;
    }
}
