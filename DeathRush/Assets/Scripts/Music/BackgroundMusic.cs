using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    public List<AudioClip> backgroundMusics;
    private AudioSource _channel;
    private int _count;
    void Awake()
    {
        _channel = GetComponent<AudioSource>();
    }
	void Start ()
    {

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "DesertTrack")
        {
            _count = 0;
        }
        else if (scene.name == "WaterTomb")
        {
            _count = 1;
        }
        else if (scene.name == "CityTrack")
        {
            _count = 2;
        }
        else if (scene.name == "SatelliteTrack")
        {
            _count = 3;
        }
        else if (scene.name == "InsideTheCore")
        {
            _count = 4;
        }
        else
        {
            //print("No Music");
        }

        /*
        _count = Random.Range(0, backgroundMusics.Count);
        _channel.clip = backgroundMusics[_count];
        _channel.Play();
        */
    }
	
	void Update ()
    {
        if (!_channel.isPlaying)
        {
            //_count = Random.Range(0, backgroundMusics.Count);
            _channel.clip = backgroundMusics[_count];
            _channel.Play();
        }
        //if (_count == backgroundMusics.Count - 1) _count = 0;
	}
}
