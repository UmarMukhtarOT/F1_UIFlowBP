using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;


public class PlayerSelection : MonoBehaviour
{

    [System.Serializable]
    public class PlayerAttributes
    {

        public string Name;


        public GameObject PlayerObject;
        public Transform ActualPos;
        [Range(0, 100)]
        public float Speed;
        [Range(0, 100)]
        public float Boost;
        [Range(0, 100)]
        public float Accel;
        [Range(0, 100)]
        public float Shocks;
        public bool Locked;
        public int Price;



        [Space(15)]

        public float speedDefault;
        public float boostDefault;
        public float AccelDefault;
        public float shocksDefault;


    }



    public static float EpicPowerAmount = 0.25f;
    public static float RarePowerAmount = 0.45f;
    public static float LegendaryPowerAmount = 0.8f;

    [Header("Scene Selection")]
    public string PreviousScene;
    public string NextScene;
    [Header("Player Attributes")]

    public Transform DisplayPos;







    [Header("UI Elements")]
    public GarageRefrences UI_Reference;

    CoreTypeInfo coreTypeForEquip;
    CorePowerType corePowerTypeForEquip;
    int coreAmountTO_Equip;
    GameObject CurrentBtn;




    public CoreGenerationHandler coreGenScript;
    public Battle_Pass_Manager BPManagerScript;
    public static PlayerSelection Instance;
    public static int current;
    public PlayerAttributes[] Players;
    public GameObject OnPurcheseConfetti;
    public GameObject[] ObjToHideOnPrompt;
    public GameObject[] MainPanelObj;
    private bool InCoreState;






    public GameObject[] VehicleNameImages;



    public void HidePrompt()
    {


        HideOnPrompt(true);


    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }



    void Start()
    {
        current = PlayerPrefs.GetInt(PlayerPrefKeys.SelectedVehicleIndex);



        InCoreState = false;
        foreach (var item in Players)
        {

            item.Name = item.PlayerObject.name;

        }
        // setting Starting playerPref Values
        SetAndSaveCoreInfo();





        Time.timeScale = 1;



        UI_Reference.LoadingScreen.SetActive(false);



        UI_Reference.TotalCash.text = CoinsManager.instance.Totalcoins.ToString();

        PlayerPrefs.SetString(Players[0].Name, "Unlocked");

        GetPlayerInfo();

        SoundManager.Instance.PlayBG(SoundManager.Instance.MMSound, 0.5f);


    }


    void Update()
    {
        
    }


    public void UnlockAllVehiclePurchased()
    {


        for (int i = 0; i < Players.Length; i++)
        {
            PlayerPrefs.SetString(Players[i].Name, "Unlocked");
        }

    }


    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Megus+Games&hl=en");
    }



    public void GetPlayerInfo()
    {


        foreach (var item in Players)
        {
            item.PlayerObject.SetActive(false);
        }
        foreach (var item in VehicleNameImages)
        {
            item.SetActive(false);
        }


        Players[current].PlayerObject.SetActive(true);
        VehicleNameImages[current].SetActive(true);



        if (Players[current].Locked)
        {

            UI_Reference.PlayBtn.gameObject.SetActive(false);
            UI_Reference.BuyButton.gameObject.SetActive(true);
            UI_Reference.coreButton.gameObject.SetActive(false);

            UI_Reference.PlayButtonGenericMenu.interactable = false;
            UI_Reference.InfoText.text = Players[current].Price.ToString();

        }
        else
        {

            UI_Reference.PlayBtn.gameObject.SetActive(true);
            UI_Reference.BuyButton.gameObject.SetActive(false);
            //UI_Reference.coreButton.gameObject.SetActive(true);
            UI_Reference.PlayButtonGenericMenu.interactable = true;
            PlayerPrefs.SetInt(PlayerPrefKeys.SelectedVehicleIndex, current);

        }


        SetArrowButtons();


        UI_Reference.Speed_Bar_Img.fillAmount = Players[current].Speed * 0.01f;
        UI_Reference.Boost_Bar_Img.fillAmount = Players[current].Boost * 0.01f;
        UI_Reference.Acceleration_Bar_Img.fillAmount = Players[current].Accel * 0.01f;
        UI_Reference.Shocks_Bar_Img.fillAmount = Players[current].Shocks * 0.01f;










        if (Players[current].Price <= CoinsManager.instance.Totalcoins)
        {
            UI_Reference.InfoText.color = Color.white;

        }
        else
        {
            UI_Reference.InfoText.color = Color.red;
        }


        if (PlayerPrefs.GetString(Players[current].Name) == "Unlocked")
        {

            UI_Reference.PlayBtn.gameObject.SetActive(true);
            UI_Reference.PlayButtonGenericMenu.interactable = true;

            if (!InCoreState)
            {
                UI_Reference.coreButton.gameObject.SetActive(true);
            }
            UI_Reference.BuyButton.gameObject.SetActive(false);
            UI_Reference.InfoText.color = Color.white;
            //UpdateCoreValues();

        }
  

    }


    void SetArrowButtons()
    {
        if (current == 0)
        {
            UI_Reference.PrevBtn.SetActive(false);
            UI_Reference.NextBtn.SetActive(true);
            UI_Reference.BuyStatusPanel.SetActive(false);
        }
        else if (current == Players.Length - 1)
        {
            UI_Reference.PrevBtn.SetActive(true);
            UI_Reference.NextBtn.SetActive(false);
        }
        else
        {
            UI_Reference.PrevBtn.SetActive(true);
            UI_Reference.NextBtn.SetActive(true);
        }



    }








    public void Buy()
    {
        OnPurcheseConfetti.SetActive(false);
        if (Players[current].Price <= CoinsManager.instance.Totalcoins)
        {
            CoinsManager.instance.DedCoins(Players[current].Price);
            UI_Reference.TotalCash.text = "COINS :" + CoinsManager.instance.Totalcoins.ToString();

            Players[current].Locked = false;
            PlayerPrefs.SetString(Players[current].Name, "Unlocked");
            UI_Reference.InfoText.color = Color.white;


            UI_Reference.PlayBtn.gameObject.SetActive(true);
            UI_Reference.coreButton.gameObject.SetActive(true);
            UI_Reference.BuyButton.gameObject.SetActive(false);
            UI_Reference.PlayButtonGenericMenu.interactable = true;

            OnPurcheseConfetti.SetActive(true);

        }
        else

        {

            HideOnPrompt(false);

            UI_Reference.BuyStatusPanel.SetActive(true);
            UI_Reference.CongratulationsTitle.SetActive(false);
            UI_Reference.EarnMoreTitle.SetActive(true);
            UI_Reference.BuyStatusPhraseTxt.text = "Insufficient coins";

        }
        GetPlayerInfo();
        SoundManager.Instance.PlayBtnClick();
    }

    public void Previous()
    {


        current--;
        GetPlayerInfo();


        SoundManager.Instance.PlayBtnClick();
    }

    public void Next()
    {
        current++;
        GetPlayerInfo();
        SoundManager.Instance.PlayBtnClick();
    }



    public void PlayLevel()
    {

        Players[current].PlayerObject.SetActive(false);

        //  PlayerPrefs.SetInt(PlayerPrefKeys.SelectedVehicleIndex, current);
        HideOnPrompt(false);
        UI_Reference.coreButton.SetActive(false);
        UI_Reference.LoadingScreen.SetActive(true);
        SoundManager.Instance.PlayBtnClick();
        StartCoroutine(LevelStart());
    }







    IEnumerator LevelStart()
    {

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(NextScene.ToString());

    }


    public void closeStatusPanel()
    {
        Debug.Log(" clicked");
        UI_Reference.BuyStatusPanel.SetActive(false);
        HideOnPrompt(true);

    }





    public void BackBtn()
    {

        HidePrompt();
        UI_Reference.LoadingScreen.SetActive(true);
        SceneManager.LoadScene(PreviousScene.ToString());
    }

    public void HideOnPrompt(bool check)
    {
        foreach (var item in ObjToHideOnPrompt)
        {
            item.SetActive(check);
        }

    }





    public void CoreCustomizer(bool status)
    {

        UI_Reference.CorePanel.SetActive(status);
        UI_Reference.GeneralMenu.SetActive(!status);
       
        UI_Reference.coreButton.SetActive(!status);
        UI_Reference.NextBtn.SetActive(!status);
        UI_Reference.PrevBtn.SetActive(!status);
        InCoreState = !InCoreState;

        SetArrowButtons();



    }

   










    public void SetAndSaveCoreInfo()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            if (!PlayerPrefs.HasKey(Players[i].PlayerObject.name + "Speed"))
                PlayerPrefs.SetFloat(Players[i].PlayerObject.name + "Speed", Players[i].speedDefault);

            if (!PlayerPrefs.HasKey(Players[i].PlayerObject.name + "Boost"))
                PlayerPrefs.SetFloat(Players[i].PlayerObject.name + "Boost", Players[i].boostDefault);

            if (!PlayerPrefs.HasKey(Players[i].PlayerObject.name + "Accel"))
                PlayerPrefs.SetFloat(Players[i].PlayerObject.name + "Accel", Players[i].AccelDefault);

            if (!PlayerPrefs.HasKey(Players[i].PlayerObject.name + "Shocks"))
                PlayerPrefs.SetFloat(Players[i].PlayerObject.name + "Shocks", Players[i].shocksDefault);


            Players[i].Speed = PlayerPrefs.GetFloat(Players[i].PlayerObject.name + "Speed");
            Players[i].Boost = PlayerPrefs.GetFloat(Players[i].PlayerObject.name + "Boost");
            Players[i].Accel = PlayerPrefs.GetFloat(Players[i].PlayerObject.name + "Accel");
            Players[i].Shocks = PlayerPrefs.GetFloat(Players[i].PlayerObject.name + "Shocks");

        }
    }












}
