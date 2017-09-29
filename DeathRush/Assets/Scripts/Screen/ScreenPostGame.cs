using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class ScreenPostGame : ScreenView
{
    public Text position, oponents, chaos, credits, exp;
    public int creditsFirstPosition, creditsSecondPosition, creditsThirdPosition, defaultCredits, creditsPerKill, chaosFirstPosition, chaosSeconPosition, chaosThirdPosition, destroyedVehicleChaos, defaultChaos, expFirstPosition, expSecondPosition, expThirdPosition, expDefaultPosition;

    private PlayerData _playerData;
    private RacerData _racerData;
    private bool _isDestroyed;

    private void OnEnable()
    {
        _playerData = PlayerData.instance;
        _racerData = _playerData.racerList[_playerData.selectedRacer];
        RacerPortrait.Build(_racerData);
        _playerData.canHireNewRacers = true;
        _playerData.hiredRacers = 0;
        _playerData.isRaceFinished = false;
        _playerData.racesCompleted++;
        if (_racerData.currentLife <= 0)
        {
            var s = "";
            if (_racerData.racerClass.classType == Classes.Type.Soldier &&
                _racerData.unlockedTierTwo == Classes.TypeTierTwo.Survivor)
            {
                if (Random.value >= .5f) _isDestroyed = false;
                else
                {
                    _isDestroyed = true;
                    s = "\n(Racer Destroyed)";
                }
            }
            else if (_racerData.racerClass.classType == Classes.Type.Superstar &&
               _racerData.unlockedTierOne == Classes.TypeTierOne.LastHope)
            {
                if (Random.value >= .25f)
                {
                    _isDestroyed = false;
                    _playerData.countryChaos[_playerData.countryType] -= 20;
                }
                else
                {
                    _isDestroyed = true;
                    s = "\n(Racer Destroyed)";
                }
            }
            else
            {
                _isDestroyed = true;
                s = "\n(Racer Destroyed)";
            }
            position.text = "Position " + _racerData.lastRacePosition + s;
        }
        else
        {
            position.text = "Position " + _racerData.lastRacePosition;
            _isDestroyed = false;
        }
        credits.text = "$" + SetStats(_racerData.lastRacePosition, _isDestroyed);
        oponents.text = "Oponents crushed: " + PersistendData.instance.GiveKill();
        chaos.text = "Chaos: " + _playerData.countryChaos[_playerData.countryType];
        if (_isDestroyed) exp.text = "";
        else exp.text = "Pilot exp: " + _racerData.racerExp;
    }

    private int SetStats(int pos, bool destroyed)
    {
        int resources = 0;
        int chaos = 0;
        int plusChaos = 0;
        if (destroyed)
        {
            if (_racerData.racerClass.classType == Classes.Type.Superstar)
            {
                if (_racerData.unlockedTierOne == Classes.TypeTierOne.Martir) chaos -= defaultChaos;
                else chaos += destroyedVehicleChaos + (int)(destroyedVehicleChaos * .2f);
            }
            else chaos = destroyedVehicleChaos;
            if (_playerData.playerUpgrades.Contains(Upgrade.Type.MediaControl))
            {
                chaos -= (int)(chaos * .3f);
            }
            _playerData.countryChaos[_playerData.countryType] += chaos;
        }
        else
        {
            //TODO: CAMBIAR
            if (_playerData.racersQty == pos) pos = 4;
            float percentage = 0;
            switch (pos)
            {
                case 1:
                    int plusCredits = 0;
                    if (_racerData.racerClass.classType == Classes.Type.Superstar)
                    {
                        if (_racerData.unlockedTierThree == Classes.TypeTierThree.PeaceMaker)
                            percentage = .4f;
                        else percentage = .25f;
                        chaos += chaosFirstPosition + (int)(chaosFirstPosition * percentage);
                        if (_racerData.unlockedTierThree == Classes.TypeTierThree.TooGood) plusChaos = 5;
                    }
                    else chaos = chaosFirstPosition;
                    _playerData.countryChaos[_playerData.countryType] -= chaos;
                    _racerData.AddExperience(expFirstPosition);
                    if (_racerData.racerClass.classType == Classes.Type.Runner &&
                        (_racerData.unlockedTierOne == Classes.TypeTierOne.HighStandars ||
                        _racerData.unlockedTierThree == Classes.TypeTierThree.Golden)
                        )
                    {
                        if (_racerData.unlockedTierOne == Classes.TypeTierOne.HighStandars) plusCredits += (int)(creditsFirstPosition + (creditsFirstPosition * .25f));
                        if (_racerData.unlockedTierThree == Classes.TypeTierThree.Golden)
                        {
                            if (Random.value >= .5) plusCredits *= 2;
                        }
                    }
                    else
                    {
                        plusCredits = creditsFirstPosition;
                    }
                    resources = AddResources(plusCredits);
                    break;
                case 2:
                    if (_racerData.racerClass.classType == Classes.Type.Superstar)
                    {
                        if (_racerData.unlockedTierThree == Classes.TypeTierThree.PeaceMaker)
                            percentage = .4f;
                        else percentage = .25f;
                        chaos += chaosSeconPosition + (int)(chaosSeconPosition * percentage);
                    }
                    else chaos = chaosSeconPosition;
                    _playerData.countryChaos[_playerData.countryType] -= chaos;
                    _racerData.AddExperience(expSecondPosition);
                    resources = AddResources(creditsSecondPosition);
                    break;
                case 3:
                    if (_racerData.racerClass.classType == Classes.Type.Superstar)
                    {
                        if (_racerData.unlockedTierThree == Classes.TypeTierThree.PeaceMaker)
                            percentage = .4f;
                        else percentage = .25f;
                        chaos += chaosThirdPosition + (int)(chaosThirdPosition * percentage);
                    }
                    else chaos = chaosThirdPosition;
                    _playerData.countryChaos[_playerData.countryType] -= chaosThirdPosition;
                    _racerData.AddExperience(expThirdPosition);
                    resources = AddResources(creditsThirdPosition);
                    break;
                default:
                    if (_racerData.racerClass.classType == Classes.Type.Superstar) chaos += defaultChaos + (int)(defaultChaos * .2f);
                    else chaos = defaultChaos;
                    _playerData.countryChaos[_playerData.countryType] += chaos;
                    _racerData.AddExperience(expDefaultPosition);
                    resources = AddResources(defaultCredits);
                    break;
            }
        }
        _playerData.countryChaos[_playerData.countryType] = Mathf.Clamp(_playerData.countryChaos[_playerData.countryType], 0, 100);
        foreach (var country in _playerData.countryChaos.Keys.ToList())
        {
            if (country != _playerData.countryType)
            {
                _playerData.countryChaos[country] += plusChaos;
                _playerData.countryChaos[country] = Mathf.Clamp(_playerData.countryChaos[country], 0, 100);
            }
        }
        return resources;
    }

    private int AddResources(int amount)
    {
        if (_racerData.racerClass.classType == Classes.Type.Soldier &&
            _racerData.unlockedTierOne == Classes.TypeTierOne.LiveForAnotherDay)
        {
            amount += (int)(amount * .1f);
        }
        amount += (int)(_racerData.unlockedTierOne == Classes.TypeTierOne.BountyHunter ? (PersistendData.instance.GiveKill() * creditsPerKill) * 2 : PersistendData.instance.GiveKill() * creditsPerKill);
        _playerData.resources += amount;
        return amount;
    }

    public override void OnBackButton()
    {
        if (_isDestroyed)
        {
            _playerData.racerList.RemoveAt(_playerData.selectedRacer);
            _playerData.selectedRacer = -1;
        }
        base.OnBackButton();
    }
}