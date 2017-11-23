using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointReference : MonoBehaviour
{
    public bool enableMoveToSection;
    public enum Section { Monitor = 0, Hologram = 1,Garage = 2, Weapons = 3, HUB = 4 }
    public Section moveToSection = Section.Monitor;

    public BottomMenu bottomMenu;

    public Material lightMat;
    public ScreenMyWeapons screenMyWeapons;
    public Weapon.Type weapon;
    public enum WeaponType { PrimaryWeapon = 0, SecondaryWeapon = 1, Gadget = 2 }
    public WeaponType weaponType;
    private bool _mouseIsOver;

    private void Awake()
    {
        lightMat.SetColor("_EmissionColor", Color.black);
        _mouseIsOver = false;

    }

    void Update ()
    {
        transform.LookAt(Camera.main.transform);

        CheckIfMouseIsOver();

    }

    private void OnMouseDown()
    {
        if (enableMoveToSection)
        {
            if (moveToSection == Section.Hologram) bottomMenu.BTNSearchForRace(this.gameObject);
            else if (moveToSection == Section.Monitor) bottomMenu.BTNShowHireRacer(this.gameObject);
            else if (moveToSection == Section.Garage) bottomMenu.BTNShowGarage(this.gameObject);
            else if (moveToSection == Section.Weapons) bottomMenu.BTNShowWeapons(this.gameObject);
            else if (moveToSection == Section.HUB) bottomMenu.BTNShowHUB(this.gameObject);

            gameObject.SetActive(false);
            lightMat.SetColor("_EmissionColor", Color.black);
        }
        else
        {
            if (weaponType == WeaponType.PrimaryWeapon) screenMyWeapons.HandlePrimaryWeapon(weapon);
            else if (weaponType == WeaponType.SecondaryWeapon) screenMyWeapons.HandleSecondaryWeapon(weapon);
            else screenMyWeapons.HandleGadget(weapon);
        }
    }

    private void OnMouseOver()
    {
        _mouseIsOver = true;
        lightMat.SetColor("_EmissionColor", Color.green);
        
    }
    private void OnMouseExit()
    {
        _mouseIsOver = false;
        lightMat.SetColor("_EmissionColor", Color.black);
    }

    private void CheckIfMouseIsOver()
    {
        if (enableMoveToSection) return;

        if (screenMyWeapons == null || screenMyWeapons._playerData == null) return;
        if (screenMyWeapons._playerData.racerList[screenMyWeapons._playerData.selectedRacer].equippedPrimaryWeapon == weapon ||
           screenMyWeapons._playerData.racerList[screenMyWeapons._playerData.selectedRacer].equippedSecondaryWeapon == weapon ||
           screenMyWeapons._playerData.racerList[screenMyWeapons._playerData.selectedRacer].equippedGadget == weapon)
        {
            lightMat.SetColor("_EmissionColor", Color.green);
            return;
        }

        else if (!_mouseIsOver) lightMat.SetColor("_EmissionColor", Color.black);

    }
}
