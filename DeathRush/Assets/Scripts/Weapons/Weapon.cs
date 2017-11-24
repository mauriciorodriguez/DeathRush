using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Weapon : MonoBehaviour
{
    public static Dictionary<Type, Func<GameObject>> prefabsDict = new Dictionary<Type, Func<GameObject>>()
    {
        {Type.Turret, ( )=> Resources.Load<GameObject>(K.PATH_TURRET)},
        {Type.MissileLuncher, () => Resources.Load<GameObject>(K.PATH_MISSILE_LAUNCHER) },
        {Type.SawLauncher, () => Resources.Load<GameObject>(K.PATH_SAW_LAUNCHER) },
        {Type.LaserBeam, () => Resources.Load<GameObject>(K.PATH_LASER_BEAM) },
        {Type.MolotovLauncher, () => Resources.Load<GameObject>(K.PATH_MOLOTOV_LAUNCHER) },
        {Type.LockLauncher, () => Resources.Load<GameObject>(K.PATH_LOCK_LAUNCHER) },
        {Type.FreezeRay, () => Resources.Load<GameObject>(K.PATH_FREEZE_RAY) },
        {Type.FlameThrower, () => Resources.Load<GameObject>(K.PATH_FLAME_THROWER) },
        {Type.Mines, () => Resources.Load<GameObject>(K.PATH_MINES) },
        {Type.Oil, () => Resources.Load<GameObject>(K.PATH_OIL) },
        {Type.CombatDrone, () => Resources.Load<GameObject>(K.PATH_COMBAT_DRONE) },
        {Type.ElectromagneticMine, () => Resources.Load<GameObject>(K.PATH_ELECTROMAGNETIC_MINE) }
    };

    public enum Type
    {
        Turret,
        MissileLuncher,
        SawLauncher,
        LaserBeam,
        MolotovLauncher,
        LockLauncher,
        FreezeRay,
        FlameThrower,
        Mines,
        Oil,
        Shield,
        Smoke,
        ElectromagneticMine,
        CombatDrone,
        Null
    }

    public Type weaponType;
    public bool activeShoot;
    public float cooldown;
    public short shootButtom;
    public float damage;
    protected bool canShoot;
    private float _timeCoolDown;
    public bool isCrosshair;
    public Sprite crosshair;

    public Image visualAmmo;
    public float maxAmmo;
    public float currentAmmo;
    public bool ammoEmpty;
    public float missileCountAmmo;
    public float reloadSpeed;
    public float ammoTimer;
    protected float _ammoTimer;
    protected SoundManager _soundManagerReference;
    public float reach; //TODO: Implementar

    public float screenPercent = 50f;
    public float maxMinAngle = 20f;
    protected bool _isShooting;
    protected Camera _mainCam;
    public Collider myCollider;
    public GameObject weaponRotation;
    public Transform shootPoint;
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes rotationXY = RotationAxes.MouseXAndY;
    private float offset = 5;

    private bool _timeStoped;
    public float durationDishable;

    protected VehicleData _owner;

    protected virtual void Start()
    {
        /*
        for (int i = 0; i < transform.parent.gameObject.transform.parent.childCount; i++)
        {
            Transform trans = transform.parent.gameObject.transform.parent.GetChild(i);
            if (trans.CompareTag("ColliderHolder") && trans.GetComponentInChildren<Collider>() != null)
            {
            }
        }*/

        if (activeShoot && GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<SoundManager>() != null)
            _soundManagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<SoundManager>();
        _mainCam = Camera.main;
        offset = 5;
        _owner = this.gameObject.GetComponentInParent<VehicleData>();
        myCollider = _owner.transform.Find("ChassisCollider").GetComponent<Collider>();
    }

    public virtual void ShootDownButtom()
    {
        if (!_timeStoped)
        {
            if (activeShoot)
            {
                _timeCoolDown += Time.deltaTime;

                if (Input.GetMouseButton(shootButtom) && _timeCoolDown > cooldown && !canShoot && !ammoEmpty)
                {
                    //      print("intenta");
                    canShoot = true;
                    _timeCoolDown = 0;
                }
            }
        }
    }

    public void EMP()
    {
        _timeStoped = true;
        Invoke("RevertFreze", durationDishable);
    }

    private void RevertFreze()
    {
        _timeStoped = false;
    }

    protected virtual void Update()
    {
        if (!_timeStoped)
        {
            if (activeShoot)
            {
                if (Input.GetMouseButton(shootButtom)) _isShooting = true;
                else _isShooting = false;
                if (shootButtom == 0)
                {
                    TurnWeapon();
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(from, to);
    }
    Vector3 from;
    Vector3 to;

    void TurnWeapon()
    {
        var cam = Camera.main;
        var posOffset = Input.mousePosition + new Vector3(0, 0, reach);
        //      posOffset = new Vector3(posOffset.x, posOffset.y, posOffset.z);

        var viewPoint = Camera.main.ScreenToWorldPoint(posOffset);

        //      viewPoint.z = transform.position.z + 30;
        //      viewPoint += (Camera.main.transform.forward * 30); // cambiar 30 por weapon reach
        weaponRotation.transform.LookAt(viewPoint);

        from = weaponRotation.transform.position;
        to = viewPoint;
        //SCREEN TO WORLD POINT (OFFSET)

        /*Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x + 15, mouse.y, Camera.main.farClipPlane));
        Vector3 forward = mouseWorld - weaponRotation.transform.position;
        weaponRotation.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);

        Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.forward * 1500, Color.blue);*/

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // CHUCK

        /*      float mousePosY = Input.mousePosition.y;
                      float screenArea = Screen.width * (screenPercent / 100f);
                      mousePosY -= Screen.width / 2;
                      mousePosY = mousePosY / ((Screen.width / 2) * (screenPercent / 100f));
                      mousePosY = Mathf.Clamp(mousePosY, -1f, 1f);
                      Vector3 angle = weaponRotation.transform.localRotation.eulerAngles;
                      angle.x = -mousePosY * maxMinAngle;
                      weaponRotation.transform.localRotation = Quaternion.Euler(angle);*/

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //RAYCAST (TODO CON COLLIDER)

        /*
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var relativePos = hit.point - shootPoint.position;

            Debug.DrawRay(shootPoint.position, relativePos * 1500, Color.red);
            var rotation = Quaternion.LookRotation(relativePos) * (Quaternion.AngleAxis(offset, Vector3.right));

            if (rotationXY == RotationAxes.MouseXAndY) weaponRotation.transform.rotation = rotation;
            else if (rotationXY == RotationAxes.MouseX) weaponRotation.transform.rotation = new Quaternion(weaponRotation.transform.rotation.x, rotation.y, weaponRotation.transform.rotation.z, weaponRotation.transform.rotation.w);
            else weaponRotation.transform.rotation = new Quaternion(rotation.x, weaponRotation.transform.rotation.y, weaponRotation.transform.rotation.z, weaponRotation.transform.rotation.w);
        }
        */

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //PLANE RAYCAST (SOLO ROTA EN EJE Y)

        /*     Plane playerPlane = new Plane(Vector3.up, weaponRotation.transform.position);
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

             float hitdist = 0f;

             if (playerPlane.Raycast(ray, out hitdist))
             {
                 var targetPoint = ray.GetPoint(hitdist);
                 var rotY = Quaternion.LookRotation(targetPoint - weaponRotation.transform.position);
                 weaponRotation.transform.rotation = rotY;
             }*/

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //RAYCAST FROM CAMERA (EL IDEAL)
        /*    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 1500, Color.green);*/

    }


    public virtual void OneShoot()
    {
        if (!_timeStoped)
        {
            if (activeShoot)
            {
                if (!canShoot)
                    _timeCoolDown += Time.deltaTime;

                if (Input.GetMouseButtonDown(shootButtom) && _timeCoolDown > cooldown && !canShoot && !ammoEmpty)
                {
                    _timeCoolDown = 0;
                    canShoot = true;
                }
            }
        }
    }
    public virtual void Shoot()
    {

        if (!_timeStoped)
        {
            if (activeShoot)
                canShoot = false;
        } //  print("Disparo");
    }
}
