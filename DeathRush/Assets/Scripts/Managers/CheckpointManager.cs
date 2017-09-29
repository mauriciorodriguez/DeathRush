using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CheckpointManager : Manager
{
    public float checkpointValue { get; private set; }
    public List<Checkpoint> checkpointsList { get; private set; }

    private Dictionary<Vehicle, int> _vehiclesDictionary; // <Vehiculo, proximo checkpoint>

    private void Awake()
    {
        checkpointsList = new List<Checkpoint>();
        foreach (var checkpoint in GameObject.FindGameObjectWithTag(K.TAG_CHECKPOINTS).GetComponentsInChildren<Checkpoint>())
        {
            checkpointsList.Add(checkpoint);
        }
        checkpointValue = (float)1 / checkpointsList.Count;
        int aux = 1;
        foreach (var chk in checkpointsList)
        {
            chk.SetNextCheckpoint(checkpointsList[aux]);
            if (aux == checkpointsList.Count - 1)
            {
                aux = 0;
            }
            else
            {
                aux++;
            }
        }
    }

    private void Start()
    {
        _vehiclesDictionary = new Dictionary<Vehicle, int>();
        foreach (var vehicle in GetComponent<GameManager>().GetAllRacers())
        {
            _vehiclesDictionary[vehicle] = 0;
        }
        OnInit();
    }

    protected override void OnInit()
    {
        foreach (var racer in _vehiclesDictionary.Keys) racer.OnDeath += VehicleDestroyed;
    }

    /// <summary>
    /// Checkea si el vehiculo puede pasar por el checkpoint
    /// </summary>
    /// <param name="vehicle">Vehiculo</param>
    /// <param name="chk">Checkpoint</param>
    /// <returns>puede o no pasar por el checkpoint</returns>
    public bool CheckVehicleCheckpoint(Vehicle vehicle, Checkpoint chk)
    {
        if (_vehiclesDictionary[vehicle] == checkpointsList.IndexOf(chk))
        {
            if (_vehiclesDictionary[vehicle] == checkpointsList.Count - 1)
            {
                _vehiclesDictionary[vehicle] = 0;
                return true;
            }
            else
            {
                _vehiclesDictionary[vehicle]++;
                return true;
            }
        }
        return false;
    }

    private void VehicleDestroyed(Vehicle v)
    {
        if (_vehiclesDictionary.ContainsKey(v))
        {
            v.OnDeath -= VehicleDestroyed;
            _vehiclesDictionary.Remove(v);
        }
    }
}
