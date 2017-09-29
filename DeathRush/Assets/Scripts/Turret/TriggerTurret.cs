using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerTurret : MonoBehaviour
{
    public Turret turretControl;
    public float selectRandomTargetTimer = 3;

    private List<Vehicle> _vehiclesOnTrigger;
    private float _timer;

    private void Start()
    {
        _vehiclesOnTrigger = new List<Vehicle>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == K.LAYER_PLAYER || col.gameObject.layer == K.LAYER_IA)
        {
            if (!_vehiclesOnTrigger.Contains(col.GetComponentInParent<Vehicle>())) _vehiclesOnTrigger.Add(col.GetComponentInParent<Vehicle>());
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == K.LAYER_PLAYER || col.gameObject.layer == K.LAYER_IA)
        {
            if (_vehiclesOnTrigger.Contains(col.GetComponentInParent<Vehicle>())) _vehiclesOnTrigger.Remove(col.GetComponentInParent<Vehicle>());
        }
    }

    private void Update()
    {
        if (_timer <= 0)
        {
            if (_vehiclesOnTrigger.Count > 0)
            {
                turretControl.SetTarget(_vehiclesOnTrigger[Random.Range(0, _vehiclesOnTrigger.Count)].transform);
                _timer = selectRandomTargetTimer;
            }
            else turretControl.SetTarget(null);
        }
        else _timer -= Time.deltaTime;
    }
}
