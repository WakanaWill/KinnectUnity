using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject Canvas;
    public GameObject Panel;
    public GameObject Camera;

    bool Paused = false;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseButton;

    void Start()
    {
        Canvas.gameObject.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKey ("space"))
        {
            if (!Paused)
            {
                PauseButton();
            }
            else
            {
                ResumeButton();
            }
        }
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        Canvas.gameObject.SetActive(false);
        Panel.gameObject.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseButton()
    {
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
        _pauseButton.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        Panel.gameObject.SetActive(false);
    }
    public void ResumeButton()
    {
        Time.timeScale = 1.0f;
        _pauseMenu.SetActive(false);
        _pauseButton.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        Panel.gameObject.SetActive(true);

    }
}
