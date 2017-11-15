using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointReference : MonoBehaviour
{
    public enum Section { Monitor = 0, Hologram = 1,Garage = 2, Weapons = 3, HUB = 4 }
    public Section moveToSection = Section.Monitor;

    public BottomMenu bottomMenu;
    public Material lightMat;
    private void Awake()
    {

    }

    void Update ()
    {
        transform.LookAt(Camera.main.transform);
    }

    private void OnMouseDown()
    {
        if (moveToSection == Section.Hologram) bottomMenu.BTNSearchForRace(this.gameObject);
        else if (moveToSection == Section.Monitor) bottomMenu.BTNShowHireRacer(this.gameObject);
        else if (moveToSection == Section.Garage) bottomMenu.BTNShowGarage(this.gameObject);
        else if (moveToSection == Section.Weapons) bottomMenu.BTNShowWeapons(this.gameObject);
        else if (moveToSection == Section.HUB) bottomMenu.BTNShowHUB(this.gameObject);

        gameObject.SetActive(false);
        lightMat.SetColor("_EmissionColor", Color.black);
    }

    private void OnMouseOver()
    {
        float emission = Mathf.PingPong(Time.time, 1.0f);
        Color baseColor = new Color(0.6323529f,0,0); //Replace this with whatever you want for your base color at emission level '1'

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

        lightMat.SetColor("_EmissionColor", Color.green);
        
    }
    private void OnMouseExit()
    {
        lightMat.SetColor("_EmissionColor", Color.black);
    }
}
