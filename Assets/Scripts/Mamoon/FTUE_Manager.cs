using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE_Manager : MonoBehaviour
{
    public bool singleHandTutorial = false;
    public bool leftSteer = false;
    public bool rightSteer = false;
    public bool speedDownTrigger = false;


    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey(PlayerPrefKeys.TutorialLevel))
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.TutorialLevel, 0);
        }
    }

    public void SetTutorialLevel()
    {
        if(PlayerPrefs.GetInt(PlayerPrefKeys.TutorialLevel, 0) == 0)
        {
            ReferenceManager.instance.gameManager.OnTutorialLevelStart.Invoke();
        }
    }

    public void DeActiveTutorialHand()
    {
        ReferenceManager.instance.uiManager.tutorialLevelItems.tutorialHand.SetActive(false);
        Time.timeScale = 1.5f;
    }

    public void DeActiveTutorialLeftSteerHand()
    {
        if (!leftSteer)
        {
            ReferenceManager.instance.uiManager.tutorialLevelItems.leftSteerHandImage.SetActive(false);
            //ReferenceManager.instance.uiManager.tutorialLevelItems.rightSteerButton.SetActive(true);
            Time.timeScale = 1.5f;
            leftSteer = true;
        }
    }

    public void DeActiveTutorialRightSteerHand()
    {
        if (!rightSteer)
        {
            ReferenceManager.instance.uiManager.tutorialLevelItems.rightSteerHandImage.SetActive(false);
            //ReferenceManager.instance.uiManager.tutorialLevelItems.leftSteerButton.SetActive(true);
            Time.timeScale = 1.5f;
            rightSteer = true;
        }
    }

    public void DeActiveBothTutorialHandForSpeedDown()
    {
        if (!speedDownTrigger)
        {
            ReferenceManager.instance.uiManager.tutorialLevelItems.rightSteerHandImage.SetActive(false);
            ReferenceManager.instance.uiManager.tutorialLevelItems.leftSteerHandImage.SetActive(false);
            ReferenceManager.instance.uiManager.tutorialLevelItems.slowDownText.SetActive(false);
            Time.timeScale = 1.5f;
            speedDownTrigger = true;
        }

    }
}
