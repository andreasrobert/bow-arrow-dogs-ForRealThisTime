using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level4 : MonoBehaviour
{

    public GameObject enemy1;
    public GameObject enemy2;
    bool done = true;


    void Start()
    {
        
    }

    void Update()
    {
        if (enemy1 == null && enemy2 == null && done)
        {
            done = false;
            finish();
        }

    }

    void finish()
    {
        AudioManager.instance.StopPlaying("Theme");
        AudioManager.instance.Play("Win");
        Invoke("menuScene", 3.2f);
    }

    void menuScene()
    {
        SceneManager.LoadScene("RealMenu");
    }


}
