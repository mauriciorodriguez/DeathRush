using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlagFromOurCountry : MonoBehaviour
{
    void Start()
    {
        GetComponent<Image>().sprite = Country.flagSprite[PlayerData.instance.countryType];
    }
}
