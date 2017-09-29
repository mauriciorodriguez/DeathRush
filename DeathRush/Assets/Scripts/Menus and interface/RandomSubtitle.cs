using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RandomSubtitle : MonoBehaviour {

    public static List<string> subtitles = new List<string>();

   

    void Start ()
    {

        GetComponent<Text>().text = randomText();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    string randomText()
    {
        string subtitleText = "";
        int random = Random.Range(0, 26);

        if (random==0)
        {
            subtitleText = "The Re-Awakening";
        }
        else if(random==1)
        {
            subtitleText = "The revange";
        }
        else if (random == 2)
        {
            subtitleText = "The Game";
        }
        else if (random == 3)
        {
            subtitleText = "Revelations";
        }
        else if (random == 4)
        {
            subtitleText = "Revolutions";
        }
        else if (random == 5)
        {
            subtitleText = "Generations";
        }
        else if (random == 6)
        {
            subtitleText = "The Pre-Sequel";
        }
        else if (random == 7)
        {
            subtitleText = "Homecoming";
        }
        else if (random == 8)
        {
            subtitleText = "Homecoming";
        }

        else if (random == 9)
        {
            subtitleText = "Ultimate Edition";
        }

        else if (random == 10)
        {
            subtitleText = "Reloaded";
        }

        else if (random == 11)
        {
            subtitleText = "Homecoming";
        }
        else if (random == 12)
        {
            subtitleText = "Deadliest, rushest";
        }
        else if (random == 13)
        {
            subtitleText = "The interactive experiencie";
        }
        else if (random == 14)
        {
            subtitleText = "Rebooted";
        }
        else if (random == 15)
        {
            subtitleText = "Overdrive";
        }
        else if (random == 16)
        {
            subtitleText = "Technicolor";
        }
        else if (random == 17)
        {
            subtitleText = "Turbo";
        }
        else if (random == 18)
        {
            subtitleText = "Director's Cut";
        }
        else if (random == 19)
        {
            subtitleText = "Unity";
        }
        else if (random == 20)
        {
            subtitleText = "Ligthspeed";
        }
        else if (random == 21)
        {
            subtitleText = "Go";
        }
        else if (random == 22)
        {
            subtitleText = "Lost in time";
        }
        else if (random == 22)
        {
            subtitleText = "Megaforce";
        }
        else if (random == 22)
        {
            subtitleText = "Fury Goat";
        }
        else if (random == 23)
        {
            subtitleText = "Evolution";
        }
        else if (random == 24)
        {
            subtitleText = "Dead is the new Alive";
        }
        else if (random == 25)
        {
            subtitleText = "Choose your own Apocalipsis";
        }
        else
        {
            subtitleText = "";
        }
        return subtitleText;
    }


}
