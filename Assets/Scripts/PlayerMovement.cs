using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;

    public int heart;

    [Range(1, 15)]
    public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    private bool notStaggered = true;
    public float staggeredTime;
    private float staggeredTimeCounter;

    public float groundedFor;
    float lastTimeGrounded;

    private bool facingRight = true;

    public Animator animator;

    public bool timesUp = false;

    public bool yourTurn = true;

    public bool key = false;

   

    //bool notShooting = true;

    void start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
    }


    void Update()
    {
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, checkRadius, whatIsGround);

        //if (isGrounded == true)
        //{
        //    extraJumps = extraJumpsValue;
        //}

        //foreach(var c in colliders)
        //{
        //    if (c.tag == "MovingPlatform")
        //        transform.parent = c.transform;
        //    else
        //        transform.parent = null;
        //}

        //if (Input.GetButtonDown("Jump") && extraJumps > 0 )
        //{
        //    rb.velocity = Vector2.up * jumpVelocity;
        //    extraJumps--;
        //    jumpTimeCounter = jumpTime;
        //    isJumping = true;// this
        //}
        //if (Input.GetButton("Jump") && isJumping == true)
        //{
        //    if(jumpTimeCounter > 0)
        //    {
        //        rb.velocity = Vector2.up * jumpVelocity;
        //        jumpTimeCounter -= Time.deltaTime;
        //    }
        //    else
        //    {
        //        isJumping = false;  //this
        //    }
        //}
        //if(Input.GetButtonUp("Jump")) // this
        //{
        //    isJumping = false;
        //}

        //if (rb.velocity.y < 0)
        //{
        //    rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        //}
        //else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        //{
        //    rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        //}
        //else 

        //if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded == true)
        //{
        //    rb.velocity = Vector2.up * jumpVelocity;
        //}

        //move = Input.GetAxisRaw("Horizontal");

        //if (move > 0)
        //{
        //    gameObject.GetComponent<SpriteRenderer>().flipX = true;
        //    notStaggered = true;
        //}
        //else if (move < 0)
        //{
        //    gameObject.GetComponent<SpriteRenderer>().flipX = false;
        //    notStaggered = true;
        //}


        CheckIfGrounded();
        CheckTurn();

        if (yourTurn)
        {
            Jump();
        }

        FallJumpAnimation();

      

    }

    void FixedUpdate()
    {

        //if (move == 0)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x* airDrag, rb.velocity.y);
        //}

        //else 
        //if (notStaggered)
        //{
        //    rb.velocity = new Vector2(move * movementSpeed, rb.velocity.y); ;
        //}
        //ApplyMovement();

        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //if (notStaggered)
        //{
        //    Move();
        //}


        BetterJump();
        if (notStaggered && !timesUp)
        {
            if (yourTurn)
            {
                Move();
            }

            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                animator.SetFloat("Speed", 0);
            }
        }

        else
        {
            if(staggeredTimeCounter > 0)
            {
                staggeredTimeCounter -= Time.deltaTime;
            }

            else if(!timesUp)
            {
                notStaggered = true;
                Damaged();
            }
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * movementSpeed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(x));
        //animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (x > 0 && !facingRight)
        {
            //gameObject.GetComponent<SpriteRenderer>().flipX = true;
            //gameObject.transform.Rotate(0f, 180f, 0f);
            Flip();
        }
        else if (x < 0 && facingRight)
        {
            //gameObject.GetComponent<SpriteRenderer>().flipX = false;
            //gameObject.transform.Rotate(0f, 180f, 0f);
            Flip();
        }
    }
        
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && (isGrounded || Time.time - lastTimeGrounded <= groundedFor || extraJumps > 0) && notStaggered)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            extraJumps--;
            jumpTimeCounter = jumpTime;
            isJumping = true;
            FindObjectOfType<AudioManager>().Play("Jump");
            //animator.SetBool("IsJumping", true);

        }

        if (Input.GetButton("Jump") && isJumping == true && notStaggered)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpVelocity;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (collider != null)
        {
            isGrounded = true;
            extraJumps = extraJumpsValue;
            //animator.SetBool("IsJumping", false);

        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;

        }
    }

    void BetterJump()
    {
        if (rb.velocity.y <= 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

   

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = other.transform;
        }
        else
        {
            transform.parent = null;
        }

        if (other.gameObject.CompareTag("Spikes") || other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("Enemy"))
        {
            notStaggered = false;
            Vector2 direction = (other.transform.position - this.transform.position).normalized;

            staggeredTimeCounter = staggeredTime;
            if(direction.x > 0)
            {
                rb.velocity += new Vector2(-5, 8);
            }
            else
            {
                rb.velocity += new Vector2(5, 8);
            }

        }

        if (other.gameObject.CompareTag("Waters"))
        {
            Invoke("DieNow", 2f);
            notStaggered = false;
            AudioManager.instance.Play("DropDead");
        }
    }

    void Damaged()
    {
        if (!timesUp)
        {
            animator.SetTrigger("Hurt");
            gameObject.GetComponent<Health>().health--;
        }

        if (gameObject.GetComponent<Health>().health == 0)
        {
            timesUp = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Invoke("DieNow", 5f);
            animator.SetBool("Dying", true);
            animator.SetTrigger("Die");
            
        }
    }


    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameObject.GetComponent<Health>().health = 5;
    }

    void DieNow()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.instance.Play("DropDead");
    }


    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }


    //void FallJumpAnimation()
    //{
    //    float y = rb.velocity.y;
    //    if(y <= 0)
    //    {
    //        animator.SetFloat("Jump", y);

    //    }        
    //}



    void FallJumpAnimation()
    {
        float y = rb.velocity.y;

        if ( y == 0 && isGrounded)
        {
            animator.SetBool("GoingDown", false);
            animator.SetBool("GoingUp", false);
            animator.SetTrigger("Landed");
        }

        else if (y > 0 && !isGrounded)
        {
            animator.SetBool("GoingDown", false);
            animator.SetBool("GoingUp", true);
        }

        else if (y < 0 && !isGrounded)
        {
            animator.SetBool("GoingDown",true);
            animator.SetBool("GoingUp", false);        
        }        
    }


    void CheckTurn()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioManager.instance.Play("Switch");
            if (yourTurn)
            {
                yourTurn = false;
            }

            else if (!yourTurn)
            {
                yourTurn = true;
            }
        }
    }

    void WalkSound1()
    {
        AudioManager.instance.Play("WalkSound1");
    }

    void WalkSound2()
    {
        AudioManager.instance.Play("WalkSound2");
    }

    void DeathSound()
    {
        AudioManager.instance.Play("DropDead");
    }





    //void shooting()
    //{
    //    canMove = false;
    //    if(animator.getTri)
    //}







    //void OnTriggerEnter2D(Collider2D others)
    //{
    //    //if (others.CompareTag("Projectile"))
    //    //{
    //    //    notStaggered = false;
    //    //    //staggeredTime = staggeredTimeCounter;
    //    //    //Staggered();
    //    //    Debug.Log("sksk");
    //    //    //Debug.Log(others.transform.position);
    //    //    staggeredTimeCounter = staggeredTime;
    //    //    rb.velocity += new Vector2(5, 10);

    //    //}


    //}




}
