using UnityEngine;
using System.Collections;
using System;

public class ClassSoldier : Classes
{
    public ClassSoldier()
    {
        Init(Type.Soldier, TypeTierOne.LiveForAnotherDay, TypeTierOne.Balanced,
            TypeTierTwo.Survivor, TypeTierTwo.TacticalVision,
            TypeTierThree.Fenix, TypeTierThree.MilitaryTraining);
    }

    public override void Execute(Vehicle v, TypeTierOne one, TypeTierTwo two, TypeTierThree three)
    {
        v.GetComponent<VehicleData>().maxLife += v.GetComponent<VehicleData>().maxLife * CSVManager.instance.getData(K.CSV_PASSIVE_SOLDIER, K.CSV_STAT_RESISTANCE);
        v.GetComponent<VehicleData>().currentLife += v.GetComponent<VehicleData>().currentLife * CSVManager.instance.getData(K.CSV_PASSIVE_SOLDIER, K.CSV_STAT_RESISTANCE);
        v.vehicleVars.topSpeed += v.vehicleVars.topSpeed * CSVManager.instance.getData(K.CSV_PASSIVE_SOLDIER, K.CSV_STAT_TOP_SPEED);
        v.vehicleVars.nitroTimer += v.vehicleVars.nitroTimer * CSVManager.instance.getData(K.CSV_PASSIVE_SOLDIER, K.CSV_STAT_TURBO);
        base.Execute(v, one, two, three);
    }

    protected override void ExecuteTierOneOne(Vehicle v)
    {
    }

    protected override void ExecuteTierOneTwo(Vehicle v)
    {
        v.vehicleVars.topSpeed += v.vehicleVars.topSpeed * (CSVManager.instance.getData(K.CSV_PASSIVE_SOLDIER, K.CSV_STAT_TOP_SPEED) * -1);
        v.vehicleVars.nitroTimer += v.vehicleVars.nitroTimer * (CSVManager.instance.getData(K.CSV_PASSIVE_SOLDIER, K.CSV_STAT_TURBO) * -1);
    }

    protected override void ExecuteTierThreeOne(Vehicle v)
    {
        v.GetComponent<VehicleData>().canUseFenix = true;
    }

    protected override void ExecuteTierThreeTwo(Vehicle v)
    {
        foreach (var weapon in v.weaponManager.GetComponentsInChildren<Weapon>())
        {
            weapon.reloadSpeed += ((weapon.reloadSpeed * .5f) * -1);
            weapon.ammoTimer += ((weapon.ammoTimer * .5f) * -1);
        }
    }

    protected override void ExecuteTierTwoOne(Vehicle v)
    {
    }

    protected override void ExecuteTierTwoTwo(Vehicle v)
    {
        foreach (var weapon in v.weaponManager.GetComponentsInChildren<Weapon>())
        {
            weapon.reach += (weapon.reach * .5f);
        }
    }
}
