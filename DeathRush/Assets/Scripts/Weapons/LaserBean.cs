using UnityEngine;
using System.Collections;

public class LaserBean : Weapon
{
    public short type;
    public GameObject particleEffect;
    private Collider _myCollider;
    // public AudioSource soundEffect;
    private bool _active;
    public GameObject[] destinRayFeedback;
    public Vector3 cent;

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

        cent = new Vector3(cent.x, cent.y, (reach / 2) + cent.z);
        _myCollider.GetComponent<CapsuleCollider>().center = cent;
        _myCollider.GetComponent<CapsuleCollider>().height = reach;

        Vector3 vect = new Vector3(0,0, reach);
        foreach (var item in destinRayFeedback)
        {
            item.transform.localPosition = vect;
        }
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
        else if (canShoot && type == 1)
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
        if (col.gameObject.layer == K.LAYER_IA && col.GetComponentInParent<IAVehicleData>() != null && Input.GetMouseButton(0))
        {
            print(col.gameObject);
            col.GetComponentInParent<IAVehicleData>().Damage(damage * 10, _owner);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == K.LAYER_IA && col.GetComponentInParent<IAVehicleData>() != null && Input.GetMouseButton(0))
        {
            col.GetComponentInParent<IAVehicleData>().Damage(damage, _owner);
        }
    }
}
