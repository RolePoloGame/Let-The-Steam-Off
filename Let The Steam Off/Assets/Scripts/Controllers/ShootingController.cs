using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is a shooting controller in which we determines in which direction bullet should fly, how fast player can shoot, how many bullets should be shot at single tap.
/// </summary>
public class ShootingController : MonoBehaviour
{
    private int bulletsShot;
    private bool shooting;
    private bool readyToShoot;
    private bool allowInvoke = true;
    private Vector3 middleScreenPoint = new Vector3(0.5f, 0.5f, 0);
    private float defaultTargetRange = 75;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float bulletShootForce;
    [SerializeField]
    private float spread;
    [SerializeField]
    private float timeBetweenShots;
    [SerializeField]
    private int bulletsPerShot;
    [SerializeField]
    private bool allowButtonHold;
    [SerializeField]
    public Transform attackPoint;
    [SerializeField]
    private Camera fpsCam;

    private InputManager inputManager;


    /// <summary>
    /// We require InputManager to get information about mouse input.
    /// We also set readyToShoot to true, to make sure player will be able to shoot after start of the game.
    /// </summary>
    void Start()
    {
        inputManager = PAR.Get.GetInputManager();
        readyToShoot = true;
    }
    /// <summary>
    /// Script checks if player's current weapon is fully-automatic or semi-automatic and if player is pressing button to shoot.
    /// </summary>
    private void Update()
    {
        if (!readyToShoot)
            return;

        shooting = inputManager.GetPlayerShooting(allowButtonHold);

        if(shooting)
        {
            bulletsShot = 0;
            Shoot();
        }
    }
    /// <summary>
    /// This method create bullet object and adds force to it.
    /// Also it is responsible for how many bullets will be created with single trigger and how long player must wait before he can perform another shot.
    /// </summary>
    private void Shoot()
    {
        readyToShoot = false;

        Vector3 directWithSpread = CalculateBulletDirection();

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directWithSpread.normalized * bulletShootForce, ForceMode.Impulse);

        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke = false;
        }

        if (bulletsShot < bulletsPerShot)
            Shoot();
    }
    /// <summary>
    /// This method adds spread to bullet direction
    /// </summary>
    /// <param name="directWithoutSpread">
    /// It is bullets direction to place where player is aiming.
    /// </param>
    /// <returns>
    /// Direction to place where player is aiming with added bullets spread, so weapon is never 100% accurate.
    /// </returns>
    private Vector3 AddSpreadToBulletDirection(Vector3 directWithoutSpread)
    {
        float xSpread = UnityEngine.Random.Range(-spread, spread);
        float ySpread = UnityEngine.Random.Range(-spread, spread);

        Vector3 directWithSpread = directWithoutSpread + new Vector3(xSpread, ySpread, 0);
        return directWithSpread;
    }

    /// <summary>
    /// This method calculate bullet direction depending on player's camera raycast.
    /// </summary>
    /// <returns>
    /// Direction from bullet spawn point to place where player's camera raycast hits first target on its way.
    /// </returns>
    private Vector3 CalculateBulletDirection()
    {
        Ray ray = fpsCam.ViewportPointToRay(middleScreenPoint);
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(defaultTargetRange);

        Vector3 directWithoutSpread = targetPoint - attackPoint.position;
        return AddSpreadToBulletDirection(directWithoutSpread);
    }

    /// <summary>
    /// This method resets player's ability to shoot. Without it player won't be able to make another shot's.
    /// </summary>
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
}
