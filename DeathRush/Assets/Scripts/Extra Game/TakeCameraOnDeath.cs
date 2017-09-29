using UnityEngine;
using System.Collections;

public class TakeCameraOnDeath : MonoBehaviour
{

    private Camera _mainCam;

	// Use this for initialization
	void Start ()
    {
        _mainCam = Camera.main;
        if (_mainCam != null)
        {
            _mainCam.depth = -5;
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
