using UnityEngine;
using System.Collections.Generic;

public class FreezeTrap : MonoBehaviour
{
    public float freezePower;
    public float defrostTimer;
    private List<float> _defrostTimerVehicles;
    private List<Vehicle> _vehicles;
    private List<bool> _freezeVehicles;
    private bool _removeFrost;
    void Awake()
    {
        _defrostTimerVehicles = new List<float>();
        _vehicles = new List<Vehicle>();
        _freezeVehicles = new List<bool>();
    }

    void Update()
    {
        var processHandler = Camera.main.GetComponent<PostProcessHandler>();

        if (processHandler.material.GetFloat("_FadeEffect") < 0) _removeFrost = false;
        if (_removeFrost && processHandler != null && processHandler.material.GetFloat("_FadeEffect") > 0) processHandler.FadeColor(false);

        if (_vehicles.Count > 0)
        {
            for (int i = 0; i < _vehicles.Count; i++)
            {
                if (_defrostTimerVehicles[i] > 0 && _freezeVehicles[i]) FreezeVehicle();
                else
                {
                    _vehicles[i].GetComponent<Rigidbody>().drag = 0;

                    //  if (processHandler != null && _vehicles[i].gameObject.layer == K.LAYER_PLAYER && processHandler.material.GetFloat("_FadeEffect") > 0) processHandler.material.SetFloat("_FadeEffect", 0);
                    //      if (processHandler != null && processHandler.material.GetFloat("_FadeEffect") > 0) processHandler.FadeColor(false);
                    if (_vehicles[i].gameObject.layer == K.LAYER_PLAYER) _removeFrost = true;
                    _defrostTimerVehicles.RemoveAt(i);
                    _freezeVehicles.RemoveAt(i);
                    _vehicles.RemoveAt(i);
                    return;
                }
            }
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == K.LAYER_PLAYER)
        {
            if (PlayerData.instance.racerList[PlayerData.instance.selectedRacer].unlockedTierTwo == Classes.TypeTierTwo.OnlyAScratch) return;
            _freezeVehicles.Add(true);
            _vehicles.Add(col.GetComponentInParent<VehiclePlayerController>());
            _defrostTimerVehicles.Add(defrostTimer);
            col.GetComponentInParent<VehiclePlayerController>().GetComponentInChildren<Weapon>().EMP();
        }

        if (col.gameObject.layer == K.LAYER_IA)
        {
            if (!col.GetComponentInParent<IAVehicleData>()) return;
            _freezeVehicles.Add(true);
            _vehicles.Add(col.GetComponentInParent<VehicleIAController>());
            _defrostTimerVehicles.Add(defrostTimer);
            col.GetComponentInParent<IAVehicleData>().StopAttacks();
        }
    }

    private void FreezeVehicle()
    {

        for (int i = 0; i < _vehicles.Count; i++)
        {
            if (_defrostTimerVehicles[i] < 0) return;
            if (_vehicles[i].gameObject.layer == K.LAYER_PLAYER) Camera.main.GetComponent<PostProcessHandler>().FadeColor(true);

            _vehicles[i].GetComponent<Rigidbody>().drag += freezePower * Time.deltaTime;
            _defrostTimerVehicles[i] -= Time.deltaTime;
        }

    }

}
