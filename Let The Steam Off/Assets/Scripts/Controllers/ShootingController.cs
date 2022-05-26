using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject bullet;

    public float bulletShootForce;
    public float grenadeBulletUpwardForce;

    public float spread, timeBetweenShots;
    public int bulletsPerShot;
    int bulletsShot;

    public bool allowButtonHold;

    bool shooting, readyToShoot;


    public Transform attackPoint;
    public Camera fpsCam;

    public bool allowInvoke = true;

    private InputManager inputManager;

    void Start()
    {
        inputManager = InputManager.Instance;
        readyToShoot = true;
    }

    private void Update()
    {
        if (allowButtonHold)
            shooting = inputManager.PlayerShooting();
        else
            shooting = inputManager.PlayerSingleShoot();

        if(readyToShoot && shooting)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        Vector3 directWithoutSpread = targetPoint - attackPoint.position;

        float xSpread = UnityEngine.Random.Range(-spread, spread);
        float ySpread = UnityEngine.Random.Range(-spread, spread);

        Vector3 directWithSpread = directWithoutSpread + new Vector3(xSpread, ySpread, 0);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directWithSpread.normalized * bulletShootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * grenadeBulletUpwardForce, ForceMode.Impulse);

        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke = false;
        }

        if (bulletsShot < bulletsPerShot)
            Invoke("Shoot", 0f);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
}
