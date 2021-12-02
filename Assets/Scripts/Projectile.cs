using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;

    public Rigidbody2D rb;

    private Vector3 direction;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Target").transform;
        //target = new Vector2(player.position.x , player.position.y);

        //transform.rotation = Quaternion.LookRotation(target);

        //transform.rotation = Quaternion.Euler(0,0,0) ;

        //target = (player.transform.position - transform.position).normalized * speed;

        //transform.LookAt(new Vector3(player.position.x, 0, player.position.z));

        direction = (player.position - transform.position).normalized;

        FindObjectOfType<AudioManager>().Play("Bow");

    }

    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);      // target = move to players last location(boom), player.position =  move to target(follow,!boom after time..!) 

        //if (transform.position.x == target.x && transform.position.y == target.y)
        //{
        //    DestroyProjectile();
        //}

        //rb.velocity = target * speed ;

        //rb.velocity = new Vector2(target.x, target.y);

        //transform.position += transform.right * speed * Time.deltaTime;

        transform.position += direction * speed * Time.deltaTime;

    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        DestroyProjectile();
    //    }
    //}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("MovingPlatform") || other.gameObject.CompareTag("Enemy"))
        {
            DestroyProjectile();
        }
    }


    void DestroyProjectile()
    {
        Destroy(gameObject);
    }


}
