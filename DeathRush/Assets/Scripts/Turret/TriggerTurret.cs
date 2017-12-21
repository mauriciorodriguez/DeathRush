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
            if(col.GetComponentInParent<Vehicle>())
            {
                if (!_vehiclesOnTrigger.Contains(col.GetComponentInParent<Vehicle>())) _vehiclesOnTrigger.Add(col.GetComponentInParent<Vehicle>());
            }

        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == K.LAYER_PLAYER || col.gameObject.layer == K.LAYER_IA)
        {
            if (col.GetComponentInParent<Vehicle>())
            {
                if (_vehiclesOnTrigger.Contains(col.GetComponentInParent<Vehicle>())) _vehiclesOnTrigger.Remove(col.GetComponentInParent<Vehicle>());
            }
        }
    }

    private void Update()
    {
        if(_vehiclesOnTrigger != null && _vehiclesOnTrigger.Count > 0)
        {
            if (_timer <= 0)
            {
                var index = Random.Range(0, _vehiclesOnTrigger.Count);

                if (_vehiclesOnTrigger[index].GetComponentInParent<Vehicle>())
                {
                    turretControl.SetTarget(_vehiclesOnTrigger[index].transform);
                    _timer = selectRandomTargetTimer;
                }
                else turretControl.SetTarget(null);
            }
            else _timer -= Time.deltaTime;
        }
       
    }
}
