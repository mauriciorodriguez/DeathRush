using UnityEngine;
using System.Collections;
using System;

public class MolotovBomb : Ammo
{
    public GameObject bomb;
    public GameObject fire;

    private VehicleData _owner;

    public override void OnAcquire()
    {
        ActiveMolotov();
    }

    public override void OnCreate()
    {
        TurnDownObjects();
    }

    public override void OnRelease()
    {
        TurnDownObjects();
    }

    private void TurnDownObjects()
    {
        transform.position = new Vector3(0f,0f,0f);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        bomb.SetActive(false);
        fire.SetActive(false);
    }

    public void ActiveMolotov()
    {
        bomb.SetActive(true);
        fire.SetActive(false);
    }

    public void SetOwner(VehicleData v)
    {
        _owner = v;
    }

    public VehicleData CallOwner()
    {
        return _owner;
    }

    public void StartBurn(Vector3 posi)
    {
        transform.position = posi;
        bomb.SetActive(false);
        fire.SetActive(true);
    }
}
