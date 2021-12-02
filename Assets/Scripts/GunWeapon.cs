using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : MonoBehaviour
{
    public Transform rightPoint;

    public GameObject bullet;

    public Animator animator;

    public float shootCooldown = 0.6f;
    float shootTimer = 0;
    bool canShoot = true;

    PlayerMovement ground;

    public bool canMove;

    public Rigidbody2D rb;

    

    void Start()
    {
        ground = GetComponent<PlayerMovement>();
        canMove = true;      
    }

    void Update()
    {
        if (!canShoot)
        {
            shootTimer += Time.deltaTime;

            if(shootTimer >= shootCooldown)
            {
                shootTimer = 0;
                canShoot = true;
                animator.SetBool("CanShoot", true);
            }
        }

        if(Input.GetButtonDown("Fire1") && ground.yourTurn)
        {
            ShootNow();

            //if (ground.isGrounded)
            //{
            //    animator.SetTrigger("Shoot");
            //}
            //else
            //{
            //    ShootNow();
            //}
        }

        if (!canMove)
        {
            //gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (canMove && !ground.timesUp)
        {
            //rb.bodyType = RigidbodyType2D.Dynamic;
            //rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        }
    }

    void ShootNow()
    {
        if (ground.isGrounded && canShoot)
        {
            canMove = false;
            animator.SetTrigger("Shoot");
        }

        else if (canShoot)
        {
            Instantiate(bullet, rightPoint.position, rightPoint.rotation);
            canShoot = false;
        }       
    }

    void Shoot()
    {
            Instantiate(bullet, rightPoint.position, rightPoint.rotation);
            canShoot = false;
            canMove = true;
            animator.SetBool("CanShoot", false);
    }

}
