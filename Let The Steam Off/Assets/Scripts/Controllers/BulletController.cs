using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hitbox")
            // Place for kill mechanics
            Destroy(collision.gameObject);

        if (collision.gameObject.tag == "Player")
            // Place for death
            return;

        if (collision.gameObject.tag == "Bullet")
            return;

        Destroy(gameObject);
    }
}
