using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScreenCountrySelect : ScreenView {
    public event Action OnCountrySelect = delegate { };

    public SpriteRenderer flag;
    public Text countryName;
    public GameObject nextButton;
    [HideInInspector]
    public Country.NamesType countryEnum;

    /// <summary>
    /// Accion al presionar el boton de seleccion de pais
    /// </summary>
    /// <param name="countrySelected">Pais seleccionado</param>
    public void OnSelectCountryButton(Country countrySelected)
    {        
        nextButton.SetActive(true);
        flag.sprite = countrySelected.flag;
        countryName.text = countrySelected.countryName;
        countryEnum = countrySelected.countryNameEnum;
    }

    public void OnNextButton()
    {
        PlayerData.instance.countryType = countryEnum;
        K.countryFlag = flag.sprite;
        OnCountrySelect();
    }

}
