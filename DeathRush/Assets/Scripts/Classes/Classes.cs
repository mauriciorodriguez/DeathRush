using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Classes
{
    public static Dictionary<Enum, string> descriptions = new Dictionary<Enum, string>()
    {
        {Type.Soldier, "Incress Damage and Resistance,\nReduce the Max Speed and Turbo" },
        {Type.Runner, "Incress the Max. Speed and Acceleration,\nDecress damage and resistances" },
        {Type.Berserk, "Incresss Damage, reduce Resistance" },
        {Type.Technician, "20% discount buying weapons and gadgets" },
        {Type.Superstar, "Winning races reduce 25% extra Chaos\nIf this pilot dies you gain\n20% more chaos than usual" },
        {TypeTierOne.LiveForAnotherDay, "Each race this pilot survives you earn 10% extra credits" },
        {TypeTierOne.Balanced, "Losses all penalizations to Max. Speed and Turbo" },
        {TypeTierOne.BountyHunter, "Double credits for killing oponents during the race" },
        {TypeTierOne.Though, "This pilot no longer lose Resistance" },
        {TypeTierOne.BargainDriver, "The cost of weapons and gadgets reduce in a 40% of their original cost" },
        {TypeTierOne.Miner, "Absorbs 75% of the damage inflicted by mines" },
        {TypeTierOne.HighStandars, "If you end up first on a race you obtain 25% more credits for it" },
        {TypeTierOne.ImprovedDesign, "No longer you have less Damage and less Resistance" },
        {TypeTierOne.LastHope, "There is a slim chance of surviving the destruction of your vehicle. \nIf that's the case, you get a reduction of 20 chaos points" },
        {TypeTierOne.Martir, "In death not only you no longer gain Chaos, you lose a little" },
        {TypeTierTwo.Survivor, "50% chances of surviving the destruction of your vehicle" },
        {TypeTierTwo.TacticalVision, "Incress 50% the reach of your weapons" },
        {TypeTierTwo.Bloodthirsty, "Destroying other vehicle restores 25% of your own health" },
        {TypeTierTwo.Crasher, "You inflict extra damage to any enemy you crash" },
        {TypeTierTwo.Overclock, "The turbo last 15% more" },
        {TypeTierTwo.Hacker, "Combat drones ignore you and Electromagnetic Mines no longer affects you" },
        {TypeTierTwo.Oportunist, "Ramps and boosters gives you more speed than usual" },
        {TypeTierTwo.OnlyAScratch, "No weapons or effects can slow you down" },
        {TypeTierTwo.Lucky, "30% of ignoring any incoming damage" },
        {TypeTierTwo.Gifted, "At the start of the race there is 25% chance of activiting this skill.\nIf that's the case, you get a boost on the Turbo, Max Speed,\nAcceleration and Damage" },
        {TypeTierThree.Fenix, "The first time in the race your health reachs 0 you regain 30% of your resistance" },
        {TypeTierThree.MilitaryTraining, "Incress the reload speed of your weapons in a 50%" },
        {TypeTierThree.AgentOfChaos, "Everytime you kill another pilot the chaos inflicted on their faction is double" },
        {TypeTierThree.OnTheEdge, "When you have 30% of your health or less you damage doubles" },
        {TypeTierThree.DamageControl, "If your health reachs 0 you get 30 seconds before exploding. \nYou can regaing health during this period" },
        {TypeTierThree.HyperCharge, "All chargers now repairs your vehicle AND recharge your turbo" },
        {TypeTierThree.Golden, "If you get first place you have a 50% chance of getting the double of credits" },
        {TypeTierThree.NeedForNitro, "Incress the turbo in a 30%" },
        {TypeTierThree.PeaceMaker, "The extra chaos reductions becomes 40% instead of 25%" },
        {TypeTierThree.TooGood, "Each time you get first place all other factions gain 5 points of chaos" }
    };

    public enum Type
    {
        Soldier,
        Berserk,
        Technician,
        Runner,
        Superstar
    }

    public enum TypeTierOne
    {
        None,
        LiveForAnotherDay,
        Balanced,
        BountyHunter,
        Though,
        BargainDriver,
        Miner,
        HighStandars,
        ImprovedDesign,
        LastHope,
        Martir
    }

    public enum TypeTierTwo
    {
        None,
        Survivor,
        TacticalVision,
        Bloodthirsty,
        Crasher,
        Overclock,
        Hacker,
        Oportunist,
        OnlyAScratch,
        Lucky,
        Gifted
    }

    public enum TypeTierThree
    {
        None,
        Fenix,
        MilitaryTraining,
        AgentOfChaos,
        OnTheEdge,
        DamageControl,
        HyperCharge,
        Golden,
        NeedForNitro,
        PeaceMaker,
        TooGood
    }

    public Type classType;
    public List<Enum> skillList = new List<Enum>();
    public Dictionary<TypeTierOne, Action<Vehicle>> tierOneFunction;
    public Dictionary<TypeTierTwo, Action<Vehicle>> tierTwoFunction;
    public Dictionary<TypeTierThree, Action<Vehicle>> tierThreeFunction;

    public void Init(Type cType, TypeTierOne oneOne, TypeTierOne oneTwo,
        TypeTierTwo twoOne, TypeTierTwo twoTwo,
        TypeTierThree threeOne, TypeTierThree threeTwo)
    {
        classType = cType;
        skillList.AddRange(new List<Enum>() { oneOne, oneTwo, twoOne, twoTwo, threeOne, threeTwo });
        tierOneFunction = new Dictionary<TypeTierOne, Action<Vehicle>>();
        tierTwoFunction = new Dictionary<TypeTierTwo, Action<Vehicle>>();
        tierThreeFunction = new Dictionary<TypeTierThree, Action<Vehicle>>();
        InitTierOne(oneOne, oneTwo);
        InitTierTwo(twoOne, twoTwo);
        InitTierThree(threeOne, threeTwo);
    }

    private void InitTierOne(TypeTierOne one, TypeTierOne two)
    {
        tierOneFunction[one] = ExecuteTierOneOne;
        tierOneFunction[two] = ExecuteTierOneTwo;
        tierOneFunction[TypeTierOne.None] = DoNothing;
    }

    private void InitTierTwo(TypeTierTwo one, TypeTierTwo two)
    {
        tierTwoFunction[one] = ExecuteTierTwoOne;
        tierTwoFunction[two] = ExecuteTierTwoTwo;
        tierTwoFunction[TypeTierTwo.None] = DoNothing;
    }

    private void InitTierThree(TypeTierThree one, TypeTierThree two)
    {
        tierThreeFunction[one] = ExecuteTierThreeOne;
        tierThreeFunction[two] = ExecuteTierThreeTwo;
        tierThreeFunction[TypeTierThree.None] = DoNothing;
    }

    public virtual void Execute(Vehicle v, TypeTierOne one, TypeTierTwo two, TypeTierThree three)
    {
        tierOneFunction[one](v);
        tierTwoFunction[two](v);
        tierThreeFunction[three](v);
    }

    protected abstract void ExecuteTierOneOne(Vehicle v);
    protected abstract void ExecuteTierOneTwo(Vehicle v);
    protected abstract void ExecuteTierTwoOne(Vehicle v);
    protected abstract void ExecuteTierTwoTwo(Vehicle v);
    protected abstract void ExecuteTierThreeOne(Vehicle v);
    protected abstract void ExecuteTierThreeTwo(Vehicle v);
    protected void DoNothing(Vehicle v)
    {
        return;
    }
}
