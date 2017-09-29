using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class WeaponsManager : MonoBehaviour {

    private List<GameObject> _gadgetDrop = new List<GameObject>();
    public GameObject crosshair;
    private RawImage _lockOn;
    private Ray ray;
    private RaycastHit hit;
    public Camera _mainCam;
    public float distance;
    private bool _activeCrosshair;
    private Vector3 _frezze;

    public float screenPercent = 50f;
    public float maxMinAngle = 20f;

    // Use this for initialization
    void Start()
    {
        if (crosshair == null)
        {
            crosshair = GameObject.FindGameObjectWithTag("TargetCursor");
            crosshair.SetActive(true);
            _lockOn = crosshair.GetComponent<RawImage>();
        }
        _mainCam = Camera.main;
        
        //gadgetLauncher.gameObject.SetActive(true);
        //gadgetLauncher.GetComponent<GadgetLauncher>().SetGadget(gadgetsWeapons[0], 3);

        /*
        primaryWeapons[PlayerPrefs.GetInt("PrimaryWeapon")].gameObject.SetActive(true);
        sideWeapons[PlayerPrefs.GetInt("SideWeapon")].gameObject.SetActive(true);
        if (PlayerPrefs.GetString("GadgetActive") == "true")
        {
            print("enter " + PlayerPrefs.GetString("GadgetActive"));
            gadgetLauncher.gameObject.SetActive(true);
            gadgetLauncher.GetComponent<GadgetLauncher>().SetGadget(gadgetsWeapons[PlayerPrefs.GetInt("gadgetCode")], PlayerPrefs.GetInt("gadgetAmmo"));
        }
        else if (PlayerPrefs.GetString("GadgetActive") == "false")
            gadgetLauncher.gameObject.SetActive(false);
        */


        /*
        if (activeSet == 0)
        {
            Desactivate(weaponsSet2);
            Activate(weaponsSet1);
        }
        else if (activeSet == 1)
        {
            Desactivate(weaponsSet1);
            Activate(weaponsSet2);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
        /*
        if (activeSet == 0)
        {
            _frezze = _mainCam.WorldToScreenPoint(weaponsSet1[0].transform.position + -weaponsSet1[0].transform.forward * distance);
            _lockOn.rectTransform.position = _frezze;
            AimCollision();

        }
        else if (activeSet == 1)
        {
            _lockOn.transform.position = Input.mousePosition;
            AimCollision();
        }*/
        if(_lockOn != null)
            _lockOn.transform.position = Input.mousePosition;
        AimCollision();     
       // TurnWeapon();

    }

    /*
    void TurnWeapon()
    {

        float mousePosX = Input.mousePosition.x;
        float screenArea = Screen.width * (screenPercent / 100f);
        mousePosX -= Screen.width / 2;
        mousePosX = mousePosX / ((Screen.width / 2) * (screenPercent / 100f));
        mousePosX = Mathf.Clamp(mousePosX, -1f, 1f);

        for (int i = 0; i < primaryWeapons.Count; i++)
        {
            if (primaryWeapons[i].activeSelf)
            {
                Vector3 angle = primaryWeapons[i].transform.localRotation.eulerAngles;
                angle.y = mousePosX * maxMinAngle;
                primaryWeapons[i].transform.localRotation = Quaternion.Euler(angle);
            }
        }

    }
    */

    private void AimCollision()
    {
        ray = _mainCam.ScreenPointToRay(_lockOn.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 << K.LAYER_DESTRUCTIBLE))
        {
            if (hit.collider.gameObject.layer == K.LAYER_IA || hit.collider.gameObject.layer == K.LAYER_DESTRUCTIBLE) _lockOn.gameObject.GetComponent<CanvasRenderer>().SetColor(Color.red);
            else _lockOn.gameObject.GetComponent<CanvasRenderer>().SetColor(Color.white);
        }
    }

    private void Activate(List<GameObject> t)
    {
        for (int i = 0; i < t.Count; i++)
        {

            t[i].SetActive(true);
        }

        Sprite crosshair = t[0].GetComponent<Weapon>().crosshair;

        //TODO:
        //1. Acceder al crosshair de la pantalla (canvas).
        //2. Si el crosshair del arma es null, desactivar crosshair de canvas.
        //3. Si el crosshair del arma no es null, activars crosshair de canvas y setearle el sprite (crosshairCanvas.sprite = crosshair;)
    }

    private void Desactivate(List<GameObject> t)
    {
        for (int i = 0; i < t.Count; i++)
        {
            t[i].SetActive(false);
//            if (t[i].GetComponent<RocketLauncher>() != null) t[i].GetComponent<RocketLauncher>().lockOn.SetActive(false);
        }
    }


    public void DropGadget(GameObject gad)
    {
        _gadgetDrop.Add(gad);
    }

    public void RemoveGadget(GameObject gad)
    {
        _gadgetDrop.Remove(gad);
    }

    private void SetCrosshair(bool hasCrosshair)
    {
        if (hasCrosshair != _activeCrosshair)
        {

            _activeCrosshair = hasCrosshair;
        }
    }

}