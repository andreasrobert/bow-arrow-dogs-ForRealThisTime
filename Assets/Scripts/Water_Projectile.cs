using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Projectile : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;

    public Rigidbody2D rb;

    private Vector3 direction;

    public Animator animator;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Target").transform;
        direction = (player.position - transform.position).normalized;
        FindObjectOfType<AudioManager>().Play("WaterProjectile");
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("MovingPlatform") || other.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("Hit");
            DestroyProjectile();
        }
    }


    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    
}
