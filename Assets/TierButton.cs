using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class BPCoreInfoClass
{

    public CoreTypeInfo coretype;
    public CorePowerType corePowerType;



}




public class TierButton : MonoBehaviour
{
    [Header("Lock/UnLock")]



    [Space(30)]
    public int unlockThreshHold; 

    public bool isUnlocked;
    [Space(30)]
    public GameObject TickMark;
    public GameObject CollectBtn;
   

    [HideInInspector]
    public GameObject AchievementStar_Golden;
    






    [Space(30)]
    public bool hasCoins;
    public bool hasGems;
    public bool hasCore;
    private bool IsPicked;
    
    public int tierCoins;
    public int tierGems;
    public int noOfCores;

    [Space(30)]
    public Image displayImage;
    public GarageRefrences UI_Reference;
    public Battle_Pass_Manager BpManager;
    public List<BPCoreInfoClass> bpCoreInfoClass;


    // Start is called before the first frame update
    void Start()
    {




        for (int i = 0; i < noOfCores; i++)
        {

            bpCoreInfoClass[i].coretype = getRandomCoreType();
            bpCoreInfoClass[i].corePowerType = getRandomCorePower();

        }



        if (PlayerPrefs.GetInt(PlayerPrefKeys.TotalGems) >= unlockThreshHold)
        {
            isUnlocked = true;
            LockedBehaviour();          //Uncollectable

        }
        else
        {
            isUnlocked = false;
            UnLockedBehaviour();            //Collectable

        }


    }

    public void GeneratePopUpCards()
    {
       

        if (hasCoins)
        {

            Debug.Log("Granting Coins");
            BP_EquipButton PopUpCard = Instantiate(BpManager.PopUpCardPrefab, UI_Reference.BP_PopUp_PrefabParent.transform);
            PopUpCard.AmountText.text = tierCoins + " Coins";
          
            PopUpCard.cardIcone.sprite = UI_Reference.Bp_CoinIcon;
        }
        if (hasGems)
         {
            Debug.Log("Granting Gems");

            BP_EquipButton PopUpCard = Instantiate(BpManager.PopUpCardPrefab, UI_Reference.BP_PopUp_PrefabParent.transform);
            PopUpCard.AmountText.text = tierGems + " Gems";
           
            PopUpCard.cardIcone.sprite = UI_Reference.Bp_GemIcon;
        }

         if (hasCore)
         {

            Debug.Log("Granting Cores");


            for (int i = 0; i < bpCoreInfoClass.Count; i++)
            {
                BP_EquipButton PopUpCard = Instantiate(BpManager.PopUpCardPrefab, UI_Reference.BP_PopUp_PrefabParent.transform);
                PopUpCard.AmountText.text = bpCoreInfoClass[i].corePowerType+" "+ bpCoreInfoClass[i].coretype+" Core";
             
                PopUpCard.cardIcone.sprite = UI_Reference.CoreTypeIconSprite[(int)bpCoreInfoClass[i].coretype];
                PopUpCard.gameObject.SetActive(true);
            }
         }





    }


    public void LockedBehaviour()
    {

        //SetCoreStatus();
       

    }

    
    public void UnLockedBehaviour()
    {
       // SetCoreStatus();
      

    }

    public void OpenToCollect()
    {
        UI_Reference.BP_PopupCollectButton.onClick.AddListener(() => CollectTier());
    

     UI_Reference.BP_PopUp.SetActive(true);
        GeneratePopUpCards();
    }








    public void CollectTier()
    {

        foreach (Transform child in UI_Reference.BP_PopUp_PrefabParent.transform)
        {
            //Debug.Log(child.name + "dvz");
            if (child.GetComponent<BP_EquipButton>())
            {
               
                Destroy(child.gameObject);

            }
        }

       


        if (hasCoins)
            {
                CoinsManager.instance.AddCoins(tierCoins);
            }
            if (hasGems)
            {
                CoinsManager.instance.AddCoins(tierGems);

            }

            if (hasCore)
            {
                for (int i = 0; i < bpCoreInfoClass.Count; i++)
                {

                    PlayerSelection.Instance.coreGenScript.AddCoreInInventory(bpCoreInfoClass[i].coretype, bpCoreInfoClass[i].corePowerType, 1);

                }

            }

            IsPicked = true;
        CollectBtn.SetActive(false);
        TickMark.SetActive(true);
        UI_Reference.BP_PopUp.SetActive(false);
        PlayerPrefs.SetInt(gameObject.name + "picked", 1);
        BpManager.UpdateCoinsGems(); 
        BpManager.CheckBpLockStatus();

    }

     CorePowerType getRandomCorePower()
     {
        
        CorePowerType coreTempPower = (CorePowerType) UnityEngine.Random.Range(0, 2);
        return coreTempPower;

     }


      CoreTypeInfo getRandomCoreType()
      {
        
        CoreTypeInfo CoretypE = (CoreTypeInfo)UnityEngine.Random.Range(0,3);
        return CoretypE;

      }




    


}
