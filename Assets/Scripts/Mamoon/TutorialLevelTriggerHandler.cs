using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialTriggerType { SteerLeft, SteerRight, SpeedDown, PickedItem, LeftRightSteer}

public class TutorialLevelTriggerHandler : MonoBehaviour
{
    public TutorialTriggerType triggerType;

    UiManager uiManager;
    private bool once;
    

    // Start is called before the first frame update
    void Start()
    {
        uiManager = ReferenceManager.instance.uiManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Tags.Player) && !once)
        {
            Time.timeScale = 0.01f;
            switch (triggerType)
            {
                case TutorialTriggerType.LeftRightSteer:
                    ReferenceManager.instance.ftue_manager.singleHandTutorial = true;
                    uiManager.tutorialLevelItems.leftSteerButton.SetActive(true);
                    uiManager.tutorialLevelItems.rightSteerButton.SetActive(true);
                    //uiManager.tutorialLevelItems.leftSteerHandImage.SetActive(true);
                    Debug.Log("Left Steer Tutorial");
                    gameObject.SetActive(false);

                    break;
                case TutorialTriggerType.SteerLeft:
                    ReferenceManager.instance.ftue_manager.singleHandTutorial = true;
                    uiManager.tutorialLevelItems.leftSteerButton.SetActive(true);
                    //uiManager.tutorialLevelItems.rightSteerButton.SetActive(false);
                    uiManager.tutorialLevelItems.leftSteerHandImage.SetActive(true);
                    Debug.Log("Left Steer Tutorial");
                    gameObject.SetActive(false);

                    break;
                case TutorialTriggerType.SteerRight:
                    ReferenceManager.instance.ftue_manager.singleHandTutorial = true;
                    uiManager.tutorialLevelItems.rightSteerButton.SetActive(true);
                    //uiManager.tutorialLevelItems.leftSteerButton.SetActive(false);
                    uiManager.tutorialLevelItems.rightSteerHandImage.SetActive(true);
                    Debug.Log("Right Steer Tutorial");
                    gameObject.SetActive(false);

                    break;

                case TutorialTriggerType.SpeedDown:
                    ReferenceManager.instance.ftue_manager.singleHandTutorial = false;
                    uiManager.tutorialLevelItems.leftSteerHandImage.SetActive(true);
                    uiManager.tutorialLevelItems.rightSteerHandImage.SetActive(true);
                    uiManager.tutorialLevelItems.slowDownText.SetActive(true);
                    Debug.Log("Speed Down Tutorial");
                    gameObject.SetActive(false);

                    break;
            }
                
        }
    }
}
