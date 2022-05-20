using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{

    public int Totalcoins;
    public int TotalGems;
    public int TestCoins;

    public static CoinsManager instance;
    public bool GiveTestCoins;
    public Text TotalCoinsText;
    public Text TotalGemsText;
   
    



    // Start is called before the first frame update
    void Start()
    {

        if (instance==null)
        {
            instance = this;
        }


        if (GiveTestCoins)
        {
            AddCoins(TestCoins);

        }


      //  AddCoins(100000);
        Totalcoins = PlayerPrefs.GetInt(PlayerPrefKeys.TotalCoins);
        TotalGems = PlayerPrefs.GetInt(PlayerPrefKeys.TotalGems);
        UpdateStat();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddCoins(int amount)
    {
        Totalcoins += amount;
        PlayerPrefs.SetInt(PlayerPrefKeys.TotalCoins, Totalcoins);
        UpdateStat();
    }

    
    public void AddGems(int amount)
    {
        TotalGems += amount;
        PlayerPrefs.SetInt(PlayerPrefKeys.TotalGems, TotalGems);
        UpdateStat();
    }








    public void DedCoins(int amount)
    {
        Totalcoins -= amount;
        PlayerPrefs.SetInt(PlayerPrefKeys.TotalCoins, Totalcoins);
        UpdateStat();
    }

    
    public void DedGems(int amount)
    {
        TotalGems -= amount;
        PlayerPrefs.SetInt(PlayerPrefKeys.TotalGems, TotalGems);
        UpdateStat();
    }


    void UpdateStat()
    {

        if (TotalCoinsText)
        {
            TotalCoinsText.text = Totalcoins + "";

        }

        if (TotalGemsText)
        {
            TotalGemsText.text = TotalGems + "";
        }
        {
           

        }

    }


}
