using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class VehicleData : MonoBehaviour
{
    public Country.NamesType country;
    public RacerData.Gender gender;
    public float maxLife;
    public float currentLife;
    public GameObject remainsCar;
    public GameObject explosion;
    protected SoundManager _soundManagerReference;
    protected bool _alive;
    public ParticleSystem whiteSmoke;
    public ParticleSystem blackSmoke;
    public ParticleSystem fire;
    [HideInInspector]
    public bool canUseFenix, canUseOnTheEdge, damageControlUsed, damageControlSaved;
    [HideInInspector]
    public IngameUIManager uiRef;

    protected bool _onTheEdgeUsed;
    protected RacerData _racerData;

    private float _totalDamageDone = 0;

    private float _affectedStacks;
    private float _timerXFreeze;
    private float _cooldownUnfreeze = 5f;
    private bool _freezed;
    protected float _damageControlTimer;

    protected virtual void Start()
    {
        currentLife = maxLife;
        _alive = true;
        _soundManagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<SoundManager>();
    }

    protected virtual void Update()
    {
        if (damageControlUsed && !damageControlSaved)
        {
            _damageControlTimer -= Time.deltaTime;
            uiRef.showDamagecontrolCountdown = true;
            uiRef.SetDamageControlCountdownText(_damageControlTimer);
            if (_damageControlTimer <= 0) currentLife = -1;
        }
    }

    /*   public virtual void Damage(float damageTak)
       {
           Damage(damageTak, this);
       }*/

    public float GetMyDamageDone() { return _totalDamageDone; }

    public void IncreaseDamage(float dam) { _totalDamageDone += dam; }

    private void FreezeTimer()
    {
        if (_timerXFreeze >= 0)
        {
            _timerXFreeze -= Time.deltaTime;
            if (_freezed && _timerXFreeze <= 0)
            {
                _freezed = false;
                _timerXFreeze = -1;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            else if (_affectedStacks > 0 && _timerXFreeze <= 0)
            {
                _affectedStacks--;
                if (_affectedStacks <= 0) _timerXFreeze = -1;
                else _timerXFreeze = _cooldownUnfreeze;
            }

        }
    }

    public virtual void FreezeMovement()
    {
        if (!_freezed)
        {
            _affectedStacks++;
            if (_affectedStacks >= 5)
            {
                _freezed = true;
                _affectedStacks = 0;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                _timerXFreeze = _cooldownUnfreeze;
            }
            else _timerXFreeze = _cooldownUnfreeze;
        }
    }

    public virtual void Damage(float damageTaken, VehicleData atkcer)
    {
        if (_alive)
        {
            currentLife -= damageTaken;

            if (atkcer != null && atkcer != this) atkcer.IncreaseDamage(damageTaken);

            if (currentLife <= 0 && canUseFenix)
            {
                currentLife = maxLife * .3f;
                canUseFenix = false;
                return;
            }
            if (currentLife <= (maxLife * .3f) && canUseOnTheEdge)
            {
                if (!_onTheEdgeUsed)
                {
                    foreach (var weapon in GetComponent<Vehicle>().weaponManager.GetComponentsInChildren<Weapon>())
                    {
                        weapon.damage = weapon.damage * 2;
                    }
                    _onTheEdgeUsed = true;
                }
            }
            else if (currentLife >= (maxLife * .3f) && canUseOnTheEdge)
            {
                if (_onTheEdgeUsed)
                {
                    foreach (var weapon in GetComponent<Vehicle>().weaponManager.GetComponentsInChildren<Weapon>())
                    {
                        weapon.damage = weapon.damage / 2;
                    }
                    _onTheEdgeUsed = false;
                }
            }
            
            CheckHealthBar(false);
            if (currentLife <= 0)
            {
                if (atkcer != null && atkcer != this)
                {
                    PersistendData.instance.KillInfo(atkcer, this);
                }

                _alive = false;
                Instantiate(explosion, transform.position + transform.up, transform.rotation);
                _soundManagerReference.PlaySound(K.SOUND_CAR_DESTROY);
                Instantiate(remainsCar, transform.position + transform.up, transform.rotation);
                GetComponent<Vehicle>().Die();
            }
        }
    }

    public virtual void CheckHealthBar(bool hasCured)
    {

    }

}
