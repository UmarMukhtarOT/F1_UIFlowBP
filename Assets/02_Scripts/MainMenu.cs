using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    public string nextScene;

    public Image FillImage;
    AsyncOperation async = null;
    public GameObject loadingScreen, SettingPanel, PrivacyPanel, StorePanel, RewardedPanel, ExitPanel, mainPanel, UserProfilePanel;
    public Text CoinTxt, UsernameDisplayText;
    public static bool ShowingPrivacy = true;
    public int TotalCars = 11, TotalLevels = 15;
    public InputField inputField;
    private string _userName;

    public GameObject[] Players;
    public GameObject[] VehicleNameImages;



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
       
        

        if (!PlayerPrefs.HasKey(PlayerPrefKeys.FirstPlay))
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.FirstPlay, 0);
           
            PromptUserNamePanel();


        }
        else
        {
            
            Debug.Log("user name is : "+ GetUserName());
            UsernameDisplayText.text = GetUserName();
            UserProfilePanel.SetActive(false);
        }




        FillImage.fillAmount = 0;
        CoinTxt.text = CoinsManager.instance.Totalcoins + "";
        SoundManager.Instance.PlayBG(SoundManager.Instance.MMSound, 0.5f);



       
        Time.timeScale = 1;
        ClosePrivacy();

        SetSelectedPlayer();
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
            Play();
        }

    }



    public void SetSelectedPlayer()
    { 
    
        int current = PlayerPrefs.GetInt("SelectedPlayer");
        VehicleNameImages[current].SetActive(true);
        Players[current].SetActive(true);



    }



    
    void Update()
    {

        if (async != null)
        {
            FillImage.fillAmount = async.progress;
            if (async.progress >= 0.9f)
            {
                FillImage.fillAmount = 1.0f;
            }
        }

    }


    public void Play()
    {
        StartCoroutine(LevelStart());
        loadingScreen.SetActive(true);
        SoundManager.Instance.PlayBtnClick();
    }



     public void PromptExit(bool check)
     {
        ExitPanel.SetActive(check);
        mainPanel.SetActive(!check);

     }


    public void ExitYes()
    {
        Debug.Log("quit app");
        Application.Quit();
    }






    IEnumerator LevelStart()
    {
        yield return new WaitForSeconds(1.5f);
        

        async = SceneManager.LoadSceneAsync(nextScene);
        yield return async;
    }

    public void RateUS()
    {
        string url = "https://play.google.com/store/apps/details?id=" + Application.identifier;
        Application.OpenURL(url);
        SoundManager.Instance.PlayBtnClick();
    }

    
    public void store()
    {
        StorePanel.SetActive(true);
        mainPanel.SetActive(false);

    }

    public void BackFromstore()
    {
        StorePanel.SetActive(false);
        mainPanel.SetActive(true);

    }



    public void PrivacyPolicy()
    {

        PrivacyPanel.SetActive(true);
    }

    public void ClosePrivacy()
    {
        Time.timeScale = 1;
        ShowingPrivacy = false;
        PrivacyPanel.SetActive(false);
    }







    public void Settings()
    {
        SettingPanel.SetActive(true);
    }

    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Megus+Games&hl=en");
    }
    public void PrivacyPolicyLink()
    {
        Application.OpenURL("https://megusgaming.com/privacy.html");
    }




    public void RemoveAds()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
        Debug.Log("removeads");
    }

    public void UnlockAllCars()
    {
        for (int i = 0; i < TotalCars; i++)
        {
            PlayerPrefs.SetString("Car_" + i, "Unlocked");


        }

        Debug.Log("unlockAllCars" + TotalCars);

    }

    public void UnlockAllLevels()
    {
        PlayerPrefs.SetInt("MaxPlayed0", TotalLevels);
        PlayerPrefs.SetInt("MaxPlayed1", TotalLevels);
        Debug.Log("UnlockAllLevels");

    }

    public void UnlockAllEveryThing()
    {

        for (int i = 0; i < TotalCars; i++)
        {
            PlayerPrefs.SetString("Car_" + i, "Unlocked");
        }

        PlayerPrefs.SetInt("MaxPlayed0", TotalLevels);
        PlayerPrefs.SetInt("MaxPlayed1", TotalLevels);
        PlayerPrefs.SetInt("RemoveAds", 1);
        Debug.Log("UnlockEverything");
    }






    public void GetCoins()
    {
        CloseRewardPanel();
        //***AD Call***//
        //AdsManagerWrapper.Instance.ShowRewardedVideo(RewardedAd);

    }


    public void RewardedAd()
    {
        CoinsManager.instance.AddCoins(1000);

    }



    public void AskForReward()
    {
        RewardedPanel.SetActive(true);
    }

    public void CloseRewardPanel()
    {
        RewardedPanel.SetActive(false);


    }

 
}
