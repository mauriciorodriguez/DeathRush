using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    public float shootRate = 2.5f;
    public float xOffset, zOffset;
    public float power = 49f;
    public Transform weaponToSpin, shootPoint;

    private float _timer;
    private Transform _target;
    private Vector3 positionToLook;
    private bool _canCalculateNewPosition = true;

    public void SetTarget(Transform tar)
    {
        _target = tar;
        /*Vector3 dir = target.position - weaponToSpin.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        weaponToSpin.transform.rotation = Quaternion.LookRotation(dir);

        if (cooldown > 0f)
        {
            Shoot();
            cooldown -= Time.deltaTime * 5;
        }*/
    }

    private void LookAtTarget()
    {
        if (!_target) return;
        weaponToSpin.LookAt(_target);
        if (!_canCalculateNewPosition) return;
        positionToLook = _target.transform.position;
        positionToLook.x += Random.Range(-xOffset, xOffset);
        positionToLook.z += Random.Range(0, zOffset);
        shootPoint.LookAt(positionToLook);
        _canCalculateNewPosition = false;
    }

    void Shoot()
    {/*
        GameObject bullet = PoolManager.instance.bullets.GetObject();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;*/

        GameObject rock = PoolManager.instance.rocketLauncher.GetObject();
        rock.GetComponent<Rocket>().SetTarget(_target.position, power); //cambio

        rock.transform.position = shootPoint.position;
        rock.transform.rotation = shootPoint.rotation;

        _canCalculateNewPosition = true;
        /*var dir = shootPoint.TransformDirection(Vector3.forward);
        GameObject bala = (GameObject)Instantiate(bulletPrefab, shootPoint.position + shootPoint.forward, shootPoint.rotation);

        GameObject bala = PoolManager.instance.bullets.GetObject();
        bala.transform.position = shootPoint.position + shootPoint.forward;
        bala.transform.rotation = shootPoint.rotation;*/
    }

    private void Update()
    {
        LookAtTarget();
        if (_timer <= 0)
        {
            if (_target)
            {
                Shoot();
                _timer = shootRate;
            }
        }
        else _timer -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (_target) Gizmos.DrawLine(shootPoint.position, positionToLook);
    }
}
