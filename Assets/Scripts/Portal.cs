using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject Portals;
    public GameObject Player;

    bool canEnter;

    void Start()
    {
        
    }

    void Update()
    {
        if (canEnter)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                canEnter = false;
                StartCoroutine(Teleport());
            }
        }


    }

    //public void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        StartCoroutine(Teleport());
    //    }       
    //}

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

    IEnumerator Teleport()
    {
        FindObjectOfType<AudioManager>().Play("Portal");
        yield return new WaitForSeconds(1f);
        Player.transform.position = new Vector2(Portals.transform.position.x, Portals.transform.position.y);
    }

}
