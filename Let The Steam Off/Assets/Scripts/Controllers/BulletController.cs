using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class defines bullet's behavior when it hits other objects and how long bullet should exist before it will be destroyed.
/// </summary>
public class BulletController : MonoBehaviour
{
    private float bulletLifetime = 4;

    /// <summary>
    /// Script is checking how much bullet's lifetime left and if it hits 0 bullet will be destroyed.
    /// </summary>
    private void Update()
    {
        if (bulletLifetime > 0)
            bulletLifetime -= Time.deltaTime;
        if (bulletLifetime <= 0)
        Destroy(gameObject);
    }
    /// <summary>
    /// Script is checking on which GameObject we have hit with our bullet
    /// </summary>
    /// <param name="collision">
    /// This paremeter holds information for collision such as reference to GameObject we have hit.
    /// </param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
            // Place for kill mechanics
            Destroy(collision.gameObject);

        if (collision.gameObject.CompareTag("Player"))
            // Place for player death
            return;

        if (collision.gameObject.CompareTag("Bullet"))
            return;

        Destroy(gameObject);
    }
}
