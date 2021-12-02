using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    void start()
    {
        //AudioManager.instance.Play("Menu");
        Time.timeScale = 1f;
    }

    void Awake()
    {
        AudioManager.instance.Play("Menu");
        Time.timeScale = 1f;
    }
    void Update()
    {
    
    }

    void FixedUpdate()
    {

    }


    public void LoadLevel1()
    {
        SceneManager.LoadScene("RealLevel1.0");
        AudioManager.instance.StopPlaying("Menu");
        AudioManager.instance.Play("Theme");
        Time.timeScale = 1f;
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("RealLevel2.0");
        AudioManager.instance.StopPlaying("Menu");
        AudioManager.instance.Play("Theme");
        Time.timeScale = 1f;
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("RealLevel3.0");
        AudioManager.instance.StopPlaying("Menu");
        AudioManager.instance.Play("Theme");
        Time.timeScale = 1f;
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene("RealLevel4.0");
        AudioManager.instance.StopPlaying("Menu");
        AudioManager.instance.Play("Theme");
        Time.timeScale = 1f;
    }


}
