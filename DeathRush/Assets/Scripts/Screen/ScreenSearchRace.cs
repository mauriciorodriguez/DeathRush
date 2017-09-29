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

    private void OnEnable()
    {
        _playerData = PlayerData.instance;
        loading.SetActive(false);
        EnableTracks();
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
            PersistendData.instance.ReleaseAllInfo();
            if (track == 0)
            {
                loading.SetActive(true);
                SceneManager.LoadScene((int)SCENES_NUMBER.DesertTrack);
            }
            else if (track == 1)
            {
                loading.SetActive(true);
                SceneManager.LoadScene((int)SCENES_NUMBER.WaterTomb);
            }
            else if (track == 2)
            {
                loading.SetActive(true);
                SceneManager.LoadScene((int)SCENES_NUMBER.RushHour);
            }
            else if (track == 3)
            {
                loading.SetActive(true);
                SceneManager.LoadScene((int)SCENES_NUMBER.Satellitrack);
            }

            else if (track == 4)
            {
                loading.SetActive(true);
                SceneManager.LoadScene((int)SCENES_NUMBER.InsideTheCore);
            }
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
}
