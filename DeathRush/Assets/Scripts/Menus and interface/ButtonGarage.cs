using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ButtonGarage : MonoBehaviour
{
    /*public int idData;
    public string idPlayerPref;
    public Text showText;
    public int costUnlock;
    public bool unlocked;
    public string unlock;
    public GameObject notEnought;

	// Use this for initialization
	void Start ()
    {
        if (PlayerPrefs.GetString(unlock) == "true")
            unlocked = true;

        if (unlocked)
        {
            PlayerPrefs.SetString(unlock, "true");
            
            if (PlayerPrefs.GetInt(idPlayerPref) != idData)
                showText.text = "Equip";
            else
                showText.text = "Equiped";
        }else if (!unlocked)
        {
            showText.text = "Cost: " + costUnlock;
        }
    }

    public void Press()
    {
        if (!unlocked)
        {
            int resources = PlayerPrefs.GetInt("Resources");
            if (resources < costUnlock)
            {
                print("posees " + resources + " y te cuesnta " + costUnlock);
                notEnought.SetActive(true);
            }
            else if (resources >= costUnlock)
            {
                showText.text = "Equip";
                resources -= costUnlock;
                PlayerPrefs.SetInt("Resources", resources);
                unlocked = true;
                PlayerPrefs.SetString(unlock, "true");
            }
        }
        else if (unlocked)
        {
            if (PlayerPrefs.GetInt(idPlayerPref) != idData)
            {
                PlayerPrefs.SetInt(idPlayerPref, idData);
                showText.text = "Equiped";
                print(PlayerPrefs.GetInt(idPlayerPref));
            }
        }
    }

    public void Update()
    {
        if (unlocked)
        {
            if (PlayerPrefs.GetInt(idPlayerPref) != idData)
                showText.text = "Equip";
            else
                showText.text = "Equiped";
        }
    }*/
}
