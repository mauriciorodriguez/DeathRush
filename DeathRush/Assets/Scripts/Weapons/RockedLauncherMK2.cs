using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RockedLauncherMK2 : Weapon
{
    public float angleView;
    public float maxDistance;
   // public float damage;
  //  public Transform launchPoint;
    public GameObject rocket;
    public Vector3 _pointAttack;
   // public Camera _mainCam;
    
    public GameObject lockOn;
    private RawImage _lockOn;
    private Ray ray;
    private LayerMask layer;

    public short funcionality = 0;

    protected override void Start ()
    {
        base.Start();
        isCrosshair = true;
       // lockOn.SetActive(false);
       // _lockOn = lockOn.GetComponent<RawImage>();
        currentAmmo = maxAmmo = visualAmmo.fillAmount;
    }


    //currentAmmo >= maxAmmo / missileCountAmmo: chequea mínimo requerido para lanzar misil.
    protected override void Update()
    {
        if (GameManager.disableShoot == false && funcionality == 0)
        {
            CheckAmmoBar();
            OneShoot();
            if (canShoot)
            {
                ray = _mainCam.ScreenPointToRay(Input.mousePosition);
                var rays = Physics.RaycastAll(ray, Mathf.Infinity);
                foreach (var item in rays)
                {
                    if (!item.collider.isTrigger)
                    {
                        if (item.collider.gameObject.layer == K.LAYER_GROUND || item.collider.gameObject.layer == K.LAYER_SIDEGROUND || item.collider.gameObject.layer == K.LAYER_OBSTACLE)
                        {
                            _pointAttack = item.point;
                            //         print(currentAmmo % 3  + " asdfa " + maxAmmo / missileCountAmmo);
                            if (visualAmmo.fillAmount > 0 && currentAmmo >= maxAmmo / missileCountAmmo && Input.GetMouseButtonDown(shootButtom)) Shoot();
                            return;
                        }
                    }
                }
            }
        }
    }
    public override void Shoot()
    {
        base.Shoot();

        GameObject rock = PoolManager.instance.rocketLauncher.GetObject();
        rock.GetComponent<Rocket>().SetTarget(_pointAttack, damage); //cambio

        rock.transform.position = shootPoint.position;
        rock.transform.rotation = shootPoint.rotation;

        if (funcionality == 0)
        { 
            _soundManagerReference.PlaySound(K.SOUND_MISIL_LAUNCH);
        //GameObject rock = (GameObject)GameObject.Instantiate(rocket, launchPoint.position, launchPoint.rotation);
        }


        if (myCollider.GetComponentInParent<VehicleData>() != null)
            rock.GetComponent<Ammo>().SetObjectParent(myCollider.GetComponentInParent<VehicleData>());

        if (myCollider != null && rock.GetComponentInChildren<Collider>() != null)
            Physics.IgnoreCollision(myCollider, rock.GetComponentInChildren<Collider>());
        else
            print("Error Colider misil");

        //   lockOn.SetActive(false);
        currentAmmo -= maxAmmo / missileCountAmmo;
    }

    private void CheckAmmoBar()
    {
        visualAmmo.GetComponentInParent<Canvas>().transform.LookAt(Camera.main.transform.position);
        float calc_ammo = currentAmmo / maxAmmo;
        visualAmmo.fillAmount = calc_ammo;
        ReloadAmmo();

    //    if (visualAmmo.fillAmount == 0) ammoEmpty = true;
    }

    private void ReloadAmmo()
    {
        if (currentAmmo < maxAmmo) currentAmmo += Time.deltaTime * reloadSpeed;

        if (visualAmmo.fillAmount == 1)
        {
            ammoEmpty = false;
            currentAmmo = maxAmmo;
        }
    }
}
