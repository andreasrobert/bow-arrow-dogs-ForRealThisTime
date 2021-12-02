using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    bool canEnter;
    public Animator animator;

    public GameObject platform;

    void Start()
    {
        
    }

    void Update()
    {
        if (canEnter)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //canEnter = false;
                FindObjectOfType<AudioManager>().Play("Lever");
                platform.GetComponent<MovingPlatform>().moveSpeed = 4;
                animator.SetTrigger("Switch");
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "DogPlayer")
        {
            canEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "DogPlayer")
        {
            canEnter = false;
        }
    }




}
