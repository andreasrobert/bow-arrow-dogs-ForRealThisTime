using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform player;

    public float lineOfSight;

    public Animator animator;

    private Vector3 direction;

    private bool facingRight = true;

    public int lives = 3;

    public Transform rightPoint;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        PlayerLook();

        float distance = Vector2.Distance(player.position, transform.position);
        if(distance < lineOfSight)
        {
            shootWhen();
        }
    }

    void shootWhen()
    {
        animator.SetTrigger("Shoot");
    }


    void Shoot()
    {
        direction = (player.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 

        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle ));

        Instantiate(projectile, rightPoint.position, rot);

        //Instantiate(projectile, transform.position, Quaternion.identity);

    }

    void PlayerLook()
    {
        if (transform.position.x > player.position.x && !facingRight)
        {
            //gameObject.GetComponent<SpriteRenderer>().flipX = true;
            //gameObject.transform.Rotate(0f, 180f, 0f);
            Flip();
        }
        else if (transform.position.x < player.position.x && facingRight)
        {
            //gameObject.GetComponent<SpriteRenderer>().flipX = false;
            //gameObject.transform.Rotate(0f, 180f, 0f);
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void death()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MyProjectile"))
        {
            lives -= 1;
            if (lives == 0)
            {
                animator.SetTrigger("Dying");
            }
        }
    }

    void DeathSound()
    {
        AudioManager.instance.Play("DropDead");
    }


    //void Shoot()
    //{
    //    if (timeBtwShots <= 0)
    //    {
    //        Instantiate(projectile, transform.position, Quaternion.identity);
    //        timeBtwShots = startTimeBtwShots;

    //    }
    //    else
    //    {
    //        timeBtwShots -= Time.deltaTime;
    //    }
    //}
}
