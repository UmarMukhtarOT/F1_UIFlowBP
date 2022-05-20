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
    public bool hasCoins;
    public bool hasGems;
    public bool hasCore;
    public bool isUnlocked;
    private bool IsPicked;
    
    public int tierCoins;
    public int tierGems;
    
    [Space(20)]
    public Image displayImage;
    public int noOfCores;
    public GarageRefrences UI_Reference;
    public Battle_Pass_Manager BpManager;
    public int unlockThreshHold;
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

        BP_EquipButton PopUpCard = Instantiate(BpManager.PopUpCardPrefab, UI_Reference.BP_PopUp.transform);



    }






    public void SetCoreStatus()
    {
        for (int i = 0; i < bpCoreInfoClass.Count; i++)
        {

            switch (bpCoreInfoClass[i].coretype)
            {
                case CoreTypeInfo.Speed:
                    if (isUnlocked)
                    {
                        displayImage.sprite = UI_Reference.Bp_SpeedCoreSpriteUnLocked;

                    }
                    else
                    {

                        displayImage.sprite = UI_Reference.Bp_SpeedCoreSpriteLocked;


                    }
                    break;
                case CoreTypeInfo.Boost:
                    if (isUnlocked)
                    {
                        displayImage.sprite = UI_Reference.Bp_BoostCoreSpriteUnLocked;

                    }
                    else
                    {

                        displayImage.sprite = UI_Reference.Bp_BoostCoreSpriteLocked;


                    }
                    break;
                case CoreTypeInfo.Accelration:
                    if (isUnlocked)
                    {
                        displayImage.sprite = UI_Reference.Bp_AccelCoreSpriteUnLocked;

                    }
                    else
                    {

                        displayImage.sprite = UI_Reference.Bp_AccelCoreSpriteLocked;


                    }
                    break;
                case CoreTypeInfo.Shocks:
                    if (isUnlocked)
                    {
                        displayImage.sprite = UI_Reference.Bp_ShocksCoreSpriteUnLocked;

                    }
                    else
                    {

                        displayImage.sprite = UI_Reference.Bp_ShocksCoreSpriteLocked;


                    }
                    break;
                default:
                    break;
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
      UI_Reference.BP_PopUp.SetActive(true);
    }








    public void CollectTier()
    {
        if (!IsPicked)
        {
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

                    BpManager.GenerateCoreScript.AddCoreInInventory(bpCoreInfoClass[i].coretype, bpCoreInfoClass[i].corePowerType, 1);

                }

            }

            IsPicked = true;
        }



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
