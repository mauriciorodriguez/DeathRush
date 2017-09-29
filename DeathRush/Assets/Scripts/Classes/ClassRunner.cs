using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ClassRunner : Classes
{
    public ClassRunner()
    {
        Init(Type.Runner, TypeTierOne.HighStandars, TypeTierOne.ImprovedDesign,
            TypeTierTwo.Oportunist, TypeTierTwo.OnlyAScratch,
            TypeTierThree.Golden, TypeTierThree.NeedForNitro);
    }

    public override void Execute(Vehicle v, TypeTierOne one, TypeTierTwo two, TypeTierThree three)
    {
        v.vehicleVars.topSpeed += v.vehicleVars.topSpeed * CSVManager.instance.getData(K.CSV_PASSIVE_RUNNER, K.CSV_STAT_TOP_SPEED);
        v.vehicleVars.torque += v.vehicleVars.torque * CSVManager.instance.getData(K.CSV_PASSIVE_RUNNER, K.CSV_STAT_TOP_ACCELERATION);
        foreach (var weapon in v.weaponManager.GetComponentsInChildren<Weapon>())
        {
            weapon.damage += weapon.damage * CSVManager.instance.getData(K.CSV_PASSIVE_RUNNER, K.CSV_STAT_DAMAGE);
        }
        v.GetComponent<VehicleData>().maxLife += v.GetComponent<VehicleData>().maxLife * CSVManager.instance.getData(K.CSV_PASSIVE_RUNNER, K.CSV_STAT_RESISTANCE);
        v.GetComponent<VehicleData>().currentLife += v.GetComponent<VehicleData>().currentLife * CSVManager.instance.getData(K.CSV_PASSIVE_RUNNER, K.CSV_STAT_RESISTANCE);
        base.Execute(v, one, two, three);
    }

    protected override void ExecuteTierOneOne(Vehicle v)
    {
    }

    protected override void ExecuteTierOneTwo(Vehicle v)
    {
        foreach (var weapon in v.weaponManager.GetComponentsInChildren<Weapon>())
        {
            weapon.damage += ((weapon.damage * CSVManager.instance.getData(K.CSV_PASSIVE_RUNNER, K.CSV_STAT_DAMAGE)) * -1);
        }
        v.GetComponent<VehicleData>().maxLife += ((v.GetComponent<VehicleData>().maxLife * CSVManager.instance.getData(K.CSV_PASSIVE_RUNNER, K.CSV_STAT_RESISTANCE)) * -1);
        v.GetComponent<VehicleData>().currentLife += ((v.GetComponent<VehicleData>().currentLife * CSVManager.instance.getData(K.CSV_PASSIVE_RUNNER, K.CSV_STAT_RESISTANCE)) * -1);
    }

    protected override void ExecuteTierThreeOne(Vehicle v)
    {
    }

    protected override void ExecuteTierThreeTwo(Vehicle v)
    {
        v.vehicleVars.nitroTimer += (v.vehicleVars.nitroTimer * .3f);
    }

    protected override void ExecuteTierTwoOne(Vehicle v)
    {
    }

    protected override void ExecuteTierTwoTwo(Vehicle v)
    {
    }
}
