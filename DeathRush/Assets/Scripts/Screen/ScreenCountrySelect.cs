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
    private Renderer[] renderers;
    private Material[] mats;

    public Material countrySelectBackground;
    public MeshRenderer background;
    private Material _defaultBackground;

    private void OnEnable()
    {
        cameraMenu.setMount(cameraMenu.selectCountry);
        assistRobot.setMount(assistRobot.arSelectCountry);
        renderers = GetComponentsInChildren<Renderer>();

        mats = new Material[8];
        for (int i = 0; i < renderers.Length; i++) if (renderers[i].sharedMaterial != null) mats[i] = renderers[i].sharedMaterial;
        foreach (var mat in mats)
        {
            if (mat.HasProperty("_EdgeColor") && mat.GetColor("_EdgeColor") == Color.green)
            {
                mat.SetColor("_EdgeColor", Color.cyan);
                mat.SetFloat("_EdgeWidth", 0);
            }
        }
        _defaultBackground = background.sharedMaterial;

        background.sharedMaterial = countrySelectBackground;

    }

    /// <summary>
    /// Accion al presionar el boton de seleccion de pais
    /// </summary>
    /// <param name="countrySelected">Pais seleccionado</param>
    public void OnSelectCountryButton(Country countrySelected)
    {        
        nextButton.SetActive(true);
   //     flag.sprite = countrySelected.flag;
    //    countryName.text = countrySelected.countryName;
        countryEnum = countrySelected.countryNameEnum;

        foreach (var mat in mats)
        {
            if (mat.HasProperty("_EdgeColor") && mat.GetColor("_EdgeColor") == Color.green)
            {
                mat.SetColor("_EdgeColor", Color.cyan);
                mat.SetFloat("_EdgeWidth", 0);
            }
        }
        countrySelected.GetComponent<Renderer>().sharedMaterial.SetColor("_EdgeColor", Color.green);
    }

    public void OnNextButton()
    {
        PlayerData.instance.countryType = countryEnum;
        K.countryFlag = flag.sprite;
        OnCountrySelect();

        background.sharedMaterial = _defaultBackground;
    }

}
