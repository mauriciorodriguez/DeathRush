using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class IAVehicleData : VehicleData
{
    public GameObject hpBarContainer;
    public RawImage hpBarImage;
    public Weapon myWeapon;
    public Weapon secondWeapon;
    public GameObject primaryWeaponFeedback;
    public GameObject eyes;
    public GameObject mineDrop;
    public Transform dropPoss;

    private float _dropTimer;
    public float timerMines;

    public float clip;
    public float _usedBullets;
    public float cooldownShoot;
    private float _currentCool;
    public bool _activeShoot;
    private bool _enemyInSight;
    public bool _attacking;

    private Vector3 _aimShoot;
    private int result;

    private bool _enableAttack = true;
    public float duration;

    //  private List<IObserver> _observers;

    //private float _currentHp;
    private Vector3 _aux;



    public enum TypeWeapon { Turret, MissileLuncher }

    public TypeWeapon weaponType;

    protected override void Start()
    {
        //_observers = new List<IObserver>();
        base.Start();
        eyes.SetActive(false);
     //   primaryWeaponFeedback.SetActive(false);
        //_maxHp = K.IA_MAX_HP;
        //_currentHp = _maxHp;
        _aux = hpBarImage.transform.localScale;
        currentLife = maxLife;



    }

    protected override void Update()
    {
        // UpdateHpBar();
        hpBarContainer.transform.LookAt(Camera.main.transform.position);
        if (!GameManager.disableShoot)
        {
            if (_enableAttack)
            {
                if (!_activeShoot)
                {
                    _currentCool += Time.deltaTime;
                    if (_currentCool >= cooldownShoot)
                    {
                        eyes.SetActive(true);
                        _activeShoot = true;
                    }
                }

                if (_activeShoot && _enemyInSight && _aimShoot != null)
                {
                    _attacking = true;
                    result = UnityEngine.Random.Range(0, 100);

                    Attack();
                }


                if (_attacking && primaryWeaponFeedback.activeSelf == false)
                    primaryWeaponFeedback.SetActive(true);

                _dropTimer += Time.deltaTime;

                if (_dropTimer >= timerMines)
                {
                    Instantiate(mineDrop, dropPoss.transform.position, dropPoss.transform.rotation);
                    _dropTimer = 0;
                    timerMines += timerMines * 0.25f;
                }
            }

        }
    }
    public void StopAttacks()
    {
        _enableAttack = false;
        Invoke("ResetEPM", duration);
    } 

    void ResetEPM()
    {
        _enableAttack = true;
    }

    public override void Damage(float damageTaken, VehicleData atk)
    {
        base.Damage(damageTaken, atk);
        if(!_alive)
        {
            GetComponent<VehicleIAController>().Die();
            //Destroy(this.gameObject);
        }
    }
    public override void CheckHealthBar(bool hasCured)
    {
        base.CheckHealthBar(hasCured);
        _aux.x = currentLife / maxLife;
        hpBarImage.transform.localScale = _aux;
    }

    void Attack()
    {
        if (myWeapon != null)
        {

            if (result > 30 && result != 0)
            {
                ShootPrimary();
                
            }
            else if (secondWeapon != null)
            {
                if (secondWeapon != null && secondWeapon.GetComponent<Burner>() != null)
                {
                    secondWeapon.GetComponent<Burner>().ShootIA();    
                }
            }
        }
    }

    void ShootPrimary()
    {
        if (weaponType == TypeWeapon.Turret)
        {
            myWeapon.transform.gameObject.transform.LookAt(_aimShoot);

            myWeapon.Shoot();
            _enemyInSight = false;
            eyes.SetActive(false);
            if (clip > _usedBullets)
            {
                _usedBullets++;
                Invoke("Attack", 0.25f);
            }
            else
            {
                _currentCool = 0;
                _attacking = false;
                _activeShoot = false;
                _usedBullets = 0;
                primaryWeaponFeedback.SetActive(false);
                result = 0;
            }

        }
        else if (weaponType == TypeWeapon.MissileLuncher)
        {
            var weap = myWeapon.GetComponent<RockedLauncherMK2>();
            Ray ray = new Ray(this.gameObject.transform.position, _aimShoot.normalized);
            var rays = Physics.RaycastAll(ray, Mathf.Infinity);
            foreach (var item in rays)
            {
                if (item.collider.gameObject.layer == K.LAYER_GROUND || item.collider.gameObject.layer == K.LAYER_SIDEGROUND || item.collider.gameObject.layer == K.LAYER_OBSTACLE)
                {
                    weap._pointAttack = item.point;
                    //         print(currentAmmo % 3  + " asdfa " + maxAmmo / missileCountAmmo);
                    //if (weap.visualAmmo.fillAmount > 0 && weap.currentAmmo >= weap.maxAmmo / weap.missileCountAmmo)
                    weap.Shoot();
                    _currentCool = 0;
                    _attacking = false;
                    _activeShoot = false;
                    _enemyInSight = false;
                    eyes.SetActive(false);
                    result = 0;
                    return;
                }
            }

        }

    }

    //private void UpdateHpBar()
    //{
    //    hpBarContainer.transform.LookAt(Camera.main.transform.position);
    //    _aux.x = _currentHp / _maxHp;
    //    hpBarImage.transform.localScale = _aux;
    //}


    //public void Damage(float d)
    //{
    //    _currentHp -= d;

    //    if (_currentHp <= 0)
    //    {
    //        _soundManagerReference.PlaySound(K.SOUND_CAR_DESTROY);
    //        NotifyObserver(K.OBS_MESSAGE_DESTROYED);
    //        Destroy(this.gameObject);
    //        Instantiate(remains, transform.position, transform.rotation);

    //    }
    //}

    public void EnemySee(Vector3 vecto)
    {
        _enemyInSight = true;
        _aimShoot = vecto;
    }
    /*
    public void AddObserver(IObserver obs)
    {
        if (!_observers.Contains(obs)) _observers.Add(obs);
    }

    public void NotifyObserver(string msg)
    {
        foreach (var obs in _observers)
        {
            obs.Notify(this.gameObject.GetComponent<Vehicle>(), msg);
        }
    }

    public void RemoveObserver(IObserver obs)
    {
        if (_observers.Contains(obs)) _observers.Remove(obs);
    }

    */
    /*
    public override void GetInput(float _accel, float _brake,float _handbrake, float _steer, float _nitro)
    {
    }

    private void ApplyDrive()
    {
        CalculateSpeed();
        transform.forward = Vector3.Slerp(transform.forward, _nextDestinationPoint - transform.position, K.IA_TURN_SPEED * Time.deltaTime);
        transform.position += transform.forward * _currentSpeed * Time.deltaTime;
    }

    private void CalculateSpeed()
    {
        if (lapCount + (_checkpointMananagerReference.checkpointValue) < ((JeepController)_gameManagerReference.playerReference).lapCount)
        {
            _currentSpeed += 0.15f;
            _currentSpeed = Mathf.Clamp(_currentSpeed, 1, _maxSpeed);

        }
        else if (lapCount - (_checkpointMananagerReference.checkpointValue) > ((JeepController)_gameManagerReference.playerReference).lapCount)
        {
            _currentSpeed -= 0.3f;
            _currentSpeed = Mathf.Clamp(_currentSpeed, _maxSpeed / 3, _maxSpeed);
        }
        else
        {
            _currentSpeed += 0.1f;
            _currentSpeed = Mathf.Clamp(_currentSpeed, 1, _maxSpeed);

        }
    }

    private void CalculateNextCheckpoint(Checkpoint chk)
    {
        _nextCheckpoint = chk.nextCheckpoint;
    }
    
    
    /// <summary>
    /// Tomo el proximo checkpoint y calculo un punto aleatorio dentro del mismo, si hay un obstaculo vuelvo a calcular. 
    /// </summary>
    /// <param name="chk">Proximo Checkpoint</param>
    private void CalculateNextPoint(Checkpoint chk)
    {
        //_nextDestinationPoint = chk.GetRandomPositionFromNode();
        _nextDestinationPoint = Vector3.zero;
        var randomPoint = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        randomPoint = chk.transform.TransformPoint(randomPoint * 0.5f);
        randomPoint.y = 200;
        Ray ray = new Ray(randomPoint, -Vector3.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.layer == K.LAYER_GROUND)
            {
                _nextDestinationPoint = hit.point + Vector3.up;
                return;
            }
        }
        CalculateNextPoint(chk);
    }
    
    protected void CheckIfGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        RaycastHit hit;
        _isGrounded = false;
        _isGroundedRamp = false;
        if (Physics.Raycast(ray, out hit, 1))
        {
            if (hit.collider.gameObject.layer == K.LAYER_GROUND || hit.collider.gameObject.layer == K.LAYER_RAMP)
            {
                _isGrounded = true;
                if (hit.collider.gameObject.layer == K.LAYER_RAMP) _isGroundedRamp = true;
            }
        }
    }

    protected void FallSpeed()
    {
        if (!_isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(-Vector3.up * K.IA_FALLFORCE);
        }
    }*/

}
