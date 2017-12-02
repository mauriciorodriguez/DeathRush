using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScreenSearchRace : ScreenView
{
    public event Action OnNoPilot = delegate { };
    public event Action OnNoSelectedPilot = delegate { };

    public GameObject loading;
    public List<GameObject> trackList;


    private int _totalPilots;
    private List<GameObject> _vehiclesList = new List<GameObject>();
    private PlayerData _playerData;
    public Animation[] lockers;
    public Animation[] doorBunker;
    public Animation lockersRotation;
    private float _timer = 10;
    public Material lockerColor;

    private int _track;

    private void OnEnable()
    {
        cameraMenu.setMount(cameraMenu.searchRaceMount);
        assistRobot.setMount(assistRobot.arSearchRaceMount);
        _playerData = PlayerData.instance;
        loading.SetActive(false);
        EnableTracks();
        lockerColor.SetColor("_Color", Color.red);
    }

    private void EnableTracks()
    {
        for (int i = 0; i < trackList.Count; i++)
        {
            if (i <= _playerData.racesCompleted) trackList[i].SetActive(true);
            else trackList[i].SetActive(false);
        }
    }

    public void OnSearchForRaceButton(int track)
    {
        _totalPilots = _playerData.racerList.Count;
        if (_totalPilots > 0 && _playerData.selectedRacer != -1)
        {
            _track = track;
            PersistendData.instance.ReleaseAllInfo();
            cameraMenu.setMount(cameraMenu.doorMount);
            OpenDoor();

        }
        else if (_totalPilots == 0)
        {
            OnNoPilot();
        }
        else
        {
            OnNoSelectedPilot();
        }
    }

    private void LoadTrack()
    {
        if (_track == 0)
        {
            loading.SetActive(true);
            SceneManager.LoadScene((int)SCENES_NUMBER.DesertTrack);
        }
        else if (_track == 1)
        {
            loading.SetActive(true);
            SceneManager.LoadScene((int)SCENES_NUMBER.WaterTomb);
        }
        else if (_track == 2)
        {
            loading.SetActive(true);
            SceneManager.LoadScene((int)SCENES_NUMBER.RushHour);
        }
        else if (_track == 3)
        {
            loading.SetActive(true);
            SceneManager.LoadScene((int)SCENES_NUMBER.Satellitrack);
        }

        else if (_track == 4)
        {
            loading.SetActive(true);
            SceneManager.LoadScene((int)SCENES_NUMBER.InsideTheCore);
        }
    }

    private void OpenDoor()
    {
        lockerColor.SetColor("_Color", Color.green);
        foreach (var locker in lockers)
        {
            locker.enabled = true;
            locker.Play();
        }
    }


    protected override void Update()
    {
        if (lockers[0].enabled && !lockers[0].isPlaying)
        {
            lockersRotation.enabled = true;
            lockersRotation.Play();
            lockers[0].enabled = false;
        }

        if (lockersRotation.enabled && !lockersRotation.isPlaying)
        {
            foreach (var door in doorBunker)
            {
                door.enabled = true;
                door.Play();
            }
            lockersRotation.enabled = false;
            lockerColor.SetColor("_Color", Color.red);
        }

        if(doorBunker[0].enabled && !doorBunker[0].isPlaying) LoadTrack();

    }
       
}
