using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle_Pass_Manager : MonoBehaviour
{
    public BP_EquipButton PopUpCardPrefab;
    public Slider progressSlider;
    public Slider sliderForHandelImage;

    
    public List<TierButton> regularButtons;    
    public List<TierButton> premiumButtons;
    public GameObject[] AchvStarsYellow;
    public Text GemsText;
    public Text CoinsText;
    
    
  
   
    





    // Start is called before the first frame update
    void Start()
    {


        for (int i = 0; i < regularButtons.Count; i++)
        {

            regularButtons[i].AchievementStar_Golden = AchvStarsYellow[i];
            premiumButtons[i].AchievementStar_Golden = AchvStarsYellow[i];



        }
        UpdateCoinsGems();
        CheckBpLockStatus();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }






    public void CheckBpLockStatus()
    {


        for (int i = 0; i < regularButtons.Count; i++)
        {

            if (!PlayerPrefs.HasKey(regularButtons[i].name + "picked"))
            {
                
                PlayerPrefs.SetInt(regularButtons[i].name + "picked", 0);


            }

            regularButtons[i].unlockThreshHold = 100 * (i+1);


            if (PlayerPrefs.GetInt(PlayerPrefKeys.TotalGems) >= regularButtons[i].unlockThreshHold)
            {


                if (PlayerPrefs.GetInt(regularButtons[i].name + "picked") == 0)
                {
                    //Debug.Log("Index "+i);
                    regularButtons[i].CollectBtn.gameObject.SetActive(true);
                  //  premiumButtons[i].CollectBtn.gameObject.SetActive(true);
                    regularButtons[i].TickMark.gameObject.SetActive(false);
                  //  premiumButtons[i].TickMark.gameObject.SetActive(false);

                }
                else
                {
                    regularButtons[i].TickMark.gameObject.SetActive(true);
                   // premiumButtons[i].TickMark.gameObject.SetActive(true);
                    regularButtons[i].CollectBtn.gameObject.SetActive(false);
                   // premiumButtons[i].CollectBtn.gameObject.SetActive(false);

                }



            }
            else
            {
                regularButtons[i].CollectBtn.gameObject.SetActive(false);
                regularButtons[i].TickMark.gameObject.SetActive(false);
              //  premiumButtons[i].CollectBtn.gameObject.SetActive(false);
               // premiumButtons[i].TickMark.gameObject.SetActive(false);


            }


            


               
        }


    }



    public void UpdateCoinsGems()
    {
        GemsText.text = PlayerPrefs.GetInt(PlayerPrefKeys.TotalGems) + "";
        CoinsText.text = PlayerPrefs.GetInt(PlayerPrefKeys.TotalCoins) + "";




    }






}
