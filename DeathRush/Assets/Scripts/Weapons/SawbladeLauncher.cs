using UnityEngine;
using System.Collections;

public class SawbladeLauncher : Weapon
{
    //public float damage;
    public float speed;
//    public Transform launchPoint;
   // public Camera _mainCam;
    public LayerMask myLayer;
    private Ray ray;

    protected override void Start()
    {
        base.Start();

        isCrosshair = false;
        currentAmmo = maxAmmo = visualAmmo.fillAmount;
    }


    //currentAmmo >= maxAmmo / missileCountAmmo: chequea mínimo requerido para lanzar misil.
    protected override void Update()
    {
        if (GameManager.disableShoot == false)
        {
            CheckAmmoBar();
            OneShoot();
            if (canShoot && visualAmmo.fillAmount > 0 && currentAmmo >= maxAmmo / missileCountAmmo)
            {
                ray = _mainCam.ScreenPointToRay(Input.mousePosition);
                Shoot();
            }
        }
    }
    public override void Shoot()
    {
        base.Shoot();
        _soundManagerReference.PlaySound(K.SOUND_MISIL_LAUNCH);
        GameObject saw = PoolManager.instance.sawBlade.GetObject();

        if (myCollider.GetComponentInParent<VehicleData>() != null)
            saw.GetComponent<Ammo>().SetObjectParent(myCollider.GetComponentInParent<VehicleData>());

        if (saw.GetComponent<Collider>() != null && myCollider != null)
            Physics.IgnoreCollision(saw.GetComponent<Collider>(), myCollider);

        saw.transform.position = shootPoint.position;
        saw.transform.rotation = shootPoint.rotation;
        saw.GetComponent<SawBlade>().SetLayer(myLayer);
        saw.GetComponent<SawBlade>().SetPower(damage);
        saw.GetComponent<Rigidbody>().AddForce(ray.direction * ( speed * reach), ForceMode.Force);
        ray = new Ray();

        currentAmmo -= maxAmmo / missileCountAmmo;
    }

    private void CheckAmmoBar()
    {
        visualAmmo.GetComponentInParent<Canvas>().transform.LookAt(Camera.main.transform.position);
        float calc_ammo = currentAmmo / maxAmmo;
        visualAmmo.fillAmount = calc_ammo;
        ReloadAmmo();
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
