﻿using UnityEngine;
using System.Collections;

public abstract class InputController : MonoBehaviour
{
    protected Vehicle _vehicleReference;
    protected float _accel, _brake, _handbrake, _steer, _nitro;

    protected virtual void Start()
    {
        _vehicleReference = GetComponent<Vehicle>();
    }

    protected virtual void FixedUpdate()
    {
        _vehicleReference.Move(_accel, _handbrake, _steer, _nitro);
    }
}
