using UnityEngine;
using System.Collections;

public class MolotovLauncher : Weapon
{
    public float angleView;
    public GameObject bomb;
  //  public Transform launchPoint;
    public Camera mainCamera;
    private VehicleData _owner;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        _owner = this.gameObject.GetComponentInParent<VehicleData>();
        isCrosshair = true;
        currentAmmo = maxAmmo = visualAmmo.fillAmount;
    }
	
	// Update is called once per frame
	protected override void Update ()
    {
        if (GameManager.disableShoot == false)
        {
            CheckAmmoBar();
            OneShoot();
            if (canShoot && currentAmmo >= maxAmmo / missileCountAmmo) Shoot();
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Shoot()
    {
        base.Shoot();
        _soundManagerReference.PlaySound(K.SOUND_MOLOTOV_LAUNCH);
        /*GameObject granade = PoolManager.instance.molotov.GetObject();
        granade.transform.position = launchPoint.position + launchPoint.forward * 2;
        granade.transform.rotation = Quaternion.identity;*/

        GameObject granade = PoolManager.instance.molotov.GetObject();
        granade.transform.position = shootPoint.position + shootPoint.forward * 2;
        granade.transform.rotation = shootPoint.rotation;
        //granade.GetComponent<MolotovBomb>().ActiveMolotov(); //cambio
        granade.GetComponent<Rigidbody>().AddForce(transform.forward * 500, ForceMode.Impulse);
        granade.GetComponent<Molotov>().SetObjectParent(_owner);
        currentAmmo -= maxAmmo / missileCountAmmo;
    }

    private void CheckAmmoBar()
    {
        visualAmmo.GetComponentInParent<Canvas>().transform.LookAt(Camera.main.transform.position);
        float calc_ammo = currentAmmo / maxAmmo;
        visualAmmo.fillAmount = calc_ammo;
        ReloadAmmo();

      //  if (visualAmmo.fillAmount == 0) ammoEmpty = true;
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
