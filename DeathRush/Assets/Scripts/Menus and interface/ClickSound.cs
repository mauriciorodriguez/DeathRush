using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickSound : MonoBehaviour
{
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    public List<AudioClip> sounds;

    void Start ()
    {
        source.playOnAwake = false;
	}
	
    public void ButtonSound()
    {
        source.PlayOneShot(sounds[0]);
    }

    public void BuySound()
    {
        source.PlayOneShot(sounds[1]);
    }

    public void GoToRaceSound()
    {
        source.PlayOneShot(sounds[2]);
    }
}
