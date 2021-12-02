using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7 : MonoBehaviour
{
    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform player;

    public float lineOfSight;

    public Animator animator;

    private Vector3 direction;

    private bool facingRight = true;

    public int lives = 25;

    public Transform rightPoint;

    public Rigidbody2D rb;

    float shootCooldown = 3f;
    float shootTimer = 0;
    bool canShoot = true;

    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public List<Transform> points;
    public Transform path;
    int goalPoint = 0;
    public float moveSpeed;

    bool dying = false;

    float IdleCooldown = 4f;
    float IdleTimer = 0;
    bool goNext = true;
    bool stoped = false;

    bool lastLook = true;

    public GameObject Blast;
    public Transform castPoint;
    


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (path.transform.position.x - points[goalPoint].transform.position.x < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        canIShoot();                   // shoot timer

        Movement();

        float distance = Vector2.Distance(player.position, transform.position);
        if (distance < lineOfSight && canShoot)
            //if (canShoot)
        {
            shootWhen();              // for shooting
        }

        //PurpleBlast();
    }

    void FixedUpdate()
    {

    }

    void Movement()
    {
        if (!dying)
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));       // for idle and running animation

            //PlayerLook();                  // agro state
            MoveToNextPoint();             // patrol / complex agro
        }

        else if (dying)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }



    void shootWhen()
    {
        canShoot = false;
        animator.SetTrigger("Shoot");
    }


    void canIShoot()
    {
        if (!canShoot)
        {
            shootTimer += Time.deltaTime;

            if (shootTimer >= shootCooldown)
            {
                shootTimer = 0;
                canShoot = true;
            }
        }
    }


    void canIGo()
    {
        if (!goNext)
        {
            IdleTimer += Time.deltaTime;

            if (IdleTimer >= IdleCooldown)
            {
                IdleTimer = 0;
                goNext = true;
                stoped = false;
            }
        }
    }


    void Shoot()
    {
        Instantiate(Blast, rightPoint.position, Quaternion.identity);
    }

    void PlayerLook()
    {
        if (transform.position.x > player.position.x && !facingRight)
        {
            Flip();
            lastLook = !lastLook;
        }
        else if (transform.position.x < player.position.x && facingRight)
        {
            Flip();
            lastLook = !lastLook;
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
                dying = true;
                animator.SetTrigger("Dying");
            }
        }
    }



    void MoveToNextPoint()
    {
        //path.position = Vector2.MoveTowards(path.position, points[goalPoint].position, Time.deltaTime * moveSpeed);
        float temp = path.transform.position.x - points[goalPoint].transform.position.x;
        float distances = Mathf.Abs(temp);



        if (goNext)
        {
            if (!lastLook)
            {
                lastLook = true;
                Flip();
            }

            if (temp > 0)
            {
                rb.velocity = new Vector2(-7f, rb.velocity.y);
            }

            else if (temp < 0)
            {
                rb.velocity = new Vector2(7f, rb.velocity.y);
            }
        }


        //if (Vector2.Distance(path.position , points[goalPoint].position) < 0.1f)

        if (distances < 0.1f)
        {
            goNext = false;
            stoped = true;
            rb.velocity = new Vector2(0, 0);


            Flip();
            if (goalPoint == points.Count - 1)
            {
                goalPoint = 0;
            }

            else
            {
                goalPoint++;
            }
        }

        if (stoped)
        {
            canIGo();
            PlayerLook();
        }
    }


    void PurpleBlast()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Instantiate(Blast, rightPoint.position, Quaternion.identity);         
        }
    }

    void DeathSound()
    {
        AudioManager.instance.Play("PurpleDeath");
    }

   

}
