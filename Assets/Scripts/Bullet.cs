using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    public GameObject impactEffect;
    

    void Start()
    {
        rb.velocity = transform.right * speed;
        FindObjectOfType<AudioManager>().Play("Bow");
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
        

    //    Destroy(gameObject);
    //}

    void OnCollisionEnter2D(Collision2D other)
    {
        if ( other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("MovingPlatform") || other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    
    
}
