using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    bool canEnter;
    public Animator animator;
    public GameObject player;

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
                FindObjectOfType<AudioManager>().Play("Gate");
                if (player.GetComponent<PlayerMovement>().key)
                {
                    animator.SetBool("Gate", true);
                    AudioManager.instance.StopPlaying("Theme");
                }
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

    void finish()
    {        
        AudioManager.instance.Play("Win");
        Invoke("menuScene",3.2f);
    }

    void menuScene()
    {
        SceneManager.LoadScene("RealMenu");
    }

}
