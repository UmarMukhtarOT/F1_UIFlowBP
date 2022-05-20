using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject camera;
    public GameObject miniMapCamera;
    public GameObject startAnimCamera;
    public AllAiNamesScriptable aiNamesScriptable;

    public delegate void OnTutorialLevel();
    public OnTutorialLevel OnTutorialLevelStart;
    public delegate void OnRaceCompletion();
    public OnTutorialLevel OnRaceComplete;

    //public List<string> aiNames;

    private void Awake()
    {
        OnTutorialLevelStart += OnTutorialLevelStartFun;

    }
    private void OnDisable()
    {
        OnTutorialLevelStart -= OnTutorialLevelStartFun;

    }
    private void Start()
    {
        //camera.SetActive(false);
        //startAnimCamera.SetActive(true);
        Time.timeScale = 1.5f;
        //aiNames = aiNamesScriptable.aiNames;
        if (!PlayerPrefs.HasKey(PlayerPrefKeys.TotalNoOfAi))
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.TotalNoOfAi, 1);
        }
    }

    void OnTutorialLevelStartFun()
    {
        miniMapCamera.SetActive(false);
    }

    // this function will be called after 3 2 1 go and switch the camera to main controller camera
    public void SwitchToMainCamera()
    {
        startAnimCamera.SetActive(false);
        camera.SetActive(true);
    }

    public void SwitchToAnimCamera()
    {
        camera.SetActive(false);
        startAnimCamera.SetActive(true);
    }
}
