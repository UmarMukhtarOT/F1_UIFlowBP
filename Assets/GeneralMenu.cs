using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_IOS

//using Unity.Advertisement.IosSupport;

#endif


public class GeneralMenu : MonoBehaviour
{
    public int screenNum;
    public GameObject shopPanel,eventsPanel,loadingPanel, UserProfilePanel, ExitPanel;
    public Button[] lowerButtons;
    public string garageSceneName, mainMenuSeneName,gamePlaySceneName;

    public Text  UsernameDisplayText;
    public InputField inputField;
    private string _userName;
#if UNITY_IOS
    //private ATTrackingStatusBinding.AuthorizationTrackingStatus m_PreviousStatus;
    //private bool statusUpdated;
#endif

    public string userName
    {
        get
        {
            if (_userName == string.Empty || _userName == null)
                _userName = PlayerPrefs.GetString("PlayerName", "Player");
            return _userName;
        }
        set
        {
            _userName = value;
            PlayerPrefs.SetString("PlayerName", _userName);

        }
    }




    // Start is called before the first frame update
    void Start()
    {

        if(!PlayerPrefs.HasKey(PlayerPrefKeys.FirstPlay))
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.FirstPlay, 0);

        }

        if (PlayerPrefs.GetInt(PlayerPrefKeys.FirstPlay)==0)
        {

            PromptUserNamePanel();


        }
        else
        {

          //  Debug.Log("user name is : " + GetUserName());
            UsernameDisplayText.text = GetUserName();
            UserProfilePanel.SetActive(false);
        }




        if (screenNum==0)
        {
            foreach (var item in lowerButtons)
            {
                item.interactable = true;

            }

        }

        if (screenNum==1)
        {

            DisablCurrentScreenBtn();
        }


#if UNITY_IOS
        //m_PreviousStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
#endif
        AppTracking();


    }


    public void PromptUserNamePanel()
    {
        UserProfilePanel.SetActive(true);


    }


    public void SetUserName(string text)
    {
        userName = text;
    }

    public string GetUserName()
    {
        return userName;

    }





    // Update is called once per frame
    private void Update()
    {
#if UNITY_IOS
        //var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
        //if (m_PreviousStatus != status)
        //{
        //    m_PreviousStatus = status;
        //    Debug.LogFormat("Tracking status updated: {0}", status);
        //    statusUpdated = true;
        //}

        //if (statusUpdated)
        //{
        //    statusUpdated = false;
        //    if (m_PreviousStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
        //    {
        //        //SDKInitialization();

        //    }
        //    else
        //    {
        //        //SDKInitialization();
        //        PlayerPrefs.SetInt("ConsentValue", 1);

        //    }
        //}
#endif
        //if (RewardBool)
        //{
        //    RewardBool = false;
        //    Rewards();


        //}
    }
    public void AppTracking()
    {
#if UNITY_IOS
        //if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        //{
        //    ATTrackingStatusBinding.RequestAuthorizationTracking();
        //    //InitilizedSDKs(true);
        //}

        //else
        //{

        //    if (PlayerPrefs.GetInt("ConsentValue", 1) == 2)
        //    {
        //        //SDKInitialization();
        //    }
        //    else if (PlayerPrefs.GetInt("ConsentValue", 1) == 1)
        //    {
        //       // SDKInitialization();
        //    }
        //}
#endif
    }

    public void OnUserNameNotedOK()
    {


        if (inputField.text == string.Empty || inputField.text == null)
            inputField.text = PlayerPrefs.GetString("PlayerName", "Player" + (Random.Range(45562, 65705)));

        SetUserName(inputField.text);
        UsernameDisplayText.text = GetUserName();

        UserProfilePanel.SetActive(false);
        if (PlayerPrefs.GetInt(PlayerPrefKeys.FirstPlay) == 0)
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.FirstPlay, 1);
            StartCoroutine(LoadLevel(gamePlaySceneName));
        }

    }



    public  void SetUPScreen(int Num)
    {

        lowerButtons[Num].interactable = false;
        switch (Num)
        {

            
            case 0:
                shopPanel.SetActive(true);

                break;

            case 1:
           
           StartCoroutine(LoadLevel(garageSceneName));
           
                break;

            case 2:
            
           StartCoroutine(LoadLevel(gamePlaySceneName));
           
                break;
            case 3:
            eventsPanel.SetActive(true);
          
           
                break;



            default:
                break;
        }



  }




    IEnumerator LoadLevel(string Name)
    {
        foreach (var item in lowerButtons)
        {
            item.gameObject.SetActive(false);
        }
        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(Name);

    }



    public void LoadMainMenu()
    {

        StartCoroutine(LoadLevel(mainMenuSeneName));

    }




    public void DisablCurrentScreenBtn()
    {
        lowerButtons[screenNum].interactable = false;



    }



    public void PromptExit(bool check)
    {
        ExitPanel.SetActive(check);
       

    }


    public void ExitYes()
    {
        Debug.Log("quit app");
        Application.Quit();
    }




}
