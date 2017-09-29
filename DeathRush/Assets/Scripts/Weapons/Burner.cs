using UnityEngine;
using System.Collections;

public class Burner : Weapon
{
    //public float damage = 0.1f;
    public GameObject flames;
    public float duration;
    private bool activeFeed;
    private VehicleData _owner;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        isCrosshair = false;
        flames.SetActive(false);
        _ammoTimer = ammoTimer;

        _owner = this.gameObject.GetComponentInParent<VehicleData>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        CheckAmmoBar();
        ShootDownButtom();

        if (GameManager.disableShoot == false && activeShoot && shootButtom != 3)
        {

            if (Input.GetMouseButton(shootButtom) && visualAmmo.fillAmount > 0)
            {
                activeFeed = true;
                flames.SetActive(true);
                ammoInput();
            }
            if (Input.GetMouseButtonUp(shootButtom) || visualAmmo.fillAmount == 0)
            {
                activeFeed = false;
                flames.SetActive(false);
            }
            ShootDownButtom();
        }
    }

    public void ShootIA()
    {
        if (GameManager.disableShoot == false && shootButtom == 3 && visualAmmo.fillAmount > 0)
        {
            activeFeed = true;
            flames.SetActive(true);
            ammoInput();
            canShoot = true;
            Invoke("DisableShootIA", duration);
        }
    }

    public void DisableShootIA()
    {
        if (GameManager.disableShoot == false && shootButtom == 3)
        {
            activeFeed = false;
            flames.SetActive(false);
            visualAmmo.fillAmount = 1;
            canShoot = false;
        }
    }

    private void CheckAmmoBar()
    {
        visualAmmo.GetComponentInParent<Canvas>().transform.LookAt(Camera.main.transform.position);
        float calc_ammo = _ammoTimer / ammoTimer;
        visualAmmo.fillAmount = calc_ammo;

        if (visualAmmo.fillAmount <= 0) ammoEmpty = true;
    }

    private void ReloadAmmo()
    {
        if (visualAmmo == null) return;
        if (visualAmmo.fillAmount == 1)
        {
            ammoEmpty = false;
            _ammoTimer = ammoTimer;
        }
        else _ammoTimer += Time.deltaTime * reloadSpeed;
    }

    private void ammoInput()
    {
        _ammoTimer -= Time.deltaTime * 5f;
        if (shootButtom == 3 && _ammoTimer >= 0)
        {
            activeFeed = false;
            flames.SetActive(false);
            canShoot = false;

        }
    }


    void OnTriggerStay(Collider cols)
    {
        if (activeFeed)
        {
            if (canShoot)
            {
                if (cols.gameObject.layer == K.LAYER_IA || cols.gameObject.layer == K.LAYER_PLAYER)
                {
                    if (this.GetComponentInParent<VehicleData>() != cols.gameObject.GetComponentInParent<VehicleData>())
                    {
                        Shoot();
                        cols.gameObject.GetComponentInParent<VehicleData>().Damage(damage, gameObject.GetComponentInParent<VehicleData>());
                    }
                }
            }
        }
    }
    /*
    GameObject granade = (GameObject)GameObject.Instantiate(granadePrefab, transform.position + transform.forward * 3, Quaternion.identity);
    granade.transform.forward = transform.forward;
			granade.GetComponent<Rigidbody>().AddForce(transform.forward* 100, ForceMode.Force);*/
}
