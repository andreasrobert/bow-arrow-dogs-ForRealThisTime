using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void start()
    {

    }

    private void Awake()
    {
        GameIsPaused = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }        
    }

    void FixedUpdate()
    {

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        AudioManager.instance.StopPlaying("Menu");
        AudioManager.instance.Play("Theme");
    }

    void Pause()
    {
        //FindObjectOfType<AudioManager>().StopPlaying("Theme");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        AudioManager.instance.StopPlaying("Theme");
        AudioManager.instance.Play("Menu");
    }

    public void LoadMenu()
    {
        AudioManager.instance.StopPlaying("Menu");
        SceneManager.LoadScene("RealMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }
}
