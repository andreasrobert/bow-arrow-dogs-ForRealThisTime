using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPortal : MonoBehaviour
{
    bool canEnter;
    public Animator animator;

    public GameObject Green;
    public GameObject Purple;

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
                animator.SetTrigger("Switch");
                Vector2 temp = new Vector2(Purple.transform.position.x, Purple.transform.position.y);
                Purple.transform.position = new Vector2(Green.transform.position.x, Green.transform.position.y);
                Green.transform.position = temp;
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
