using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class defines bullet's behavior when it hits other objects and how long bullet should exist before it will be destroyed.
/// </summary>
public class BulletController : MonoBehaviour
{

    private Rigidbody rb;
    private float timeRemaining = 4;
    /// <summary>
    /// Start is called before first frame.
    /// Here we are getting reference to bullet's rigidbody component.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// Here we are checking how much bullet's lifetime left and if it hits 0 bullet will be destroyed.
    /// </summary>
    private void Update()
    {
        if (timeRemaining > 0)
            timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        Destroy(gameObject);
    }
    /// <summary>
    /// Here we are executing script depending on which GameObject we have hit with our bullet
    /// </summary>
    /// <param name="collision">
    /// This paremeter holds information for collision such as reference to GameObject we have hit.
    /// </param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hitbox")
            // Place for kill mechanics
            Destroy(collision.gameObject);

        if (collision.gameObject.tag == "Player")
            // Place for player death
            return;

        if (collision.gameObject.tag == "Bullet")
            return;

        Destroy(gameObject);
    }
}
