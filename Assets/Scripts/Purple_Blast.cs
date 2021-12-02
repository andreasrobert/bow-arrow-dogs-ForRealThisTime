using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_Blast : MonoBehaviour
{
    public float speed = 15f;

    private Transform player;
    private Vector2 target;

    public Rigidbody2D rb;

    private Vector3 direction;

    private Vector2 direct;

    public Animator animator;

    Vector3 where;

    bool otherWay = false;
    bool way;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Target").transform;
        direction = (player.position - transform.position).normalized;
        way = transform.position.x - player.transform.position.x < 0;
        FindObjectOfType<AudioManager>().Play("PurpleBlast");
    }

    void Update()
    {
        if(way)
        {
            goRight();
        }
        else
        {
            goLeft();
        }
    }

    void goRight()
    {
        if (transform.position.x - player.transform.position.x - 10 > 0)
        {
            otherWay = true;
        }

        if (otherWay)
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }

        else
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    void goLeft()
    {
        if (transform.position.x - player.transform.position.x + 10 < 0)
        {
            otherWay = true;
        }

        if (otherWay)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        else
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
    }

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
