using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public GameObject bulletPrefab, molotovPrefab, rocketLauncherPrefab, rocketLockPrefab, sawBladePrefab, fireFloor;
    public ObjectPool<Ammo> bullets, molotov, rocketLauncher, rocketLock, sawBlade, fire;

    private void Awake()
    {
        if (instance == null) instance = this;

        if (fireFloor == null) fireFloor = Resources.Load<GameObject>(K.PATH_FIREFLOOR);
        molotovPrefab = Resources.Load<GameObject>(K.PATH_MOLOTOV);


        bullets = new ObjectPool<Ammo>(() => Instantiate(bulletPrefab), K.TAG_AMMO_CONTAINER, 30);
        molotov = new ObjectPool<Ammo>(() => Instantiate(molotovPrefab), K.TAG_AMMO_CONTAINER, 10);
        fire = new ObjectPool<Ammo>(() => Instantiate(fireFloor), K.TAG_AMMO_CONTAINER, 10);
        rocketLauncher = new ObjectPool<Ammo>(() => Instantiate(rocketLauncherPrefab), K.TAG_AMMO_CONTAINER, 10);
        rocketLock = new ObjectPool<Ammo>(() => Instantiate(rocketLockPrefab), K.TAG_AMMO_CONTAINER, 10);
        sawBlade = new ObjectPool<Ammo>(() => Instantiate(sawBladePrefab), K.TAG_AMMO_CONTAINER, 15);
    }
}
