using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenu : MonoBehaviour
{
    public float speedTransition= .1f;
    public Transform searchRaceMount, upgradesMount, chaosMapMount, hubMount, hirePilotMount, selectCountry, garageMount, racerInfoMount, mainMenuMount, myWeaponsMount, doorMount;

    public Transform arSearchRaceMount, arUpgradesMount, arChaosMapMount, arHubMount, arHirePilotMount, arSelectCountry, arGarageMount, arRacerInfoMount, arMainMenuMount, arMyWeaponsMount, arDoorMount;
    private Transform currentMount;

    public float speedRotation = 50f;
    public GameObject cameraRotationCanvas;

    private void Update()
    {
        if (currentMount == null) return;

    //    if (Vector3.Distance(transform.position, currentMount.position) > 0.001f)
      //  {
            transform.position = Vector3.Lerp(transform.position, currentMount.position, speedTransition);
            transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speedTransition);
     //   }
      /*  else if(cameraRotationCanvas != null && cameraRotationCanvas.activeSelf)
        {
            if (Input.mousePosition.x > Screen.width / 1.1f) transform.Rotate(0, Time.deltaTime * speedRotation, 0);
            else if (Input.mousePosition.x < 50) transform.Rotate(0, Time.deltaTime * -speedRotation, 0);
        }*/
    }

    public void setMount(Transform newMount)
    {
        currentMount = newMount;
    }
}
