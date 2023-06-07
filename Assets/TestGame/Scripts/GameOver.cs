using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }
    public void Return()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("wwTests");
    }

    
}
