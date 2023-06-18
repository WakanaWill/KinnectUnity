using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissingPlayer : MonoBehaviour
{
    bool missingPlayer = false;
    bool missingKinect = false;

    [SerializeField] GameObject missingPopUp;
    [SerializeField] Text text;

    [SerializeField] AudioSource warningSFX;
    private void Update()
    {
        /*if(!KinectManager.IsKinectInitialized())
        {
            Time.timeScale = 0;
            missingPopUp.SetActive(true);
            text.text = "Please connect\r\nthe sensor";
        }
        else
        {
            Time.timeScale = 1;
            missingPopUp.SetActive(false);
        }*/



        if (!KinectManager.Instance.IsUserDetected() && KinectManager.IsKinectInitialized())
        {
            warningSFX.Play();
            Time.timeScale = 0;
            missingPopUp.SetActive(true);
            text.text = "Please stand back\r\nin the view of the sensor";
        }
        else
        {
            Time.timeScale = 1;
            missingPopUp.SetActive(false);
        }
    }

    public void SetMissingPlayer(bool val)
    {
        missingPlayer = val;
    }

    public void SetMissingKinect(bool val)
    {
        missingKinect = val;
    }
}
