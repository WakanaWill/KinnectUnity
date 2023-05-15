using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{


    bool Paused = false;
    [SerializeField] private GameObject _pauseMenu;

    void Start()
    {
        _pauseMenu.gameObject.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.P))
        {
            if (Paused == false)
            {
                PauseButton();
            }
            else
            {
                ResumeButton();
            }
        }
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseButton()
    {
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Paused = true;
    }
    public void ResumeButton()
    {
        Time.timeScale = 1.0f;
        _pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        Paused = false;
    }
}
