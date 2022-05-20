using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class FinishedUiPanel
{
    public GameObject Finished_Panel;
    public Text FinishedText;
    public Text FinishedPos;
    public GameObject playerInfoContainer;
    public GameObject playerInforContainerParent;
    public GameObject homeButton;
}

[System.Serializable]
public class TutorialLevelUiItems
{
    public GameObject tutorialHand;
    public GameObject rightSteerButton;
    public GameObject rightSteerHandImage;
    public GameObject rightSteerImage;
    public GameObject leftSteerButton;
    public GameObject leftSteerHandImage;
    public GameObject leftSteerImage;
    public GameObject useItemButton;
    public GameObject slowDownText;

}
public class UiManager : MonoBehaviour
{
    [Header("Car UI")]
    public GameObject CarUiPanel;
    public GameObject carControllerPanel;

    [Header("Game Complete Panel")]
    public FinishedUiPanel GameCompletePanel;
    public Sprite[] allAvatarSprites;
    public Transform[] PlayerResultPosition;

    [Header("Pause Panel")]
    public GameObject pausePanel;

    [Header("Tutorial Items")]
    public TutorialLevelUiItems tutorialLevelItems;

    // Start is called before the first frame update
    void Awake()
    {
        ReferenceManager.instance.gameManager.OnTutorialLevelStart += DisableCarButtonsInTutorialLevel;
        ReferenceManager.instance.gameManager.OnRaceComplete += OnRaceCompletion;
    }

    private void OnDisable()
    {
        ReferenceManager.instance.gameManager.OnTutorialLevelStart -= DisableCarButtonsInTutorialLevel;
        ReferenceManager.instance.gameManager.OnRaceComplete -= OnRaceCompletion;

    }

    void OnRaceCompletion()
    {
        CarUiPanel.SetActive(false);

    }
    public void DisableCarButtonsInTutorialLevel()
    {
        tutorialLevelItems.tutorialHand.SetActive(false);
        tutorialLevelItems.useItemButton.SetActive(false);
        tutorialLevelItems.leftSteerButton.SetActive(false);
        tutorialLevelItems.rightSteerButton.SetActive(false);
    }

    public void GamePause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        CarUiPanel.SetActive(false);
    }
    public void GameResume()
    {
        Time.timeScale = 1.5f;
        pausePanel.SetActive(false);
        CarUiPanel.SetActive(true);
    }

    public Sprite GetRandomAvatar()
    {
        int randomAvatarNo = Random.Range(0, allAvatarSprites.Length);
        return allAvatarSprites[randomAvatarNo];
    }

    public void Next()
    {

        // checking if this was tutorial level then mark it as Tutorial completed
        if (PlayerPrefs.GetInt(PlayerPrefKeys.TutorialLevel, 0) == 0)
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.TutorialLevel, 1);
            SceneManager.LoadScene("01_PlayerSelection");

        }
        else
        {

            // Track Generation on next level

            if (PlayerPrefs.GetInt(PlayerPrefKeys.SelectedTrack) < ReferenceManager.instance.trackManager.trackManagerScriptable.allPlayableTracks.Length)
            {
                PlayerPrefs.SetInt(PlayerPrefKeys.SelectedTrack, (PlayerPrefs.GetInt(PlayerPrefKeys.SelectedTrack) + 1));
            }
            if (PlayerPrefs.GetInt(PlayerPrefKeys.SelectedTrack) >= ReferenceManager.instance.trackManager.trackManagerScriptable.allPlayableTracks.Length)
            {
                PlayerPrefs.SetInt(PlayerPrefKeys.SelectedTrack, 0);

            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        //PlayerPrefs.SetInt(PlayerPrefKeys.TotalCoins, (PlayerPrefs.GetInt(PlayerPrefKeys.TotalCoins + 500)));
    }
}
