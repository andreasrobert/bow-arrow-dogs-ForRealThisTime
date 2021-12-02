using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    bool canEnter;

    public GameObject Player;

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
                FindObjectOfType<AudioManager>().Play("Key");
                Player.GetComponent<PlayerMovement>().key = true;
                Destroy(gameObject);
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
