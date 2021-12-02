using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DogMovement : MonoBehaviour
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

    public bool yourTurn = false;

    public bool key = false;

    void start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
    }


    void Update()
    {
        
        CheckTurn();
        CheckIfGrounded();

        if (yourTurn)
        {
            Jump();
        }

        FallJumpAnimation();

    }

    void FixedUpdate()
    {

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
            if (staggeredTimeCounter > 0)
            {
                staggeredTimeCounter -= Time.deltaTime;
            }

            else if (!timesUp)
            {
                notStaggered = true;
                //Damaged();
            }

        }

    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * movementSpeed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(x));

        if (x > 0 && !facingRight)
        {
            Flip();
        }

        else if (x < 0 && facingRight)
        {
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
            AudioManager.instance.Play("Jump");

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

        if (other.gameObject.CompareTag("Spikes") || other.gameObject.CompareTag("Projectile"))
        {
            notStaggered = false;
            Vector2 direction = (other.transform.position - this.transform.position).normalized;

            staggeredTimeCounter = staggeredTime;
            if (direction.x > 0)
            {
                rb.velocity += new Vector2(-5, 8);
            }
            else
            {
                rb.velocity += new Vector2(5, 8);
            }

        }
    }

    //void Damaged()
    //{
    //    if (!timesUp)
    //    {
    //        animator.SetTrigger("Hurt");
    //        gameObject.GetComponent<Health>().health--;
    //    }

    //    if (gameObject.GetComponent<Health>().health == 0)
    //    {
    //        timesUp = true;
    //        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    //        animator.SetTrigger("Die");
    //    }

    //}


    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameObject.GetComponent<Health>().health = 5;
    }


    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }



    void FallJumpAnimation()
    {
        float y = rb.velocity.y;

        if (y == 0 && isGrounded)
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
            animator.SetBool("GoingDown", true);
            animator.SetBool("GoingUp", false);
        }


    }

    void CheckTurn()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
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

  



}
