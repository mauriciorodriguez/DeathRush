using UnityEngine;
using System.Collections;
using System;

public abstract class Ammo : MonoBehaviour, IReusable
{
    protected VehicleData _bulletOwner;

    public virtual void OnAcquire()
    {
        gameObject.SetActive(true);
    }

    public virtual void SetObjectParent(VehicleData dat)
    {
        //print( "Padre Seteado " + dat);
        if (_bulletOwner != null) _bulletOwner = null;

        _bulletOwner = dat;
    }

    public virtual void OnCreate()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnRelease()
    {
       // if (_bulletOwner != null) _bulletOwner = null;

        gameObject.SetActive(false);
    }
}
