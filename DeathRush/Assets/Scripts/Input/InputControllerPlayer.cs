using UnityEngine;
using System.Collections;
using System;

public class InputControllerPlayer : InputController
{
    public CameraContainerFollow camera;

    protected override void FixedUpdate()
    {
        _accel = Input.GetAxis(K.INPUT_VERTICAL);
        _steer = Input.GetAxis(K.INPUT_HORIZONTAL);
        _handbrake = Input.GetAxis(K.INPUT_HANDBRAKE);
        _nitro = Input.GetAxis(K.INPUT_NITRO);
        base.FixedUpdate();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q)) ChangeCameraPos(false);
        else if (Input.GetKeyUp(KeyCode.Q)) ChangeCameraPos(true);
    }

    private void ChangeCameraPos(bool frontView)
    {
        if (frontView)
        {
            camera.frontView = frontView;
        }
        else
        {
            camera.frontView = frontView;
        }

        /* if (Input.GetKeyDown(KeyCode.Q))
         {
             rearMirror.gameObject.SetActive(true);
             Camera.main.depth = -1f;
         }
         else if (Input.GetKeyUp(KeyCode.Q))
         {
             rearMirror.gameObject.SetActive(false);
             Camera.main.depth = 0f;
         }*/
    }
}
