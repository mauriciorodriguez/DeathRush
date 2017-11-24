using UnityEngine;
using System.Collections;

public class GadgetLauncher : MonoBehaviour
{
    private GameObject _gadget;
    private WeaponsManager _myManager;
    public float cooldown;
    private float _currentCool;
    public float ammoCount;
    private bool _canShoot;

    void Start()
    {
        if (_myManager != null)
            return;
        else
            _myManager = this.GetComponentInParent<WeaponsManager>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (!GameManager.disableShoot)
        {
            if (!_canShoot)
            {
                _currentCool += Time.deltaTime;
                if (_currentCool >= cooldown)
                {
                    _currentCool = 0;
                    _canShoot = true;
                }
            }
            if (_canShoot && Input.GetKeyUp(KeyCode.Mouse2) && _gadget != null)
            {
                Shoot();
            }
        }
	}

    public void Shoot()
    {
        _canShoot = false;
        GameObject gadget = (GameObject)Instantiate(_gadget, this.transform.position + transform.forward, this.transform.rotation);
        gadget.GetComponent<Trap>().SetOwner(this.gameObject.GetComponentInParent<VehicleData>());
        //gadget.GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Force);
        _myManager.DropGadget(gadget);
    }

    public void SetGadget(GameObject gad, float ammos)
    {
        _gadget = gad;
        ammoCount = ammos;
    }

    public GameObject GetGadget()
    {
        return _gadget;
    }

    public void SetGadget(GameObject gad)
    {
        _gadget = gad;
    }
}
