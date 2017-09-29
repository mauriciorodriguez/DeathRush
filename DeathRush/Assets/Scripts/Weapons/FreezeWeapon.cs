using UnityEngine;
using System.Collections;

public class FreezeWeapon : Weapon
{
    public short type;
    public GameObject particleEffect;
    private Collider _myCollider;
    public Vector3 cent;
    // public AudioSource soundEffect;
    private bool _active;

    // Use this for initialization
    void Awake()
    {
        if (GetComponent<Collider>() != null)
        {
            _myCollider = GetComponent<Collider>();
            _myCollider.enabled = false;
        }
    }

    protected override void Start()
    {
        base.Start();
        isCrosshair = false;
        particleEffect.SetActive(false);
        _ammoTimer = ammoTimer;

        cent = new Vector3(cent.x, cent.y, (reach/2) + 1f);
        _myCollider.GetComponent<CapsuleCollider>().center = cent;
        _myCollider.GetComponent<CapsuleCollider>().height = reach;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (GameManager.disableShoot == false && type == 0)
        {
            CheckAmmoBar();
            ShootDownButtom();

            if (canShoot && visualAmmo.fillAmount > 0 && _isShooting)
                Shoot();
            else if (!_isShooting)
                particleEffect.SetActive(false);
        }
        else  if (canShoot && type == 1)
            Shoot();
        else
            particleEffect.SetActive(false);

        base.Update();

        if (!_isShooting) ReloadAmmo();
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
    }

    public override void Shoot()
    {
        _active = true;
        ammoEmpty = false;
        particleEffect.SetActive(true);
        _myCollider.enabled = true;
        if (type == 0)
        {
            ammoInput();
        }
        base.Shoot();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == K.LAYER_IA && col.GetComponentInParent<IAVehicleData>() != null)
        {
            print(col.gameObject.transform.parent.gameObject.name);
            _myCollider.enabled = false;
            col.GetComponentInParent<IAVehicleData>().FreezeMovement();
        }
    }
}
