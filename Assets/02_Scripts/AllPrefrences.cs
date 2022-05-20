using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPrefrences : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);

        }

        if (!PlayerPrefs.HasKey("RemoveAds"))
        {
            PlayerPrefs.SetInt("RemoveAds", 0);

        }


        if (!PlayerPrefs.HasKey("TotalCoins"))
        {
            PlayerPrefs.SetInt("TotalCoins", 0);

        }

        if (!PlayerPrefs.HasKey("MaxPlayed0"))
        {
            PlayerPrefs.SetInt("MaxPlayed0", 0);

        }

       
        if (!PlayerPrefs.HasKey("MaxPlayed1"))
        {
            PlayerPrefs.SetInt("MaxPlayed1", 0);

        }
         if (!PlayerPrefs.HasKey("MaxPlayed2"))
         {
            PlayerPrefs.SetInt("MaxPlayed2", 0);

         }
          if (!PlayerPrefs.HasKey("MaxPlayed3"))
          {
            PlayerPrefs.SetInt("MaxPlayed3", 0);

          }

        if (!PlayerPrefs.HasKey("CurrentControl"))
        {
            PlayerPrefs.SetInt("CurrentControl", 0);

        }



        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);

        }

        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetInt("Music", 1);

        }

    }

   







}
